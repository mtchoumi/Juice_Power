using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceTower : Tower
{
    [SerializeField] private GameObject shot, eshot;
    private float addj;
    private float intj;
    private Transform target = null;
    private float sinterval;
    private float stime;
    float angle;
    private bool canBlob = false;
    private float takeJ;
    GameObject Blob;
    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();
      interval = 1.5f;
      sinterval = 4f;
      addj = 5f;
      intj = 5f;
      takeJ = 1.5f;
      dam = 0.5f;
      strinfo = "Produce juice but has a secret weapon. Upgrade to improve juice production adn get Drone. Drone attacks enemy and improve the amount of juice they release on death";
    }

    protected override void FixedUpdate()
    {
      base.FixedUpdate();
      switch (lvl)
      {
        case 1:
          transform.GetChild(2).gameObject.SetActive(false);
          addj = 5f;
          intj = 5f;
          break;
        case 2:
          canBlob = true;
          transform.GetChild(2).gameObject.SetActive(true);
          addj = 5.5f;
          intj = 5.5f;
          break;
        case 3:
          sinterval = 3.5f;
          addj = 6f;
          intj = 6f;
          break;
      }
      stime += Time.deltaTime;
      if (totRot <= 0)
      {
        GameObject Enemy = Instantiate(eshot, new Vector3(-10f, -2f, 0f), new Quaternion(0f, 0f, 0f, 1));
        Enemy.name = "JuiceEnemy";
        Destroy(gameObject);
      }
      hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius, layer);
      if (hits.Length == 0)
      {
        target = null;
      }
      else if (tstate == TargetState.Closest && !isCold)
      {
        target = clotarget();
        angle = loopAngle(-Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg);
        transform.GetChild(0).gameObject.transform.eulerAngles = Vector3.Slerp(transform.GetChild(0).gameObject.transform.eulerAngles, new Vector3(0,0, transform.GetChild(0).gameObject.transform.eulerAngles.z + -AngleDifference(angle, loopAngle(transform.GetChild(0).gameObject.transform.eulerAngles.z))), 0.5f);
      }
      else if (tstate == TargetState.Strongest && !isCold)
      {
        target = strtarget();
        angle = loopAngle(-Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg);
        transform.GetChild(0).gameObject.transform.eulerAngles = Vector3.Slerp(transform.GetChild(0).gameObject.transform.eulerAngles, new Vector3(0,0, transform.GetChild(0).gameObject.transform.eulerAngles.z + -AngleDifference(angle, loopAngle(transform.GetChild(0).gameObject.transform.eulerAngles.z))), 0.5f);
      }
      else if (!isCold)
      {
        target = newtarget();
        angle = loopAngle(-Mathf.Atan2((transform.position.x - target.position.x), (transform.position.y - target.position.y))* Mathf.Rad2Deg);
        transform.GetChild(0).gameObject.transform.eulerAngles = Vector3.Slerp(transform.GetChild(0).gameObject.transform.eulerAngles, new Vector3(0,0, transform.GetChild(0).gameObject.transform.eulerAngles.z + -AngleDifference(angle, loopAngle(transform.GetChild(0).gameObject.transform.eulerAngles.z))), 0.5f);
      }
      if (hits.Length > 0 && inplay && stime >= sinterval && Brain.juice >= takeJ && !isCold && canBlob)
      {
        Blob = Instantiate(shot, transform.GetChild(2).gameObject.transform.position, Quaternion.Euler(0,0,angle));
        blobD(dam);
        Blob.name = "blob";
        Brain.juice -= Mathf.Min(Brain.juice, takeJ);
        stime = 0f;
      }
      getAng();
      if (time >= interval && inplay && !isCold)
      {
        Brain.juice += Juice.maxjuice - Mathf.Max(Brain.juice, Juice.maxjuice - addj);
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

    public void blobD(float d)
    {
      Blob.GetComponent<Blob>().changeD(d);
    }

    public float getAng()
    {
      if (angle < 0)
      {
        return angle += 360;
      }
      return angle;
    }

    public void changeJ(float j)
    {
      addj = intj * j;
      boost = true;
    }

    void OnDrawGizmos()
    {
      Gizmos.color = Color.white;
      Gizmos.DrawWireSphere(transform.position, radius);
    }
}
