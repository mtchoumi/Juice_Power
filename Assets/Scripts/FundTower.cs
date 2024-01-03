using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundTower : Tower
{
    [SerializeField] private GameObject eshot;
    private float inDam;
    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();
      dam = 2f;
      inDam = 1.5f;
      strinfo = "Boost the Damage of near by towers. Upgrade to improve the boost and create a sprinkler line that damages near by enemies";
    }

    protected override void FixedUpdate()
    {
      base.FixedUpdate();
      switch (lvl)
      {
        case 1:
          transform.GetChild(3).gameObject.SetActive(false);
          inDam = 1.25f;
          break;
        case 2:
          transform.GetChild(3).gameObject.SetActive(true);
          inDam = 1.5f;
          break;
        case 3:
          dam = 2.5f;
          inDam = 2f;
          break;
      }
      staffD(dam);
      if (totRot <= 0)
      {
        GameObject Enemy = Instantiate(eshot, new Vector3(-10f, -2f, 0f), new Quaternion(0f, 0f, 0f, 1));
        Enemy.name = "FundEnemy";
        Destroy(gameObject);
      }
      hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius);
      if (hits.Length > 0 && inplay)
      {
        for(int i = 0; i < hits.Length; i++)
        {
          if (hits[i].transform.name == "Sgt. Citrus")
          {
            hits[i].transform.GetComponent<ShotTower>().changeD(inDam);
          }
          else if (hits[i].transform.name == "Aristcado")
          {
            hits[i].transform.GetComponent<RangTower>().changeD(inDam);
          }
          else if (hits[i].transform.name == "BombTower")
          {
            hits[i].transform.GetComponent<BombTower>().changeD(inDam);
          }
          else if (hits[i].transform.name == "Ananasus")
          {
            hits[i].transform.GetComponent<SeedTower>().changeD(inDam);
          }
          else if (hits[i].transform.name == "Juicerator")
          {
            hits[i].transform.GetComponent<JuiceTower>().changeJ(inDam);
          }
        }
      }
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      for (int i = 0; i < hits.Length; i++)
      {
        hits[i].transform.GetComponent<Tower>().notboost();
      }
    }

    public void staffD(float d)
    {
      transform.GetChild(3).GetComponent<Staff>().changeD(d);
    }

    void OnDrawGizmos()
    {
      Gizmos.color = Color.white;
      Gizmos.DrawWireSphere(transform.position, radius);
    }
}
