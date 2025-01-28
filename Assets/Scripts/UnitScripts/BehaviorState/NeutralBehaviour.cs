using System;
using UnityEngine;

public class NeutralBehaviour : AbstractBehaviour
{
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
        AbstractUnit targetUnit = CurrentMinecraftUnit.MovementHandler.TargetUnit;
        if (targetUnit == null)
        {
            return;
        }
        Vector3 targetPos = targetUnit.transform.position;
        Vector3 goalPos = targetPos + (transform.position - targetPos).normalized * distanceGoal;
        CurrentMinecraftUnit.MovementHandler.MoveTowards(goalPos);
    }
}
