using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullets : MonoBehaviour
{
	Rigidbody2D rb;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * -5f * Brain.intp;
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Tower")
        {
            Destroy(gameObject);
        }
    }

    public float takeD()
    {
        return damage;
    }
}
