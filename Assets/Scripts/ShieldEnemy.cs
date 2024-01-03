using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 5f;
        maxhp = 5f;
        speed = 2;
        intSp = 2;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("waypoints");
		damage = 3;
        str = 4;
        addj = 3f;
        resis = 0.25f;
        damj = 7f;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
	
	protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "boomrang")
        {
            resis = 1f;
        }
        base.OnTriggerEnter2D(col);
    }
}
