using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class AttackHandler : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private Collider attackShape;
    [SerializeField] private float knockback;
    
    private float _timer;
    private Unit _unit;

    void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    void Start()
    {
        _timer = cooldown;
    }

    void Update()
    {
        _timer = _timer - Time.deltaTime;
        Attack();
    }
    
    /// <summary>
    /// Launch an Attack, and return true if it's possible to attack
    /// see what Units are in the attackShape, apply damage and knockback to those unit if they're ennemies
    /// </summary>
    public bool Attack()
    {
        if (_timer > 0) return false;
        
        Collider[] targets = DetectTargets();
        print(targets.Length);
        foreach (Collider target in targets)
        {
            if (!target.CompareTag("Unit")) continue;
            // GetComponent is expensive in performance, optimize here if it's slow
            Unit unit = target.GetComponent<Unit>();
            
            // No friendly fire
            if (unit.IsTeamA == _unit.IsTeamA) continue;
            
            unit.Health.TakeDamage(damage);
            Vector3 knockbackVector = knockback * (target.transform.position - transform.position).normalized;
            unit.Body.AddForce(knockbackVector, ForceMode.Impulse);

        }
        _timer = cooldown;
        return true;
    }

    private Collider[] DetectTargets()
    {
        // Make sure to manager layers for better performance
        Collider[] hitColliders;
    
        switch (attackShape)
        {
            case SphereCollider sphere:
                hitColliders = Physics.OverlapSphere(sphere.transform.position, sphere.radius);
                break;
            case BoxCollider box:
                hitColliders = Physics.OverlapBox(box.bounds.center, box.bounds.extents, box.transform.rotation);
                break;
            default:
                throw new ArgumentException("Only sphere or box are supported");
        }

        return hitColliders;
        
    }
    
     
}
