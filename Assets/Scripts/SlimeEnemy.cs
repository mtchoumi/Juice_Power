using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : Enemy
{
    int num = 0;
    bool spawn = false;
    // Start is called before the first frame update
    void Start()
    {
        hp = 7f - num;
        maxhp = 7f - num;
        speed = 0.8f;
        intSp = 0.8f;
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.Find("waypoints");
        damage = 5f - num;
        str = 4;
        addj = 3f;
        damj = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            if (!spawn && num < 3)
            {
                GameObject NewSplits1 = Instantiate(gameObject, new Vector2(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y), gameObject.transform.rotation);
                NewSplits1.transform.localScale = new Vector2(transform.localScale.x * 0.5f,transform.localScale.y * 0.5f);
                NewSplits1.GetComponent<SlimeEnemy>().chgNum();
                NewSplits1.GetComponent<SlimeEnemy>().chgI(i);
                NewSplits1.name = "Circle";
                GameObject NewSplits2 = Instantiate(gameObject, new Vector2(gameObject.transform.position.x + 0.25f, gameObject.transform.position.y), gameObject.transform.rotation);
                NewSplits2.transform.localScale = new Vector2(transform.localScale.x * 0.5f,transform.localScale.y * 0.5f);
                NewSplits2.GetComponent<SlimeEnemy>().chgNum();
                NewSplits2.GetComponent<SlimeEnemy>().chgI(i);
                NewSplits2.name = "Circle";
                spawn = true;
            }
        }
    }

    void chgNum()
    {
        num++;
    }

    void chgI(int x)
    {
        i = x;
    }
}
