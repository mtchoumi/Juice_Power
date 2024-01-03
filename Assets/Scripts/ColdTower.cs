using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdTower : Tower
{
  [SerializeField] private GameObject eshot;
  private float speed;
  private float chgVal;
  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();
    strinfo = "Makes near by fruit last longer. Upgrade to improve its effect and slow down enemies near the fridge";
  }

  protected override void FixedUpdate()
  {
    base.FixedUpdate();
    //transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
    switch (lvl)
    {
      case 1:
        speed = 1f;
        chgVal = 0.5f;
        break;
      case 2:
        speed = 0.75f;
        chgVal = 0.5f;
        break;
      case 3:
        speed = 0.75f;
        chgVal = 0.25f;
        break;
    }
    if (totRot <= 0)
    {
      GameObject Enemy = Instantiate(eshot, new Vector3(-10f, -2f, 0f), new Quaternion(0f, 0f, 0f, 1));
      Enemy.name = "ColdEnemy";
      Destroy(gameObject);
    }
    hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius);
    if (hits.Length > 0 && inplay)
    {
      for(int i = 0; i < hits.Length; i++)
      {
        if (hits[i].transform.tag == "Tower" && !isCold && hits[i].transform != gameObject.transform)
        {
          hits[i].transform.GetComponent<Tower>().changeR(chgVal);
          if (hits[i].transform.name == "Sgt. Citrus")
          {
            hits[i].transform.GetComponent<ShotTower>().changeJ(chgVal);
          }
          if (hits[i].transform.name == "Ananasus")
          {
            hits[i].transform.GetComponent<SeedTower>().changeJ(chgVal);
          }
          else if (hits[i].transform.name == "Aristcado")
          {
            hits[i].transform.GetComponent<RangTower>().changeJ(chgVal);
          }
          else if (hits[i].transform.name == "BombTower")
          {
            hits[i].transform.GetComponent<BombTower>().changeJ(chgVal);
          }
        }
        else if (hits[i].transform.tag == "Enemy" && !isCold)
        {
          hits[i].transform.GetComponent<Enemy>().changeS(speed);
        }
      }
    }
  }

  protected override void OnDestroy()
	{
    base.OnDestroy();
    for (int i = 0; i < hits.Length; i++)
    {
      hits[i].transform.GetComponent<Tower>().notfreeze();
    }
	}

  void OnDrawGizmos()
  {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
