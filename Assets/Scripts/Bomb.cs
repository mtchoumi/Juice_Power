using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rb;
    private float damage;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = -5f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Bomb")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public float takeD()
    {
        return damage;
    }

    public void changeD(float d)
    {
      damage = d;
    }

    void OnDestroy()
    {
    }
}
