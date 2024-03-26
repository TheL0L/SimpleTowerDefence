using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Tower : MonoBehaviour
{  // can be upgraded, sold at (cost * scalar). can not be moved
    public enum TargetingMode {FIRST, LAST, STRONGEST, WEAKEST};  // set of tower's targeting modes
    [System.Serializable] public struct TowerProperties
    {  // a struct for storing tower's properties, useful for leveling up mechanics
        public float firerate_APS;  // firerate value as attacks per second
        public float damage;  // rethink, maybe shoot a 'projectile' that will deal damage on impact?
        public float range;
    }

    [SerializeField]
    public TowerProperties[] properties;  // a list that hold the properties of each level
    public int level = 0;
    public const int cost = 50;

    private float cooldown_timer = 0.0f;  // a cooldown timer for attacking
    public TargetingMode targeting_mode = TargetingMode.FIRST;
    public GameObject current_target = null;

    // Update is called once per frame
    void Update()
    {
        cooldown_timer -= Time.deltaTime;  // progress the cooldown timer

        // search for a valid target, then attempt hitting it.
        current_target = FindTargetInRange();
        if (current_target != null)
        {
            AttactTarget(current_target);
        }
    }

    private GameObject FindTargetInRange()
    {
        // get an array of all currently existing enemies
        Enemy[] possible_targets = FindObjectsOfType<Enemy>();
        List<Enemy> targets_inrange = new List<Enemy>();

        foreach (var t in possible_targets)
        {
            // filter out any enemies that are out of reach (out of range, maybe camo/flying enemies?)
            if (Vector2.Distance(this.transform.position, t.transform.position) < this.properties[level].range)
            {
                targets_inrange.Add(t);
            }
        }

        // if there are no targets inrange, return null
        if (targets_inrange.Count == 0)
        {
            return null;
        }

        // reduce the list of targets to a single target
        Enemy target = null;
        switch (this.targeting_mode)
        {
            case TargetingMode.FIRST:
            {
                float threshold = targets_inrange.Max(t => t.GetNextWaypointID());
                targets_inrange = targets_inrange.Where(t => t.GetNextWaypointID() >= threshold).ToList();
                target = targets_inrange.OrderByDescending(t => t.GetDistanceTowardsNextWaypoint()).FirstOrDefault();
                break;
            }
            case TargetingMode.LAST:
            {
                float threshold = targets_inrange.Min(t => t.GetNextWaypointID());
                targets_inrange = targets_inrange.Where(t => t.GetNextWaypointID() <= threshold).ToList();
                target = targets_inrange.OrderBy(t => t.GetDistanceTowardsNextWaypoint()).FirstOrDefault();
                break;
            }
            case TargetingMode.STRONGEST:
            {
                target = targets_inrange.OrderByDescending(t => t.GetMaxHealth()).FirstOrDefault();
                break;
            }
            case TargetingMode.WEAKEST:
            {
                target = targets_inrange.OrderBy(t => t.GetMaxHealth()).FirstOrDefault();
                break;
            }
        }

        // return the selected target
        return target.gameObject;
    }

    private void AttactTarget(GameObject target)
    {
        // rotate towards the target
        Vector2 diff = target.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Euler(0, 0,
            Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90f);
        
        // check whether tower is still on cooldown since last attack
        if (cooldown_timer > 0.0f)
        {
            return;
        }

        // shoot a projectile towards the target?
        // deal damage to the target
        target.GetComponent<Enemy>().takeDamage(this.properties[level].damage);

        cooldown_timer = 1 / this.properties[level].firerate_APS;  // apply cooldown
    }

    bool RaiseLevel() // will return true or false if it managed to raise the level
    {
        if (level < properties.Length)
        {
            level++;
            return true;
        }

        //if the level execceds the max level then return false
        return false;
    }

    public void Sell()
    {
        // draw selling animation/effects?
        // refund the player, should this logic be kept at ShopManager? most definitely!
    }
}
