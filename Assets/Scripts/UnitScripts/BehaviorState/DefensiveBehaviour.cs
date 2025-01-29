using UnityEngine;

public class DefensiveBehaviour : AbstractBehaviour
{
    protected override void MoveAction()
    {
        if (CurrentMinecraftUnit.IsTeamA)
        {
            if (GlobalsVariable.QueenA != null) return;
            CurrentMinecraftUnit.MovementHandler.UpdateNearestFrom(GlobalsVariable.QueenA.transform);
        }
        else
        {
            if (GlobalsVariable.QueenB != null) return;
            CurrentMinecraftUnit.MovementHandler.UpdateNearestFrom(GlobalsVariable.QueenB.transform);
        }
        
        Vector3 targetPos = CurrentMinecraftUnit.MovementHandler.TargetUnit.transform.position;
        Vector3 goalPos = targetPos + (transform.position - targetPos).normalized * distanceGoal;
        CurrentMinecraftUnit.MovementHandler.MoveTowards(goalPos);
    }

}
