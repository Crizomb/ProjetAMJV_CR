using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Rigidbody))]
public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform defaultMoveTarget;
    
    private Unit _unit;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _unit = GetComponent<Unit>();
        _rigidbody = GetComponent<Rigidbody>();
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
        if (agent.enabled) agent.SetDestination(destination);
    }

    public void MoveTowardsNearest()
    {
        MoveTowards(FindNearestEnemy().transform.position);
    }

    Unit FindNearestEnemy()
    {
        List<Unit> enemies = _unit.IsTeamA ? GlobalsVariable.AliveUnitsTeamB : GlobalsVariable.AliveUnitsTeamA;
        
        Unit closestUnit = null;
        float closestDistance = float.MaxValue;
        foreach (Unit enemy in enemies)
        {
            float distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < closestDistance)
            {
                closestUnit = enemy;
                closestDistance = distanceToEnemy;
            }
        }

        return closestUnit;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //MoveTowards(defaultMoveTarget.position);
            MoveTowardsNearest();
        }
    }

    public IEnumerator TakeImpulse(Vector3 impulse)
    {
        // Unity navmesh, can't handle physics (it rewrite velocity). So we deactivate it when applying force.
        agent.enabled = false;
        _rigidbody.AddForce(impulse, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);

        float noSurfaceTime = 0.0f;
        
        // Make sure to be on the navmesh surface before reactivating agent
        while (_rigidbody != null && !NavMesh.SamplePosition(_rigidbody.position, out _, 1.0f, NavMesh.AllAreas))
        {
            yield return new WaitForSeconds(0.5f);
            noSurfaceTime += 0.5f;
            
            // Die if exited navMesh for to long
            if (noSurfaceTime > 6.0f)
            {
                _unit.Health.Death();
                yield break;
            }
            
        }
        if (agent != null) agent.enabled = true;
    }
}
