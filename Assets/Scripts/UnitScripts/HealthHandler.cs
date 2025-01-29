using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MinecraftUnit))]
public class HealthHandler : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth{ get; private set; }
    [field: SerializeField] public float CurrentHealth{ get; private set; }
    [field: SerializeField] public float Armor{ get; private set; }
    
    private MinecraftUnit _minecraftUnit;

    public void Awake()
    {
        _minecraftUnit = GetComponent<MinecraftUnit>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Assert(damage >= 0, "Damage cannot be negative, use Heal if you want to heal");
        CurrentHealth -= Mathf.Max(0, damage-Armor);
        _minecraftUnit.Capacity.AddMana(damage);
        if (CurrentHealth <= 0) Death();
    }

    public void Heal(float value)
    {
        Debug.Assert(value >= 0, "value can't be less than zero");
        CurrentHealth = Mathf.Min(CurrentHealth + value, MaxHealth);
    }

    public void AddArmor(float armorBoost)
    {
        Debug.Assert(armorBoost >= 0, "armorBoost can't be less than zero, use RemoveArmor instead");
        Armor += armorBoost;
    }

    public void RemoveArmor(float armorBoost)
    {
        Debug.Assert(armorBoost >= 0, "armorBoost can't be less than zero, use AddArmor instead");
        Armor -= armorBoost;
    }

    public void Death(float delay = 0)
    {
        DeathSate deathState = _minecraftUnit.AbstractDeath();

        if (deathState == DeathSate.NotImportant)
        {
            Destroy(gameObject, delay);
            return;
        }
        
        if (deathState == DeathSate.QueenBDead)
        {
            print("get good, reload current scene");
            GameManager.Instance.ReloadLevel();
        }

        if (deathState == DeathSate.QueenADead)
        {
            print("GG going to next scene");
            GameManager.Instance.GoNextLevel();
        }
    }
    
}
