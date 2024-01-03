using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdEnemy : Enemy
{
    // Start is called before the first frame update
    private GameObject target;
    float time = 2f;
    float interval = 2f;
    void Start()
    {
       hp = 14f;
        maxhp = 14f;
        speed = 2f;
        intSp = 2f;
        str = 5;
        damage = 7f;
        damj = 8f;
        addj = 10f;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("waypoints");
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        time += Time.deltaTime;
        if (time >= interval)
        {
            newTarget();
            target.transform.GetComponent<Tower>().chgCold(true);
            time = 0f;
        }
    }

    private void newTarget()
    {
        GameObject[] towers;
        towers = GameObject.FindGameObjectsWithTag("Tower");
        if (towers.Length > 0)
        {
            target = towers[Random.Range(0,towers.Length - 1)];
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
