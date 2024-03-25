using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Vector3 target;

    private List<Vector2> path;

    GameManager manager;

    Tower tower;

    [SerializeField]
    private float health = 6.0f;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private const int reward_value = 10;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        path = manager.getPath();
        target = path.First();
        path.Remove(target);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        tower = FindObjectOfType<Tower>();
        if (getDistance(this.gameObject.transform.position, tower.gameObject.transform.position) < 3)
        {
            takeDamage(0.5f);
        }
        
        if (!isAlive())
        {
            Destroy(this.gameObject);
        }

    }

    private void move()
    {
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            if (path.Count > 0)
            {
                target = path.First();
                path.Remove(target);
            } 
            else
            {
                manager.takeDamage((int)health);//change this
                Destroy(this.gameObject);
                return;
            }
        }
        moveTowards(target);

    }

    private void moveTowards(Vector3 target)
    {

        Vector3 direction = (target - transform.position);
        direction.z = 0;
        transform.position = transform.position + direction.normalized * Time.deltaTime * speed;

    }

    public bool isAlive()
    {
        return health > 0;
    }


    public void takeDamage(float damage)
    {
        health -= damage;
    }


    public float getDistance(Vector2 _enemy, Vector2 _tower) //Calculates the distance between two coordinates
    {
        return Vector2.Distance(_enemy, _tower);
    }


    public void OnDestroy()
    {
        manager.addGold(reward_value);//fix later
    }





}
