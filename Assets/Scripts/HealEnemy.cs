using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemy : Enemy
{
    float time = 0f;
    float interval = 1f;
    private GameObject[] targets;
    // Start is called before the first frame update
   void Start()
    {
        hp = 15f;
        maxhp = 15f;
        speed = 2f;
        intSp = 2f;
        damj = 10f;
        str = 5;
        damage = 7f;
        addj = 10f;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("waypoints");
        targets = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        time += Time.deltaTime;
        if (targets.Length > 0 && time > interval)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].transform.GetComponent<Enemy>().heal();
            }
            time = 0f;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }

    void OnDrawGizmos()
    {
      Gizmos.color = Color.white;
      Gizmos.DrawWireSphere(transform.position, radius);
    }
}
