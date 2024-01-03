using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    private float damage;
    private float angle, ang;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0f,0f,1f) * Time.deltaTime * 45f;
        transform.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder = Mathf.RoundToInt(Mathf.Sin(transform.eulerAngles.z *  Mathf.Deg2Rad)) * 2;
    }

    public float takeD()
    {
        return damage;
    }

    public void changeD(float d)
    {
      damage = d;
    }
}
