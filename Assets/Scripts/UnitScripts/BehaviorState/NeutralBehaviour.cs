using UnityEngine;

public class NeutralBehaviour : AbstractBehaviour
{
    [SerializeField] private float distanceGoal = 0.0f;
    protected override void MoveAction()
    {
        if (CurrentMinecraftUnit.IsTeamA)
        {
            if (GlobalsVariable.AliveUnitsTeamB.Count == 0) return;
        }
        else
        {
            if (GlobalsVariable.AliveUnitsTeamA.Count == 0) return;
        }
        
        CurrentMinecraftUnit.MovementHandler.UpdateNearest();
        Vector3 targetPos = CurrentMinecraftUnit.MovementHandler.TargetUnit.transform.position;
        Vector3 goalPos = targetPos + (transform.position - targetPos).normalized * distanceGoal;
        CurrentMinecraftUnit.MovementHandler.MoveTowards(goalPos);
    }

    protected override void AttackAction()
    {
        //CurrentMinecraftUnit.AttackHandler.Attack();
    }
}
