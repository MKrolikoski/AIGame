using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathManager))]
public class InputController : MonoBehaviour
{
    //Singleton
    public static InputController instance;
    //Layer with tiles
    public LayerMask movementMask;
    //Layer with units
    public LayerMask unitMask;
    //Pathfinding tool
    private PathManager pathManager;
    //GameManager
    private GameManager gameManager;
    //Delay on inputs
    private float inputDelay = 0.5f;
    private float inputDelayDefault = 10f;
    //Inputs are disabled if e.g. unit is in the process of movement
    public bool inputEnabled { get; private set; }
    //Direction of moving tile selection
    private enum Direction { Up, Down, Left, Right };
    //Last mouse position different than current
    private Vector3 lastMousePosition;



 
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    void Start()
    {
        pathManager = GetComponent<PathManager>();
        gameManager = GameManager.instance;
    }

    void Update()
    {
        inputDelay -= Time.deltaTime;

        if (inputEnabled && gameManager.ActivePlayer.GetType() == typeof(Player))
        {

            #region selecting tile to move on hover
            if (gameManager.CurrentGameState == GameState.SELECTING_TILE || (gameManager.CurrentGameState == GameState.SELECTING_ENEMY_UNIT && !gameManager.unitMoved))
            {
                Vector3 mousePos = Input.mousePosition;
                if (mousePos != lastMousePosition)
                {
                    Unit unit = GetUnitAtPosition(mousePos);
                    if (unit != null && unit != gameManager.SelectedUnit && gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(unit, gameManager.attackRangeMask))
                    {
                        gameManager.SelectEnemyTile(unit.currentTile);
                    }
                    else
                    {
                        SetPathToPosition(Input.mousePosition);
                    }
                    lastMousePosition = mousePos;
                }

            }
            #endregion

            #region selecting tile with enemy
            if (gameManager.CurrentGameState == GameState.SELECTING_ENEMY_UNIT)
            {
                Vector3 mousePos = Input.mousePosition;
                if (mousePos != lastMousePosition)
                {
                    SetHoveredTileWithEnemy(Input.mousePosition);
                    lastMousePosition = mousePos;
                }

            }
            #endregion

            #region selecting unit
            if (gameManager.ActivePlayer.availableMovesLeft > 0 && Input.GetMouseButtonDown(0) && (gameManager.CurrentGameState == GameState.SELECTING_UNIT || gameManager.CurrentGameState == GameState.SELECTING_TILE))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                selectUnit(ray);
            }
            #endregion

            #region deselecting all
            else if (Input.GetMouseButtonDown(1) && (gameManager.CurrentGameState == GameState.SELECTING_TILE || gameManager.CurrentGameState == GameState.SELECTING_ENEMY_UNIT))
            {
                if(gameManager.unitMoved)
                {
                    gameManager.EndUnitMove(gameManager.SelectedUnit);
                }
                else
                {
                    gameManager.DeselectAll();
                }
            }
            #endregion

            #region input from keyboard
            else if (inputDelay <= 0f)
            {
                if(gameManager.CurrentGameState == GameState.SELECTING_TILE && Input.anyKey)
                {
                    if (Input.GetKey("up"))
                    {
                        ChangeSelectedTile(Direction.Up);
                    }
                    else if (Input.GetKey("down"))
                    {
                        ChangeSelectedTile(Direction.Down);
                    }
                    else if (Input.GetKey("left"))
                    {
                        ChangeSelectedTile(Direction.Left);
                    }
                    else if (Input.GetKey("right"))
                    {
                        ChangeSelectedTile(Direction.Right);
                    }
                    else if (Input.GetKey("space"))
                    {
                        gameManager.MoveUnit();
                    }
                    inputDelay = inputDelayDefault * Time.deltaTime;
                }
                else if (gameManager.CurrentGameState == GameState.SELECTING_ENEMY_UNIT && Input.anyKey)
                {
                    if(!gameManager.unitMoved)
                    {
                        if (Input.GetKey("up"))
                        {
                            ChangeSelectedTile(Direction.Up);
                        }
                        else if (Input.GetKey("down"))
                        {
                            ChangeSelectedTile(Direction.Down);
                        }
                        else if (Input.GetKey("left"))
                        {
                            ChangeSelectedTile(Direction.Left);
                        }
                        else if (Input.GetKey("right"))
                        {
                            ChangeSelectedTile(Direction.Right);
                        }
                        else if (Input.GetKey("space"))
                        {
                            gameManager.AttackUnit();
                        }
                    }
                    else
                    {
                        if (Input.GetKey("right"))
                        {
                            gameManager.NextTileWithEnemy();
                        }
                        else if (Input.GetKey("left"))
                        {
                            gameManager.PreviousTileWithEnemy();
                        }
                        else if (Input.GetKey("space"))
                        {
                            gameManager.AttackUnit();
                        }
                    }
                    inputDelay = inputDelayDefault * Time.deltaTime;
                }
            }
            #endregion
        }
    }

    //TODO -> Change direction to be relative to camera
    private void ChangeSelectedTile(Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                {
                    if (gameManager.SelectedTile !=  null)
                    {
                        if (gameManager.SelectedTile.upperNeighbourTile != null && gameManager.SelectedTile.upperNeighbourTile.canBeSelected && gameManager.SelectedTile.upperNeighbourTile.unitOnTile != gameManager.SelectedUnit)
                        {
                            if (gameManager.SelectedTile.upperNeighbourTile.unitOnTile != null)
                            {
                                if (gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedTile.upperNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedTile.upperNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedTile.upperNeighbourTile;
                            }
                        }
                    }
                    else
                    {
                        if (gameManager.SelectedUnit.currentTile.upperNeighbourTile != null && gameManager.SelectedUnit.currentTile.upperNeighbourTile.canBeSelected)
                        {
                            if (gameManager.SelectedUnit.currentTile.upperNeighbourTile.unitOnTile != null)
                            {
                                if (gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedUnit.currentTile.upperNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedUnit.currentTile.upperNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedUnit.currentTile.upperNeighbourTile;
                            }
                        }
                    }
                    break;
                }
            case Direction.Down:
                {
                    if (gameManager.SelectedTile != null)
                    {
                        if (gameManager.SelectedTile.lowerNeighbourTile != null && gameManager.SelectedTile.lowerNeighbourTile.canBeSelected && gameManager.SelectedTile.lowerNeighbourTile.unitOnTile != gameManager.SelectedUnit)
                        {
                            if (gameManager.SelectedTile.lowerNeighbourTile.unitOnTile != null)
                            {
                                if (gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedTile.lowerNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedTile.lowerNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedTile.lowerNeighbourTile;
                            }
                        }
                    }
                    else
                    {
                        if (gameManager.SelectedUnit.currentTile.lowerNeighbourTile != null && gameManager.SelectedUnit.currentTile.lowerNeighbourTile.canBeSelected)
                        {
                            if (gameManager.SelectedUnit.currentTile.upperNeighbourTile.unitOnTile != null)
                            {
                                if (gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedUnit.currentTile.upperNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedUnit.currentTile.upperNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedUnit.currentTile.lowerNeighbourTile;
                            }
                        }
                    }
                    break;
                }
            case Direction.Left:
                {
                    if (gameManager.SelectedTile != null)
                    {
                        if (gameManager.SelectedTile.leftNeighbourTile != null && gameManager.SelectedTile.leftNeighbourTile.canBeSelected && gameManager.SelectedTile.leftNeighbourTile.unitOnTile != gameManager.SelectedUnit)
                        {
                            if (gameManager.SelectedTile.leftNeighbourTile.unitOnTile != null)
                            {
                                if(gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedTile.leftNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedTile.leftNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedTile.leftNeighbourTile;
                            }
                        }
                    }
                    else
                    {
                        if (gameManager.SelectedUnit.currentTile.leftNeighbourTile != null && gameManager.SelectedUnit.currentTile.leftNeighbourTile.canBeSelected)
                        {
                            if (gameManager.SelectedUnit.currentTile.leftNeighbourTile.unitOnTile != null)
                            {
                                if (gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedUnit.currentTile.leftNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedUnit.currentTile.leftNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedUnit.currentTile.leftNeighbourTile;
                            }
                        }
                    }
                    break;
                }
            case Direction.Right:
                {
                    if (gameManager.SelectedTile != null)
                    {
                        if (gameManager.SelectedTile.rightNeighbourTile != null && gameManager.SelectedTile.rightNeighbourTile.canBeSelected && gameManager.SelectedTile.rightNeighbourTile.unitOnTile != gameManager.SelectedUnit)
                        {
                            if (gameManager.SelectedTile.rightNeighbourTile.unitOnTile != null)
                            {
                                if (gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedTile.rightNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedTile.rightNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedTile.rightNeighbourTile;
                            }
                        }
                    }
                    else
                    {
                        if (gameManager.SelectedUnit.currentTile.rightNeighbourTile != null && gameManager.SelectedUnit.currentTile.rightNeighbourTile.canBeSelected)
                        {
                            if (gameManager.SelectedUnit.currentTile.rightNeighbourTile.unitOnTile != null)
                            {
                                if (gameManager.SelectedUnit.unitCombat.EnemyInAttackRange(gameManager.SelectedUnit.currentTile.rightNeighbourTile.unitOnTile, gameManager.attackRangeMask))
                                {
                                    gameManager.SelectEnemyTile(gameManager.SelectedUnit.currentTile.rightNeighbourTile);
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                gameManager.SelectedTile = gameManager.SelectedUnit.currentTile.rightNeighbourTile;
                            }
                        }
                    }
                    break;
                }
            default: return;
        }
        SetPathToTile(gameManager.SelectedTile);
    }

    private void SetPathToPosition(Vector3 position)
    {
        Tile tile = GetTileAtPosition(position);
        if (tile != null && gameManager.SelectedUnit.currentTile != tile && tile != gameManager.SelectedTile && tile.walkable)
        {
            gameManager.SelectedTiles = pathManager.FindPath(gameManager.SelectedUnit.currentTile, tile, gameManager.SelectedUnit.stats.moveRange.getValue());
            if(gameManager.SelectedTiles.Count > 0)
            {
                gameManager.SelectedTile = gameManager.SelectedTiles[gameManager.SelectedTiles.Count - 1];
            }
        }
    }

    private void SetHoveredTileWithEnemy(Vector3 position)
    {
        Unit unit = GetUnitAtPosition(position);
        if (unit != null && gameManager.SelectedTile != unit.currentTile && gameManager.tilesWithEnemies.Contains(unit.currentTile))
        {
            gameManager.SelectedTile = unit.currentTile;
            gameManager.selectedEnemyTileIndex = gameManager.tilesWithEnemies.IndexOf(unit.currentTile);
            gameManager.SelectedUnit.FaceDirection(unit.transform.position);
        }
    }


    private void SetPathToTile(Tile tile)
    {
        if (tile != null && gameManager.SelectedUnit.currentTile != tile && tile.walkable)
        {
            gameManager.SelectedTiles = pathManager.FindPath(gameManager.SelectedUnit.currentTile, tile, gameManager.SelectedUnit.stats.moveRange.getValue());
            if (gameManager.SelectedTiles.Count > 0)
            {
                gameManager.SelectedTile = gameManager.SelectedTiles[gameManager.SelectedTiles.Count - 1];
            }
        }
    }

    private Tile GetTileAtPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, movementMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if(tile != null)
            {
                return tile;
            }
        }
        return null;
    }

    private Unit GetUnitAtPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, unitMask))
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                return unit;
            }
        }
        return null;
    }

    private void selectUnit(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, unitMask))
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                if(unit.canBeSelected)
                {
                    gameManager.SelectedUnit = unit;
                }
            }
        }
    }



    public void EnableInputs()
    {
        inputEnabled = true;
    }

    public void DisableInputs()
    {
        inputEnabled = false;
    }

}
