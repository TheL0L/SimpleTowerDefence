using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2[] path;
    private int next_waypoint_index = 0;
    private Vector2 next_waypoint;

    private GameManager manager;

    [SerializeField]
    private float health = 6.0f;
    private float max_health;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private const int reward_value = 10;

    // Start is called before the first frame update
    void Start()
    {
        max_health = health;  // snapshot the starting health value as max health

        manager = FindObjectOfType<GameManager>();
        path = manager.getPath();
        next_waypoint = path[next_waypoint_index];
    }

    // Update is called once per frame
    void Update()
    {
        move();
        
        if (!isAlive())
        {
            manager.addGold(reward_value);  // give reward before death
            OnDeath();  // do any last biddings of the dying enemy
            Destroy(this.gameObject);
        }
    }

    private void move()
    {
        if (Vector2.Distance(transform.position, next_waypoint) < 0.1f)
        {
            if (next_waypoint_index < path.Length)
            {
                next_waypoint = path[next_waypoint_index];
                next_waypoint_index++;
            } 
            else
            {
                CommitSudoku();
                return;
            }
        }

        float step_size = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, next_waypoint, step_size);
    }

    public bool isAlive()
    {
        return health > 0;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    private void CommitSudoku()
    {
        // the enemy has reached the player base, and does the kamikadze on it
        // no rewards or animations should be played

        manager.takeDamage(Mathf.CeilToInt(health));
        Destroy(this.gameObject);
    }

    public float GetMaxHealth()
    {
        return max_health;
    }

    public int GetNextWaypointID()
    {
        return next_waypoint_index;
    }

    public float GetDistanceTowardsNextWaypoint()
    {
        return Vector2.Distance(this.transform.position, next_waypoint);
    }

    private void OnDeath()  // unlike OnDestroy(), isn't called upon object's destruction
    {
        // draw some death animation?
        // spawn children?
        // apply AOE at death location?
    }
}
