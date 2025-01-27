using System;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalsVariable
{

    public static int money=100;

    public static List<AbstractUnit> AliveUnitsTeamA = new List<AbstractUnit>();
    public static List<AbstractUnit> AliveUnitsTeamB = new List<AbstractUnit>();

    public static AbstractUnit QueenA;
    public static AbstractUnit QueenB;

    public static Dictionary<string, int> prices = new Dictionary<string, int>()
        {
            { "Zombie",1 },
            { "Squelette",2 },
            { "Creeper",3 },
            { "Sorci�re",3 },
            { "Golem",8 }
        };

    public static void Pay(int X)
    {
        money -= X;
    }

    public static void Gain(int Y) 
    {
        money += Y;
    }
}
