using UnityEngine;
using System.Collections.Generic;


public static class ArmyManager
{
    private static List<GameObject> enemyArmy = new List<GameObject>();
    private static List<GameObject> playerArmy = new List<GameObject>();
    private static GameObject enemyCrownDuck = null;
    private static GameObject playerCrownDuck = null;


    public static List<AbstractUnit> getArmy(bool isEnemy)
    {
        List<AbstractUnit> units = (isEnemy ? GlobalsVariable.AliveUnitsTeamB : GlobalsVariable.AliveUnitsTeamA);
        return units;
    }


    public static GameObject getCrownDuck(bool isEnemy)
    {
        return(isEnemy ? GlobalsVariable.QueenB.gameObject : GlobalsVariable.QueenA.gameObject);
    }
    
    
}
