using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceEnemy : Enemy
{
    private float time;
    private float interval;
    // Start is called before the first frame update
    void Start()
    {
        interval = 4f;
        hp = 12f;
        maxhp = 12f;
        speed = 2f;
        intSp = 2f;
        str = 5;
        damage = 7f;
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
            Brain.juice -= Mathf.Min(Brain.juice, 2);
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
