using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent agent;
    
    [SerializeField] private Transform defaultMoveTarget;
    
    private Unit _unit;

    void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void StopMoving()
    {
        agent.speed = 0;
    }

    public void ResumeMoving()
    {
        agent.speed = speed;
    }
    
    public void MoveTowards(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void MoveTowardsNearest()
    {
        MoveTowards(FindNearestEnemy().transform.position);
    }

    Unit FindNearestEnemy()
    {
        List<Unit> enemies = _unit.IsTeamA ? GlobalsVariable.AliveUnitsTeamB : GlobalsVariable.AliveUnitsTeamA;
        return enemies
            .Aggregate((nearest, current) =>
                (current.transform.position - transform.position).sqrMagnitude <
                (nearest.transform.position - transform.position).sqrMagnitude
                    ? current
                    : nearest);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //MoveTowards(defaultMoveTarget.position);
            MoveTowardsNearest();
        }
    }
}
