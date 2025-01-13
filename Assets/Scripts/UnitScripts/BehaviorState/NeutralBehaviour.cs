using UnityEngine;

public class NeutralBehaviour : AbstractBehaviour
{
    protected override void MoveAction()
    {
        Unit.MovementHandler.MoveTowardsNearest();
    }

    protected override void AttackAction()
    {
        Unit.AttackHandler.Attack();
    }
}
