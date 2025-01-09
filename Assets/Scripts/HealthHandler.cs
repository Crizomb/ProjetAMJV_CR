using UnityEngine;

[RequireComponent(typeof(Unit))]
public class HealthHandler : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float armor;
    
    private Unit _unit;

    public void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Assert(damage >= 0, "Damage cannot be negative, use Heal if you want to heal");
        currentHealth -= Mathf.Max(0, damage-armor);
        if (currentHealth <= 0) Death();
    }

    public void Heal(float value)
    {
        Debug.Assert(value >= 0, "value can't be less than zero");
        currentHealth = Mathf.Min(currentHealth + value, maxHealth);
    }

    public float GetArmor()
    {
        return armor;
    }

    public void EquipArmor(float armorBoost)
    {
        Debug.Assert(armorBoost >= 0, "armorBoost can't be less than zero, use UnEquipArmor instead");
        armor += armorBoost;
    }

    public void UnEquipArmor(float armorBoost)
    {
        Debug.Assert(armorBoost >= 0, "armorBoost can't be less than zero, use EquipArmor instead");
        armor -= armorBoost;
    }

    public void Death()
    {
        print("you dead");
        if (_unit.IsTeamA)
        {
            GlobalsVariable.AliveUnitsTeamA.Remove(_unit);
        }
        else
        {
            GlobalsVariable.AliveUnitsTeamB.Remove(_unit);
        }
        
        Destroy(gameObject);
    }
    
}
