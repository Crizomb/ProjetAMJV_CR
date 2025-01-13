using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    public float price;
    [field: SerializeField] public bool IsTeamA { get; private set; }
    [field: SerializeField] public bool IsQueen { get; private set; }
    public abstract bool Attack();
    public abstract void TakeDamage(float damage);
    
}
