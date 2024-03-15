using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public struct EnemyGroup
    {
        GameObject unit;
        uint count;
    }

    public int bonus_gold = 500;
    public List<EnemyGroup> units = new List<EnemyGroup>();
}
