using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTower : MonoBehaviour
{
    [SerializeField] private GameObject seed1, tower, tableInfo, rightside;
    public static bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        rightside = GameObject.Find("RightSide");
    }

    // Update is called once per frame
    void Update()
    {
        if (Brain.juice >= Brain.Towers[tower.name][0])
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    void OnMouseDown() 
    {
        if (done == false && Brain.juice >= Brain.Towers[tower.name][0])
        {
            GameObject Seed = Instantiate(seed1, transform.position, transform.rotation);
            Seed.name = seed1.name;
            done = true;
        }
    }


    void OnMouseOver()
    {
        tableInfo.gameObject.SetActive(true);
        tableInfo.transform.GetChild(2).gameObject.GetComponent<Text>().text = "" + Brain.Towers[tower.name][2];
        tableInfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "" + Brain.Towers[tower.name][0];
        tableInfo.transform.GetChild(4).gameObject.GetComponent<Text>().text = "" + tower.name;
        tableInfo.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite = tower.GetComponent<SpriteRenderer>().sprite;
    }

    void OnMouseExit()
    {
        tableInfo.gameObject.SetActive(false);
    }
}
