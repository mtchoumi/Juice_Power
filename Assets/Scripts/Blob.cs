using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    private GameObject tower;
    Rigidbody2D rb;
    private float damage;
    private Vector3 endpos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        endpos = transform.position;
        if (Vector2.Distance(transform.position, endpos) >= tower.GetComponent<Tower>().lenIs())
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * -5f * Brain.intp;
    }

    public float takeD()
    {
        return damage;
    }

    public void changeD(float d)
    {
      damage = d;
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
