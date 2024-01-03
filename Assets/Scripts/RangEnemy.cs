using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 13f;
        maxhp = 13f;
        speed = 2f;
        intSp = 2f;
        damj = 8f;
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
    }
	
	protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
}
