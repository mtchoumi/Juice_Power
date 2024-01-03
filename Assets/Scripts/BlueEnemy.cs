 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        addj = 1.5f;
        hp = 5f;
        maxhp = 5f;
        speed = 1.5f;
        intSp = 1.5f;
        damj = 6f;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("waypoints");
		damage = 2;
        str = 2;
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
