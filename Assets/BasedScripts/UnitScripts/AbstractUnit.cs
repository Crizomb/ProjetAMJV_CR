using UnityEngine;

public enum DeathSate
{
    NotImportant = 0,
    QueenADead = 1,
    QueenBDead = 2,
    
}
// For compatibility with the other team units, only contains things that need to be in common
public abstract class AbstractUnit : MonoBehaviour
{
    public float price;
    [field: SerializeField] public bool IsTeamA { get; private set; }

    [field: SerializeField]
    protected bool _isQueen;

    public virtual bool IsQueen
    {
        get => _isQueen;
        set => SetQueen(value);
    }

    protected virtual void SetQueen(bool isQueen)
    {
        _isQueen = isQueen;
    }
    
    public abstract void TakeDamage(float damage);
    public abstract void Heal(float heal);
    public abstract void AddArmor(float armor);
    public abstract void RemoveArmor(float armor);
    public abstract void StartFight();
    
     protected void Awake()
    {
        
        if (IsTeamA)
        {
            GlobalsVariable.AliveUnitsTeamA.Add(this);
            if (IsQueen) GlobalsVariable.QueenA = this;
        }
        else
        {
            GlobalsVariable.AliveUnitsTeamB.Add(this);
            if (IsQueen) GlobalsVariable.QueenB = this;
        }
    }

    public DeathSate AbstractDeath()
    {
        if (IsTeamA)
        {
            GlobalsVariable.AliveUnitsTeamA.Remove(this);
            if (IsQueen) return DeathSate.QueenADead;
        }
        else
        {
            GlobalsVariable.AliveUnitsTeamB.Remove(this);
            if (IsQueen) return DeathSate.QueenBDead;
        }
        return DeathSate.NotImportant;
    }
    
}
