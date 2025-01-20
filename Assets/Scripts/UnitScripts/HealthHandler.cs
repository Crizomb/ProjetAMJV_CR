using UnityEngine;

[RequireComponent(typeof(MinecraftUnit))]
public class HealthHandler : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float armor;
    
    private MinecraftUnit _minecraftUnit;

    public void Awake()
    {
        _minecraftUnit = GetComponent<MinecraftUnit>();
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

    public void Death(float delay = 0)
    {
        DeathSate deathState = _minecraftUnit.AbstractDeath();
        if (deathState == DeathSate.QueenADead) print("TEAM B WIN GG");
        if (deathState == DeathSate.QueenBDead) print("TEAM A WIN GG");
        Destroy(gameObject, delay);
    }
    
}
