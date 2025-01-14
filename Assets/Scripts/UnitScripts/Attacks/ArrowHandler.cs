using UnityEngine;

/// <summary>
/// Should be attached to the Arrow, not the skeleton
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ArrowHandler : MonoBehaviour
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseKnockback;
    public Rigidbody _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void LaunchArrow(Vector3 baseSpeed)
    {
        _rigidBody.linearVelocity = baseSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Can be optimized with tags, but it add dependance beetween teams
        if (collision.gameObject.TryGetComponent<AbstractUnit>(out AbstractUnit unit))
        {
            if (unit is MinecraftUnit)
            {
                MinecraftUnit minecraftUnit = unit as MinecraftUnit;
                Vector3 knockback = _rigidBody.linearVelocity * baseKnockback;
                minecraftUnit.StartCoroutine(minecraftUnit.MovementHandler.TakeImpulse(knockback));
            }
            unit.TakeDamage(baseDamage);
        }
    }
}
