using UnityEngine;

public class AttackSkeleton : AttackHandler
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowBaseSpeed;
    [SerializeField] private Transform spawnPos;
    public override bool Attack()
    {
        if (_timer > 0) return false;
        
        GameObject arrow = Instantiate(arrowPrefab, spawnPos.position, spawnPos.rotation);
        ArrowHandler arrowHandler = arrow.GetComponent<ArrowHandler>();
        arrowHandler.LaunchArrow(arrowBaseSpeed * spawnPos.forward);
        
        return true;
    }
}
