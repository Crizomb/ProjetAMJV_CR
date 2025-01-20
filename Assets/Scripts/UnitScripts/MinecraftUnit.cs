using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody), typeof(HealthHandler), typeof(AttackHandler))]
[RequireComponent(typeof(MovementHandler))]
public class MinecraftUnit : AbstractUnit
{
    [field: SerializeField] public Rigidbody Body { get; private set; }
    [field: SerializeField] public HealthHandler HealthHandler { get; private set; }
    [field: SerializeField] public AttackHandler AttackHandler { get; private set; }
    [field: SerializeField] public MovementHandler MovementHandler { get; private set; }
    // Not required
    [field: SerializeField] public Animator Animator { get; private set; }
    



    void OnValidate()
    {
    }

    // Abstract implementation for compatibility with other team

    public override void TakeDamage(float damage)
    {
        HealthHandler.TakeDamage(damage);
    }

    public override void Heal(float heal)
    {
        HealthHandler.Heal(heal);
    }
}
