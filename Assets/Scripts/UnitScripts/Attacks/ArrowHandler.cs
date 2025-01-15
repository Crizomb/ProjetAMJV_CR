using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Should be attached to the Arrow, not the skeleton
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ArrowHandler : MonoBehaviour
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseKnockback;
    private Rigidbody _rigidBody;
    private bool _fromTeamA;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Align with speed
        if (_rigidBody.linearVelocity.magnitude >= 1f) transform.forward = _rigidBody.linearVelocity.normalized;
    }

    public void LaunchArrow(Vector3 baseSpeed, bool fromTeamA)
    {
        _rigidBody.linearVelocity = baseSpeed;
        _fromTeamA = fromTeamA;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Can be optimized with tags, but it add dependance beetween teams
        if (collision.gameObject.TryGetComponent<AbstractUnit>(out AbstractUnit unit))
        {
            if (unit is MinecraftUnit && unit.IsTeamA != _fromTeamA) // No friendly fire
            {
                MinecraftUnit minecraftUnit = unit as MinecraftUnit;
                Vector3 knockback = _rigidBody.linearVelocity * baseKnockback;
                minecraftUnit.StartCoroutine(minecraftUnit.MovementHandler.TakeImpulse(knockback));
            }
            unit.TakeDamage(baseDamage);
        }
        
        Destroy(this.gameObject);
    }
}
