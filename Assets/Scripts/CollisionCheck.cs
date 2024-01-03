using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool isCol = false;
	
	void Update()
    {
		gameObject.GetComponent<BoxCollider2D>().transform.rotation = Quaternion.Euler(0, 0, 0);
        if (isCol)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
	
    private void OnTriggerStay2D(Collider2D col) 
    {
        if (col is BoxCollider2D)
        {
            if (col.gameObject.tag == "Road" || col.gameObject.tag == "Tower")
            {
                isCol = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col) 
    {
        if (col is BoxCollider2D)
        {
            if (col.gameObject.tag == "Road" || col.gameObject.tag == "Tower")
            {
                isCol = false;
            }
        }
    }
}
