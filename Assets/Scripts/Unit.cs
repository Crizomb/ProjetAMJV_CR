using Unity.VisualScripting;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [field: SerializeField] public Rigidbody Body { get; private set; }
    [field: SerializeField] public HealthHandler Health { get; private set; }
    [field: SerializeField] public AttackHandler Attack { get; private set; }
    

    void Start()
    {
        Debug.Assert(Body != null);   
        Debug.Assert(Health != null);
        Debug.Assert(Attack != null);
    }
    
    
}
