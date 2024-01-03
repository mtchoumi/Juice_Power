using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomrang : MonoBehaviour
{
    private GameObject tower;
    Rigidbody2D rb;
    float speed;
    float back;
    private float damage;

    private Vector3 endpos, newvelo;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = -3.5f;
        back = 1f;
        endpos = transform.position;
        newvelo =  transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = newvelo * back * Brain.intp;
        transform.eulerAngles += new Vector3(0f,0f,1f) * Time.deltaTime * 720f;
        if (Vector2.Distance(transform.position, endpos) >= tower.GetComponent<Tower>().lenIs())
        {
            back = -1;
        }
        else if (Vector2.Distance( transform.position, endpos) <= 0.1f && back == -1)
        {
            Destroy(gameObject);
        }
    }

    public void setTower(GameObject gO)
    {
        tower = gO;
    }

    public float takeD()
    {
        return damage;
    }

    public void changeD(float d)
    {
      damage = d;
    }
}
