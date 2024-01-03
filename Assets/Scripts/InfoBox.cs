using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    private GameObject tower;
    private int x = 1;
    private float juicecost;
    [SerializeField] private GameObject[] IBobj;
    // Start is called before the first frame update
    public void Start()
    {
        IBobj[3].gameObject.GetComponent<Button>().onClick.AddListener( delegate {Sell();});
        IBobj[5].gameObject.GetComponent<Button>().onClick.AddListener( delegate {chgSt();});
        IBobj[4].gameObject.GetComponent<Button>().onClick.AddListener( delegate {lvlUp();});
    }

    // Update is called once per frame
    void Update()
    {
        if (tower == gameObject ||  tower == null)
        {
			transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
			transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (tower.GetComponent<Tower>().getfreeze())
        {
           IBobj[1].gameObject.GetComponent<SpriteRenderer>().color = Color.white; 
        }
        else
        {
            IBobj[1].gameObject.GetComponent<SpriteRenderer>().color = Color.black; 
        }
        if (tower.GetComponent<Tower>().getboost())
        {
            IBobj[2].gameObject.GetComponent<SpriteRenderer>().color = Color.white; 
        }
        else
        {
            IBobj[2].gameObject.GetComponent<SpriteRenderer>().color = Color.black; 
        }
        juicecost = Brain.Towers[tower.name][0] * (1.5f * (tower.GetComponent<Tower>().lvlIs() + 1));
        IBobj[6].gameObject.GetComponent<Text>().text = "Sell: " + System.Math.Round(tower.GetComponent<Tower>().costJ(),1);
        IBobj[9].gameObject.GetComponent<Text>().text = "Lvl: " + tower.GetComponent<Tower>().lvlIs() + " RotLvl: " + Mathf.Round(tower.GetComponent<Tower>().costR()) + " Damage: " + tower.GetComponent<Tower>().getDam() + " Speed: " + tower.GetComponent<Tower>().getSpeed();
        IBobj[7].gameObject.GetComponent<Text>().text = "Level Cost: " + juicecost;
        IBobj[0].gameObject.GetComponent<Text>().text = "" + tower.GetComponent<Tower>().Info();
        IBobj[10].gameObject.GetComponent<Text>().text = tower.name;
        switch (x)
        {
            case 1:
                IBobj[8].gameObject.GetComponent<Text>().text = "Target: First";
                break;
            case 2:
                IBobj[8].gameObject.GetComponent<Text>().text = "Target: Last";
                break;
            case 3:
                IBobj[8].gameObject.GetComponent<Text>().text = "Target: Close";
                break;
            case 4:
                IBobj[8].gameObject.GetComponent<Text>().text = "Target: Strong";
                break;
        }
    }

    public void chgSt()
    {
        x = (x % 4) + 1;
    }

    public void lvlUp()
    {
        if (tower.GetComponent<Tower>().lvlIs() < 3 && Brain.juice > juicecost)
        {
            tower.GetComponent<Tower>().chgLvl();
            tower.GetComponent<Tower>().chgJu(5);
            tower.GetComponent<Tower>().chgTot(50);
            Brain.juice -= Mathf.Min(Brain.juice, juicecost);
            if (tower.name == "Juicerator")
            {
                Brain.juice += 25;
                Juice.maxjuice += 50;
            }
        }
    }

    void OnMouseDown()
    {
        if (tower == gameObject ||  tower == null)
        {
			Tower.towerselected = false;
        }
        else
        {
			Tower.towerselected = true;
        }
    }

    public int state()
    {
        return x;
    }

    public void selectedTower(GameObject gO)
    {
        tower = gO;
    }

    public void Sell()
    {
        Destroy(tower);
    }
}
