using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Tower
{
    [SerializeField] private GameObject bomb, eshot;
    GameObject Bomb;
    private Transform target = null;
    float angle;
    private float takeJ;
    private float intj;
    private List<float> chgJ = new List<float>();
    private List<float> chgDam = new List<float>();
    private Animator anim;
    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();
      interval = 2f;
      dam = 3f;
      intd = 3f;
      takeJ = 3f;
      intj = 3f;
      anim = gameObject.GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
      base.FixedUpdate();
      if (totRot <= 0)
      {
        GameObject Enemy = Instantiate(eshot, new Vector3(-10f, -2f, 0f), new Quaternion(0f, 0f, 0f, 1));
        Enemy.name = "BombEnemy";
        Destroy(gameObject);
      }
      hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius, layer);
      if (hits.Length == 0 && !isCold)
      {
        target = null;
      }
      else if (tstate == TargetState.Closest && !isCold)
      {
        target = clotarget();
        angle = -Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(new Vector3(0, 0, angle)),Time.time * 5f);
      }
      else if (tstate == TargetState.Strongest && !isCold)
      {
        target = strtarget();
        angle = -Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(new Vector3(0, 0, angle)),Time.time * 5f);
      }
      else if (!isCold)
      {
        target = newtarget();
        angle = -Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(new Vector3(0, 0, angle)),Time.time * 5f);
      }
      float ang = angle;
      if (ang < 0)
      {
        ang += 360;
      }
      anim.SetFloat("angle",ang);
      dam = calValues(dam, intd, chgDam);
      takeJ = calValues(takeJ, intj, chgJ);
      if (hits.Length > 0 && inplay && time >= interval && Brain.juice >= 4)
      {
        Bomb = Instantiate(bomb, gameObject.transform.position, gameObject.transform.rotation);
        bombD(dam);
        Bomb.name = "bomb";
        Brain.juice -= Mathf.Min(Brain.juice, takeJ);
        time = 0f;
      }
    }

    public Transform newtarget()
    {
      bool inCast = false;
      int tvalue = 0;
      if (tstate == TargetState.Last)
      {
        tvalue = hits.Length - 1;
      }
      if (target == null)
      {
        return hits[tvalue].transform;
      }
      else
      {
        for (int i = 0; i < hits.Length; i++)
        {
          if (hits[i].transform == target)
          {
            inCast = true;
          }
        }
        if (inCast == true)
        {
          return target;
        }
        else
        {
          return hits[tvalue].transform; 
        }
      }
    }

    public Transform clotarget()
    {
      Transform tMin = null;
      float minDist = Mathf.Infinity;
      Vector3 currentPos = transform.position;
      for(int i = 0; i < hits.Length; i++)
      {
          float dist = Vector3.Distance(hits[i].transform.position, target.position);
          if (dist < minDist)
          {
              tMin = hits[i].transform;
              minDist = dist;
          }
      }
      return tMin;
    }

    public Transform strtarget()
    {
      Transform tMax = null;
      int maxValue = -1;
      for(int i = 0; i < hits.Length; i++)
      {
          int x = hits[i].transform.gameObject.GetComponent<Enemy>().strValue();
          if (x > maxValue)
          {
            tMax = hits[i].transform;
            maxValue = x;
          }
      }
      return tMax;
    }

    public void bombD(float d)
    {
      Bomb.GetComponent<Bomb>().changeD(d);
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

    public void changeD(float d)
    {
      chgDam.Add(d);
    }

    public void changeJ(float j)
    {
      chgJ.Add(j);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
