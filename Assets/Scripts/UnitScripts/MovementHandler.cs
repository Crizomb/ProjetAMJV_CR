using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MinecraftUnit))]
[RequireComponent(typeof(Rigidbody))]
public class MovementHandler : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool followEnemy = true;
    [SerializeField] private float knockbackTime = 1.2f;
    private float _noNavMeshDeadTime = 6.0f;

    [HideInInspector] public AbstractUnit TargetUnit {get; set; }
    
    private MinecraftUnit _minecraftUnit;
    private Rigidbody _rigidbody;
    

    void Awake()
    {
        _minecraftUnit = GetComponent<MinecraftUnit>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        agent.speed = speed;
    }

    private void Update()
    {
        if (Mathf.Abs(agent.speed - speed) < 0.01f) agent.speed = speed;
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

    public void UpdateNearest()
    {
        TargetUnit = FindNearest(followEnemy);
    }

    public void UpdateNearestFrom(Transform transform)
    {
        TargetUnit = FindNearestFromTransform(true, transform);
    }
    
    
    // If findEnemy, return closest ennemy else return closest ally
    private AbstractUnit FindNearestFromTransform(bool findEnemy, Transform from)
    {
        // Funny funny double ternary operator. 
        List<AbstractUnit> targets = findEnemy ? 
            _minecraftUnit.IsTeamA ? GlobalsVariable.AliveUnitsTeamB : GlobalsVariable.AliveUnitsTeamA
            : _minecraftUnit.IsTeamA ? GlobalsVariable.AliveUnitsTeamA : GlobalsVariable.AliveUnitsTeamB;
        
        AbstractUnit closestUnit = null;
        float closestDistance = float.MaxValue;
        foreach (AbstractUnit target in targets)
        {
            float distanceToEnemy = (target.transform.position - from.position).sqrMagnitude;
            if (distanceToEnemy < closestDistance && target != _minecraftUnit)
            {
                closestUnit = target;
                closestDistance = distanceToEnemy;
            }
        }

        if (closestUnit == null)
        {
            print("What");
            print(targets.Count);
            print(targets);
        }

        return closestUnit;
    }
    
    private AbstractUnit FindNearest(bool findEnemy)
    {
        return FindNearestFromTransform(findEnemy, transform);
    }

    public IEnumerator TakeImpulse(Vector3 impulse)
    {
        // Unity navmesh, can't handle physics (it rewrite velocity). So we deactivate it when applying force.
        agent.enabled = false;
        _rigidbody.AddForce(impulse, ForceMode.Impulse);
        yield return new WaitForSeconds(knockbackTime);

        float noSurfaceTime = 0.0f;
        
        // Make sure to be on the navmesh surface before reactivating agent
        while (_rigidbody != null && !NavMesh.SamplePosition(_rigidbody.position, out _, 1.0f, NavMesh.AllAreas))
        {
            yield return new WaitForSeconds(0.5f);
            noSurfaceTime += 0.5f;
            
            // Die if exited navMesh for to long
            if (noSurfaceTime > _noNavMeshDeadTime)
            {
                _minecraftUnit.HealthHandler.Death();
                yield break;
            }
            
        }
        if (agent != null) agent.enabled = true;
    }
}
