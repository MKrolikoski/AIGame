using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputController))]
public class GameManager : MonoBehaviour
{
    //GameManager singleton
    public static GameManager instance;
    //Input controller
    private InputController inputController;
    //Path manager
    private PathManager pathManager;
    //UI
    private UI ui;
    //Selected Unit
    private Unit selectedUnit;
    //Information whether selected unit already moved
    public bool unitMoved;
    //Selected Tile
    private Tile selectedTile;
    //Currently hovered path
    private List<Tile> selectedTiles;
    //Tiles with enemies of current unit
    public List<Tile> tilesWithEnemies { get; private set; }
    //Index of selected tile with current unit's enemy
    public int selectedEnemyTileIndex;
    //Current game state
    private GameState _currentGameState;
    //Players
    public Commander[] players;
    //Active player
    public Commander _activePlayer;
    //Layer mask used when looking for enemies in unit's attack range
    public LayerMask attackRangeMask;
    //Event fired when selecting unit
    public event Action<Unit> OnUnitSelected;
    public event Action OnUnitEndedMove;
    public event Action OnNewTurn;
    public NewTurnPanel newTurnPanel;
    public GameOverPanel gameOverPanel;
    public EndTurnButton endTurnButton;

    #region activePlayer getters/setter
    public Commander ActivePlayer
    {
        get
        {
            return _activePlayer;
        }
        private set
        {
            _activePlayer = value;
            attackRangeMask = _activePlayer.attackMask;
        }
    }
    #endregion

    #region _currentGameState getters/setters
    public GameState CurrentGameState
    {
        get
        {
            return _currentGameState;
        }
        set
        {
            _currentGameState = value;
            switch(_currentGameState)
            {
                case GameState.SELECTING_UNIT:
                    {
                        unitMoved = false;
                        tilesWithEnemies = new List<Tile>();
                        if (inputController != null)
                        {
                            inputController.EnableInputs();
                        }
                    }
                    break;
                case GameState.SELECTING_TILE:
                    {
                        tilesWithEnemies = new List<Tile>();
                        if (inputController != null)
                        {
                            inputController.EnableInputs();
                        }
                    }
                    break;
                case GameState.SELECTED_UNIT_IS_MOVING:
                    {
                        unitMoved = true;
                        if (inputController != null)
                        {
                            inputController.DisableInputs();
                        }
                    }
                    break;
                case GameState.SELECTING_ENEMY_UNIT:
                    {
                        if (inputController != null)
                        {
                            inputController.EnableInputs();
                        }
                    }
                    break;
                case GameState.SELECTED_UNIT_IS_ATTACKING:
                    {
                        if (inputController != null)
                        {
                            inputController.DisableInputs();
                        }
                    }
                    break;
                case GameState.NEW_TURN:
                    {
                        DeselectAll();
                        if (inputController != null)
                        {
                            inputController.DisableInputs();
                        }
                    }
                    break;
                case GameState.GAME_OVER:
                    {
                        DeselectAll();
                        if (inputController != null)
                        {
                            inputController.DisableInputs();
                        }
                    }
                    break;
                default:
                    {
                        if (inputController != null)
                        {
                            inputController.EnableInputs();
                        }
                    }
                    break;
            }
        }
    }
    #endregion

    #region SelectedTiles getters/setters
    public List<Tile> SelectedTiles
    {
        get
        {
            return selectedTiles;
        }

        set
        {
            if(CurrentGameState == GameState.SELECTING_ENEMY_UNIT)
            {
                CurrentGameState = GameState.SELECTING_TILE;
            }
            selectedTiles.ForEach(e => e.DehighlightTile());
            selectedTiles = value;
            selectedTiles.ForEach(e => e.HighlightTile());
        }
    }
    #endregion

    #region SelectedTile getters/setters
    public Tile SelectedTile
    {
        get
        {
            return selectedTile;
        }

        set
        {
            if (selectedTile != null && !SelectedTiles.Contains(selectedTile))
            {
                selectedTile.OnDeselect();
            }
            selectedTile = value;
            if (selectedTile != null)
            {
                if(CurrentGameState == GameState.SELECTING_ENEMY_UNIT)
                {
                    if(selectedTile.IsAdjacentTile(SelectedUnit.currentTile))
                    {
                        selectedTile.ChangeSelectionToCloseRange();
                    }
                    else
                    {
                        selectedTile.ChangeSelectionToRange();
                    }
                    if(selectedTile.unitOnTile != null)
                        selectedTile.unitOnTile.ShowHp();
                }
                else
                {
                    if (selectedTile.selection.GetComponent<Renderer>().material != selectedTile.selection.GetComponent<Selection>().defaultMat)
                    {
                        selectedTile.ChangeSelectionToDefault();
                    }
                }
                selectedTile.OnSelect();
            }
        }
    }
    #endregion

    #region SelectedUnit getters/setters
    public Unit SelectedUnit
    {
        get
        {
            return selectedUnit;
        }

        set
        {
            if (selectedUnit != null)
            {
                selectedUnit.OnDeselect();
            }
            selectedUnit = value;
            if (selectedUnit != null)
            {
                selectedTiles.ForEach(e => e.DehighlightTile());
                selectedUnit.OnSelect();
                CurrentGameState = GameState.SELECTING_TILE;
            }
            else
            {
                CurrentGameState = GameState.SELECTING_UNIT;
            }
            if(OnUnitSelected != null)
            {
                OnUnitSelected(selectedUnit);
            }
        }
    }
    #endregion


    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        selectedTiles = new List<Tile>();
        tilesWithEnemies = new List<Tile>();
    }


    void Start()
    {
        CurrentGameState = GameState.SELECTING_UNIT;
        inputController = GetComponent<InputController>();
        pathManager = GetComponent<PathManager>();
        ui = UI.instance;
        StartGame();
    }

    private void StartGame()
    {
        ActivePlayer = players[0];
        players[1].EndTurn();
        newTurnPanel.SetActivePlayer(ActivePlayer);
        newTurnPanel.Activate();
    }

    public void StartTurn()
    {
        ActivePlayer.StartTurn();
        CurrentGameState = GameState.SELECTING_UNIT;
        if (OnNewTurn != null)
        {
            OnNewTurn();
        }
    }

    public void EndTurn()
    {
        HideEndTurnButton();
        ActivePlayer.EndTurn();
        ActivePlayer = ActivePlayer == players[0] ? players[1] : players[0];
        CurrentGameState = GameState.NEW_TURN;
        newTurnPanel.SetActivePlayer(ActivePlayer);
        newTurnPanel.Activate();
    }

    void Update()
    {

    }

    public void MoveUnit()
    {
        if (SelectedUnit != null && SelectedTiles.Count > 0)
        {
            SelectedUnit.onMovedThroughTiles += SelectedUnitFinishedMoving;
            SelectedUnit.MoveThroughTiles(SelectedTiles);
            CurrentGameState = GameState.SELECTED_UNIT_IS_MOVING;
        }
    }

    public void AttackUnit()
    {
        CurrentGameState = GameState.SELECTED_UNIT_IS_ATTACKING;
        SelectedUnit.unitCombat.FinishedAttacking += SelectedUnitFinishedAttacking;
        SelectedUnit.Attack(SelectedTile.unitOnTile, !SelectedTile.IsAdjacentTile(SelectedUnit.currentTile));
    }

    public void SelectEnemyTile(Tile tile)
    {
        if(tile.unitOnTile.unitOwner != SelectedUnit.unitOwner)
        {
            SelectedTiles.ForEach(e => e.OnDeselect());
            if (tilesWithEnemies.Count > 0)
            {
                tilesWithEnemies = new List<Tile>();
            }
            tilesWithEnemies.Add(tile);
            CurrentGameState = GameState.SELECTING_ENEMY_UNIT;
            selectedEnemyTileIndex = 0;
            SelectedTile = tilesWithEnemies[selectedEnemyTileIndex];
            SelectedUnit.FaceDirection(SelectedTile.unitOnTile.transform.position);
        }
    }

    private void SelectedUnitFinishedAttacking()
    {
        SelectedUnit.unitCombat.FinishedAttacking -= SelectedUnitFinishedAttacking;
        EndUnitMove(SelectedUnit);
    }

    //Reduce active player actions
    public void EndUnitMove(Unit unit)
    {
        ActivePlayer.UnitEndedMove(unit);
        if (OnUnitEndedMove != null)
        {
            OnUnitEndedMove();
        }
        DeselectAll();
        CurrentGameState = GameState.SELECTING_UNIT;
    }

    private void SelectedUnitFinishedMoving()
    {
        SelectedUnit.onMovedThroughTiles -= SelectedUnitFinishedMoving;
        tilesWithEnemies = SelectedUnit.unitCombat.TilesWithEnemiesInRange(attackRangeMask);
        if (tilesWithEnemies.Count > 0)
        {
            CurrentGameState = GameState.SELECTING_ENEMY_UNIT;
            selectedEnemyTileIndex = 0;
            SelectedTile = tilesWithEnemies[selectedEnemyTileIndex];
            SelectedUnit.FaceDirection(SelectedTile.unitOnTile.transform.position);
        }
        else
        {
            EndUnitMove(SelectedUnit);
            CurrentGameState = GameState.SELECTING_UNIT;
        }
    }

    public void NextTileWithEnemy()
    {
        selectedEnemyTileIndex = (selectedEnemyTileIndex + 1) % tilesWithEnemies.Count;
        SelectedTile = tilesWithEnemies[selectedEnemyTileIndex];
        SelectedUnit.FaceDirection(SelectedTile.unitOnTile.transform.position);
    }

    public void PreviousTileWithEnemy()
    {
        selectedEnemyTileIndex = selectedEnemyTileIndex == 0 ? tilesWithEnemies.Count-1 : (selectedEnemyTileIndex - 1) % tilesWithEnemies.Count;
        SelectedTile = tilesWithEnemies[selectedEnemyTileIndex];
        SelectedUnit.FaceDirection(SelectedTile.unitOnTile.transform.position);
    }

    public void EssentialUnitDied(Commander player)
    {
        player.essentialUnits--;
        if(player.essentialUnits <= 0)
        {
            Commander winner = player == players[0] ? players[1] : players[0];
            PlayerWon(winner);
        }
    }

    private void PlayerWon(Commander winner)
    {
        CurrentGameState = GameState.GAME_OVER;
        gameOverPanel.Activate(winner);
    }




    public void DeselectAll()
    {
        if(CurrentGameState == GameState.SELECTING_ENEMY_UNIT)
        {
            CurrentGameState = GameState.SELECTING_UNIT;
        }
        if (SelectedUnit != null)
        {
            SelectedUnit = null;
        }
        if (SelectedTiles != null)
        {
            SelectedTiles = new List<Tile>();
        }
        if (SelectedTile != null)
        {
            SelectedTile = null;
        }
    }

    public void ShowEndTurnButton()
    {
        if(ActivePlayer.GetType() == typeof(Player))
            endTurnButton.Show();
    }

    public void HideEndTurnButton()
    {
        if (ActivePlayer.GetType() == typeof(Player))
            endTurnButton.Hide();
    }


}
