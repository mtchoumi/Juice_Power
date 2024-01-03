using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameObject waypoints;
    protected Rigidbody2D rb;
    protected float speed = 0f;
    protected float intSp = 0f;
    protected int i = 0;
	protected float hp = 0f;
    protected float maxhp = 0f;
	protected float damage = 0;
    protected int str = 0;
    protected float addj = 1;
    protected float damj = 0f;
    protected float resis = 1;
    protected RaycastHit2D[] hits;
    [SerializeField] protected float radius;
    [SerializeField] protected LayerMask layer;
    [SerializeField] private Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        transform.GetChild(0).transform.eulerAngles = waypoints.gameObject.transform.GetChild(i).eulerAngles;
        rb.velocity = transform.GetChild(0).up * speed * Brain.intp;
        if (transform.GetChild(0).transform.eulerAngles.z == 270)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (transform.GetChild(0).transform.eulerAngles.z == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else if (transform.GetChild(0).transform.eulerAngles.z == 90)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        else if (transform.GetChild(0).transform.eulerAngles.z == 180)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        if (Vector2.Distance(transform.position, waypoints.gameObject.transform.GetChild(i).position) <= 0.05f)
        {
            if (i == waypoints.gameObject.transform.childCount - 1)
            {
                Brain.basehp -= damage;
                Brain.juice -= Mathf.Min(Brain.juice, damj * Brain.numD);
                Brain.numD++;
                Destroy(gameObject);
            }
            else 
            {
                i++;
            }
        }
		if (hp <= 0)
		{
            Brain.juice += Juice.maxjuice - Mathf.Max(Brain.juice, Juice.maxjuice - addj);
			Destroy(gameObject);
		}
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "shot")
        {
            hp -= col.GetComponent<Bullets>().takeD() * resis;
        }
        else if (col.gameObject.name == "boomrang")
        {
            hp -= col.GetComponent<Boomrang>().takeD() * resis;
        }
        else if (col.gameObject.name == "staff")
        {
            hp -= col.GetComponent<Staff>().takeD() * resis;
        }
        else if (col.gameObject.name == "blob")
        {
            hp -= col.GetComponent<Blob>().takeD() * resis;
            addj *= 1.5f;
        }
    }

    public int strValue()
    {
        return str;
    }

    public void changeS(float f)
    {
        speed = intSp * f;
    }

    public void heal()
    {
        hp = maxhp - Mathf.Max(hp, maxhp - 1.5f);
    }
}
