using UnityEngine;

public class WitchSummon : BaseCapacity
{
   [SerializeField] private GameObject summonUnit;

   protected override bool CapacityCall()
   {
      GameObject unit = Instantiate(summonUnit, transform.position, Quaternion.identity);
      AbstractUnit unitScript = unit.GetComponent<AbstractUnit>();
      unitScript.StartFight();
      return true;
   }
}
