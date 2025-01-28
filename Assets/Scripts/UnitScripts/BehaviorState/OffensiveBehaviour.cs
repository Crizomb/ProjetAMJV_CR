using UnityEngine;

public class OffensiveBehaviour : AbstractBehaviour
{
    protected override void MoveAction()
    {
        if (CurrentMinecraftUnit.IsTeamA)
        {
            if (GlobalsVariable.QueenB == null) return;
        }
        else
        {
            if (GlobalsVariable.QueenA == null) return;
        }
        
        CurrentMinecraftUnit.MovementHandler.TargetUnit = GlobalsVariable.QueenB;
        Vector3 targetPos = CurrentMinecraftUnit.MovementHandler.TargetUnit.transform.position;
        Vector3 goalPos = targetPos + (transform.position - targetPos).normalized * distanceGoal;
        CurrentMinecraftUnit.MovementHandler.MoveTowards(goalPos);
    }
    
}
