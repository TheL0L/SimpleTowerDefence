using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private float health = 6.0f;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private const int reward_value = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        moveTowards(target.position);
        if (!isAlive())
        {
            Destroy(this.gameObject);
        }

    }

    private void moveTowards(Vector3 target)
    {

        Vector3 direction = (target - transform.position).normalized;
        transform.position = transform.position + direction * Time.deltaTime;

    }

    public bool isAlive()
    {
        return health > 0;
    }


    public void takeDamage(float damage)
    {
        health -= damage;
    }

    public void OnDestroy()
    {
        FindObjectOfType<GameManager>().addGold(reward_value);
    }





}
