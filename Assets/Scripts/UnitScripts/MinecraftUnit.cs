using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody), typeof(HealthHandler), typeof(AttackHandler))]
[RequireComponent(typeof(MovementHandler), typeof(BaseCapacity))]
public class MinecraftUnit : AbstractUnit
{
    [field: SerializeField] public Rigidbody Body { get; private set; }
    [field: SerializeField] public HealthHandler HealthHandler { get; private set; }
    [field: SerializeField] public AttackHandler AttackHandler { get; private set; }
    [field: SerializeField] public MovementHandler MovementHandler { get; private set; }
    [field: SerializeField] public BaseCapacity Capacity { get; private set; }
    // Not required
    [field: SerializeField] public Animator Animator { get; private set; }
    
    public bool isActive { get; private set; }



    protected override void SetQueen(bool isQueen)
    {
        _isQueen = isQueen;
        transform.Find("Crown").gameObject.SetActive(true);
    }

    new void Awake()
    {
        base.Awake();
        if (IsQueen)
        {
            transform.Find("Crown").gameObject.SetActive(true);
            if (IsTeamA)
            {
                GlobalsVariable.QueenA = this;
            }
            else
            {
                GlobalsVariable.QueenB = this;
            }
        }
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

    public override void AddArmor(float armor)
    {
        HealthHandler.AddArmor(armor);
    }

    public override void RemoveArmor(float armor)
    {
        HealthHandler.RemoveArmor(armor);
    }

    public override void StartFight()
    {
        isActive = true;
        Component[] components = GetComponents<Component>();

        foreach (Component component in components)
        {
            if (component is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.enabled = true;
            }

            if (component is NavMeshAgent agent)
            {
                agent.enabled = true;
            }
        }
    }
    
}
