using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class MinecraftUnit : AbstractUnit
{
    [field: SerializeField] public Rigidbody Body { get; private set; }
    [field: SerializeField] public HealthHandler HealthHandler { get; private set; }
    [field: SerializeField] public AttackHandler AttackHandler { get; private set; }
    [field: SerializeField] public MovementHandler MovementHandler { get; private set; }

    

    void OnValidate()
    {
        Debug.Assert(Body != null);   
        Debug.Assert(HealthHandler != null);
        Debug.Assert(AttackHandler != null);
        Debug.Assert(MovementHandler != null);
    }

    void Awake()
    {
        
        if (IsTeamA)
        {
            GlobalsVariable.AliveUnitsTeamA.Add(this);
        }
        else
        {
            GlobalsVariable.AliveUnitsTeamB.Add(this);
        }
    }
    
    // Abstract implementation for compatibility with other team

    public override bool Attack()
    {
        return AttackHandler.Attack();
    }

    public override void TakeDamage(float damage)
    {
        HealthHandler.TakeDamage(damage);
    }
}
