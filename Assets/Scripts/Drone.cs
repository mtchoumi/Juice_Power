using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private Transform tower;
    // Start is called before the first frame update
    void Start()
    {
        tower = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(Mathf.Sin(tower.GetComponent<JuiceTower>().getAng() *  Mathf.Deg2Rad), 1f);
        if (Mathf.Cos(tower.GetComponent<JuiceTower>().getAng() *  Mathf.Deg2Rad) > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
}
