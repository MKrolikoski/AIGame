using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMotor : MonoBehaviour
{
    //Navigation mesh agent
    private NavMeshAgent agent;
    //Indicates if unit is moving
    public bool isMoving { get; private set; }
    //Delegate fired when unit finished moving
    public delegate void OnMoveFinished();
    public OnMoveFinished onMoveFinished;


    void Start ()
    {
        isMoving = false;
        agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if (!agent.pathPending && isMoving)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    isMoving = false;
                    if (onMoveFinished != null)
                    {
                        onMoveFinished.Invoke();
                    }
                }
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
        isMoving = true;
    }
}
