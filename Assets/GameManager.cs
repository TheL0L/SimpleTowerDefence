using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int health = 30;
    [SerializeField]
    private int gold = 100;

    private int wave_index = -1;

    [SerializeField]
    private List<Vector2> path_points = new List<Vector2>();
    [SerializeField]
    private List<Wave> waves = new List<Wave>();

    public List<GameObject> towers = new List<GameObject>();

    public void Start()
    {

    }

    public void Update()
    {
        
    }

    public List<Vector2> getPath()
    {
        return new List<Vector2>(path_points);
    }

    public bool isAlive()
    {
        return health > 0;
    }

    public void addGold(int gold)
    {
        if (gold > 0) { this.gold += gold; }
    }

    public int getGold()
    {
        return gold;
    }

    public bool removeGold(int gold)
    {
        if (gold < 0) { return false; }
        if (this.gold < gold) { return false; }

        this.gold -= gold;
        return true;
    }
}
