using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotEnemy : Enemy
{
  // Start is called before the first frame update
  void Start()
  {
    hp = 0.1f;
    maxhp = 0.1f;
    resis = 0;
    damj = 6f;
    speed = 1.75f;
    intSp = 1.75f;
    rb = GetComponent<Rigidbody2D>();
    waypoints = GameObject.Find("waypoints");
  }

  // Update is called once per frame
  protected override void FixedUpdate()
  {
    base.FixedUpdate();
    hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius, layer);
    if (hits.Length > 0)
    {
      for (int i = 0; i < hits.Length; i++)
      {
        if (!hits[i].transform.GetComponent<Tower>().isRoted())
        {
          hits[i].transform.GetComponent<Tower>().chgRE(1.75f);
        }
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
