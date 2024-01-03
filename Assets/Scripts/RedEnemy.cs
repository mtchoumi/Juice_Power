using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 3f;
        maxhp = 3f;
        speed = 1;
        damj = 5f;
        intSp = 1;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("waypoints");
		damage = 1;
        str = 1;
        addj = 1f;
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
