using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;





public class Tower : MonoBehaviour
{
    private struct Properties
    {
        public float fire_rate;
        public float damage;  // rethink
        public float range;
    }

    // can upgrade
    // can sell at cost*scaller
    // can not move


    List<Properties> properties = new List<Properties>(); // a list that hold the properties of each level

    private float fire_rate; // per second
    private float damage; // damage per shot
    private float range; // radius

    private int level = 0;

    public const int cost = 50;

    //enum targeting_mode = ['first', 'last', 'strongest (default hp)'];



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool RaiseLevel() // will return true or false if it managed to raise the level
    {
        if (level < properties.Count) { 

            level++;//raise the level by 1

            //upgrade the values
            fire_rate = properties[level].fire_rate;
            damage = properties[level].damage;
            range = properties[level].range;
            return true;
        }


        //if the level execceds the max level (3) then return false
        return false;
    }





}
