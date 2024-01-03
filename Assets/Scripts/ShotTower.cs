using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTower : Tower
{
	  [SerializeField] private GameObject shot, eshot;
    private Transform target = null;
    float angle, lastang;
    private float takeJ;
    private float intj;
    private float ctime;
    GameObject Bullet;
    private List<float> chgJ = new List<float>();
    private List<float> chgDam = new List<float>();
    private Animator anim;
    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();
      takeJ = 2f;
      intj = 2f;
      anim = gameObject.GetComponent<Animator>();
      strinfo = "Sergeant of the fruit army and always Reliable. Upgrade him to improve his damage and speed";
    }

    protected override void FixedUpdate()
    {
      base.FixedUpdate();
      switch (lvl)
      {
        case 1:
          interval = 0.75f;
          dam = 1f;
          intd = 1f;
          break;
        case 2:
          interval = 0.7f;
          dam = 1.5f;
          intd = 1.5f;
          break;
        case 3:
          interval = 0.65f;
          dam = 2f;
          intd = 2f;
          break;
      }
      if (totRot <= 0)
      {
        GameObject Enemy = Instantiate(eshot, new Vector3(-10f, -2f, 0f), new Quaternion(0f, 0f, 0f, 1));
        Enemy.name = "ShotEnemy";
        Destroy(gameObject);
      }
      if (isCold)
      {
        ctime += Time.deltaTime;
        if (ctime > 1f)
        {
          isCold = false;
          ctime = 0;
        }
      }
      hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius, layer);
      if (hits.Length == 0)
      {
        target = null;
        anim.SetBool("rotate", false);
      }
      else if (tstate == TargetState.Closest && !isCold)
      {
        target = clotarget();
        anim.SetBool("rotate", true);
        angle = loopAngle(-Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg);
        transform.GetChild(0).gameObject.transform.eulerAngles = Vector3.Slerp(transform.GetChild(0).gameObject.transform.eulerAngles, new Vector3(0,0, transform.GetChild(0).gameObject.transform.eulerAngles.z + -AngleDifference(angle, loopAngle(transform.GetChild(0).gameObject.transform.eulerAngles.z))), 0.5f);
      }
      else if (tstate == TargetState.Strongest && !isCold)
      {
        target = strtarget();
        anim.SetBool("rotate", true);
        angle = loopAngle(-Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg);
        transform.GetChild(0).gameObject.transform.eulerAngles = Vector3.Slerp(transform.GetChild(0).gameObject.transform.eulerAngles, new Vector3(0,0, transform.GetChild(0).gameObject.transform.eulerAngles.z + -AngleDifference(angle, loopAngle(transform.GetChild(0).gameObject.transform.eulerAngles.z))), 0.5f);
      }
      else if (!isCold)
      {
        target = newtarget();
        anim.SetBool("rotate", true);
        angle = loopAngle(-Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg);
        transform.GetChild(0).gameObject.transform.eulerAngles = Vector3.Slerp(transform.GetChild(0).gameObject.transform.eulerAngles, new Vector3(0,0, transform.GetChild(0).gameObject.transform.eulerAngles.z + -AngleDifference(angle, loopAngle(transform.GetChild(0).gameObject.transform.eulerAngles.z))), 0.5f);
      }
      anim.SetFloat("angle",transform.GetChild(0).gameObject.transform.eulerAngles.z);
      dam = calValues(dam, intd, chgDam);
      takeJ = calValues(takeJ, intj, chgJ);
      if (hits.Length > 0 && inplay && time >= interval && Brain.juice >= takeJ && !isCold)
      {
        Bullet = Instantiate(shot, gameObject.transform.position, Quaternion.Euler(0,0,angle));
        Bullet.GetComponent<Bullets>().setTower(gameObject);
        bulletD();
        Bullet.name = "shot";
        Brain.juice -= Mathf.Min(Brain.juice, takeJ);
        time = 0f;
      }
    }

    public float loopAngle(float angle)
    {
      if (angle < 0)
      {
        return angle + 360;
      }
      return angle;
    }

    public float AngleDifference(float angle1, float angle2)
    {
      float diff = ( angle2 - angle1 + 180 ) % 360 - 180;
      return diff < -180 ? diff + 360 : diff;
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

    public Transform newtarget()
    {
      bool inCast = false;
      int tvalue = 0;
      if (tstate == TargetState.Last)
      {
        tvalue = hits.Length - 1;
        print("last");
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
        float dist = 0;
        if (target != null)
        {
          dist = Vector3.Distance(hits[i].transform.position, target.position);
        }
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

    public void bulletD()
    {
      Bullet.GetComponent<Bullets>().changeD(dam);
    }

    public void changeD(float d)
    {
      chgDam.Add(d);
      boost = true;
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
