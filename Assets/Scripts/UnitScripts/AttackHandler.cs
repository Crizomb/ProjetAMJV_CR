using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MinecraftUnit))]
public class AttackHandler : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private Collider attackShape;
    [SerializeField] private float knockbackHorizontalForce;
    [SerializeField] private float knockbackVerticalForce;
    
    private float _timer;
    private MinecraftUnit _minecraftUnit;

    void Awake()
    {
        _minecraftUnit = GetComponent<MinecraftUnit>();
    }

    void Start()
    {
        // Random to avoid too much synchronicity 
        _timer = cooldown + Random.Range(-cooldown*0.2f, cooldown*0.2f);
    }

    void Update()
    {
        _timer = _timer - Time.deltaTime;
    }
    
    /// <summary>
    /// Launch an Attack, and return true if it's possible to attack
    /// see what Units are in the attackShape, apply damage and knockback to those unit if they're ennemies
    /// </summary>
    public bool Attack()
    {
        if (_timer > 0) return false;
        
        Collider[] targets = DetectTargets();
        foreach (Collider target in targets)
        {
            if (!target.CompareTag("Unit")) continue;
            // GetComponent is expensive in performance, optimize here if it's slow
            AbstractUnit targetUnit = target.GetComponent<AbstractUnit>();
            
            // No friendly fire
            if (targetUnit.IsTeamA == _minecraftUnit.IsTeamA) continue;
            
            targetUnit.TakeDamage(damage);
            
            Vector3 knockbackVector = knockbackHorizontalForce * (target.transform.position - transform.position).normalized 
                                      + knockbackVerticalForce *  Vector3.up;
            
            // Knockback logic specific to MinecraftUnit (can't force other team to do our weird impl)
            if (targetUnit is MinecraftUnit)
            {
                MinecraftUnit minecraftTarget = (MinecraftUnit)targetUnit;
                minecraftTarget.StartCoroutine(minecraftTarget.MovementHandler.TakeImpulse(knockbackVector));
            }
        }
        _timer = cooldown + Random.Range(-cooldown*0.2f, cooldown*0.2f);
        return true;
    }

    private Collider[] DetectTargets()
    {
        Collider[] hitColliders;
    
        switch (attackShape)
        {
            case SphereCollider sphere:
                hitColliders = Physics.OverlapSphere(sphere.transform.position, sphere.radius, sphere.includeLayers);
                break;
            case BoxCollider box:
                hitColliders = Physics.OverlapBox(box.bounds.center, box.bounds.extents, box.transform.rotation, box.includeLayers);
                break;
            default:
                throw new ArgumentException("Only sphere or box are supported");
        }

        return hitColliders;
        
    }
    
     
}
