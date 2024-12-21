using Unity.VisualScripting;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [SerializeField] public HealthHandler healthHandler;
    [SerializeField] public AttackHandler attackHandler;
    

    void Start()
    {
        // Null safety enjoyers things 
        Debug.Assert(healthHandler != null);
        Debug.Assert(attackHandler != null);

    }
    
    
}
