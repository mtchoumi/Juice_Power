using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected enum TargetState {First, Last, Closest, Strongest};
    protected TargetState tstate;
    public GameObject InfoB;
    Vector2 screenPosition;
    Vector2 worldPosition;
    protected float time = 0f;
    protected float rtime = 0f;
	protected float interval = 0f;
    protected float dam;
    protected float intd;
	protected float totRot = 0f;
    public static bool towerselected = false;
    public static GameObject selectedtower = null;
    [SerializeField] protected float rotRate = 0;
    protected float introt = 0;
    private float sellJ = 0f;
    private float intsJ = 0f;
    protected bool inplay = false;
    private bool isRot = false;
    protected bool isCold = false;
    private bool freeze = false;
    protected bool boost = false;
    protected int lvl = 1;
    protected string strinfo = "";
    protected RaycastHit2D[] hits;
    private RaycastHit2D[] near;
    [SerializeField] protected float radius;
    [SerializeField] protected LayerMask layer;
    [SerializeField] private LayerMask lay2;
    private List<float> chgRot = new List<float>();
    // Start is called before the first frame update
    protected virtual void Start()
    {
        InfoB = GameObject.Find("UIBox");
        InfoB.GetComponent<InfoBox>().selectedTower(InfoB);
        rotRate = 0.020f;
        introt = 0.020f;
        totRot = Brain.Towers[gameObject.name][2];
        intsJ = Brain.Towers[gameObject.name][1];
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    protected virtual void FixedUpdate()
    {
        near = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius, lay2);
        rotRate = calValues(rotRate, introt, chgRot);
        transform.GetChild(0).gameObject.transform.localScale = new Vector2(radius * 2,radius * 2);
        if (transform.childCount > 1 && freeze)
        {
            if(!transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
        else if (transform.childCount > 1 && !freeze)
        {
            if(transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
            } 
        }
        if (transform.childCount > 2 && boost)
        {
            if(!transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
        else if (transform.childCount > 2 && !boost)
        {
            if(transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Stop();
            } 
        }
        switch (InfoB.GetComponent<InfoBox>().state())
        {
            case 1:
                tstate = TargetState.First;
                break;
            case 2:
                tstate = TargetState.Last;
                break;
            case 3:
                tstate = TargetState.Closest;
                break;
            case 4:
                tstate = TargetState.Strongest;
                break;
        }
        if (inplay)
        {
            if (near.Length > 3)
            {
                rotRate *= (1 + ((near.Length - 3) * 0.5f));
            }
            totRot -= rotRate;
            time += Time.deltaTime;
            rtime += Time.deltaTime;
            if (isRot = true && rtime > 5f)
            {
                isRot = false;
            }
        }
        if(Brain.wstates == Brain.WaveStates.paused)
        {
            inplay = false;
        }
        else
        {
           inplay = true;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Input.GetMouseButtonDown(0) && !towerselected)
        {
            InfoB.GetComponent<InfoBox>().selectedTower(InfoB);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if(Input.GetMouseButtonDown(0) && towerselected && selectedtower == gameObject)
        {
            InfoB.GetComponent<InfoBox>().selectedTower(gameObject);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (Input.GetMouseButtonDown(0) && towerselected && selectedtower != gameObject)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		if (Brain.Towers[gameObject.name][1] > 1)
		{
			sellJ = intsJ * (totRot/Brain.Towers[gameObject.name][2]);
		}
    }

    protected virtual void OnMouseExit()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        towerselected = false;
    }

    protected virtual void OnMouseOver()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        towerselected = true;
        selectedtower = gameObject;
    }
	
	protected virtual void OnDestroy()
	{
        if (inplay)
        {
            Brain.juice += Mathf.Min(sellJ, Juice.maxjuice - Brain.juice);
        }
	}

    public string Info()
    {
        return strinfo;
    }

    public float costJ()
    {
        return sellJ;
    }

    public void chgJu(float f)
    {
        intsJ += f;
    }

    public float costR()
    {
        return totRot;
    }

    public void chgTot(float r)
    {
        totRot += r;
    }

    public void changeP(bool b)
    {
        inplay = b;
    }

    public bool isRoted()
    {
        return isRot;
    }

    public void notfreeze()
    {
        freeze = false;
    }

    public void notboost()
    {
        boost = false;
    }

    public bool getfreeze()
    {
        return freeze;
    }

    public bool getboost()
    {
       return boost;
    }
     
    public int lvlIs()
    {
        return lvl;
    }

    public float getDam()
    {
        return dam;
    }

    public float getSpeed()
    {
        return interval;
    }

    public void chgLvl()
    {
        lvl++;
    }

    public void chgCold(bool b)
    {
        isCold = b;
    }

    public void changeR(float r)
    {
        chgRot.Add(r);
        freeze = true;
    }

    public void chgRE(float r)
    {
        chgRot.Add(r);
        isRot = true;
    }

    public float lenIs()
    {
      return radius;
    }

    private float calValues(float x, float intx, List<float> listx)
    {
      x = intx;
      for (int i = 0; i < listx.Count; i++)
      {
        x = x * listx[i];
      }
      for (int i = 0; i < listx.Count; i++)
      {
        listx.RemoveAt(i);
      }
      return x;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "eshot")
        {
            totRot -= col.GetComponent<EBullets>().takeD();
        }
        if (col.gameObject.name == "erang")
        {
            totRot -= col.GetComponent<ERang>().takeD();
        }
    }
}
