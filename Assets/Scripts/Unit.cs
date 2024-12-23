using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour
{
    [field: SerializeField] public Rigidbody Body { get; private set; }
    [field: SerializeField] public HealthHandler Health { get; private set; }
    [field: SerializeField] public AttackHandler Attack { get; private set; }
    [field: SerializeField] public MovementHandler Move { get; private set; }

    [field: SerializeField] public bool IsTeamA { get; private set; }

    [SerializeField] private int price;

    

    void OnValidate()
    {
        Debug.Assert(Body != null);   
        Debug.Assert(Health != null);
        Debug.Assert(Attack != null);
        Debug.Assert(Move != null);
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
    
    
}
