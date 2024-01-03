using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 3.5f;
        maxhp = 3.5f;
        addj = 2f;
        speed = 1.25f;
        intSp = 1.25f;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("skipwaypoints");
		damage = 1.5f;
        str = 3;
        damj = 6f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
	
	protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
}
