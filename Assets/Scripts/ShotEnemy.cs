using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : Enemy
{
    [SerializeField] private GameObject shot;
    private Transform target = null;
    private float angle;
    private float time;
    private float interval;
    GameObject EBullet;
    // Start is called before the first frame update
    void Start()
    {
        interval = 2f;
        hp = 10f;
        maxhp = 10f;
        speed = 2f;
        intSp = 2f;
        str = 5;
        damage = 7f;
        addj = 10f;
        damj = 10f;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("waypoints");
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius, layer);
        time += Time.deltaTime;
        if (hits.Length == 0)
        {
            target = null;
        }
        else
        {
            target = newtarget();
            angle = -Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg;
        }
        if (hits.Length > 0 && time >= interval)
        {
            EBullet = Instantiate(shot, gameObject.transform.position, Quaternion.Euler(0,0,angle));
            EBullet.name = "eshot";
            time = 0f;
        }
    }

    public Transform newtarget()
    {
        bool inCast = false;
        int tvalue = 0;
        if (target == null)
        {
            return hits[tvalue].transform;
        }
        else
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform == target)
                {
                    inCast = true;
                }
            }
            if (inCast == true)
            {
                return target;
            }
            else
            {
                return hits[tvalue].transform; 
            }
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
