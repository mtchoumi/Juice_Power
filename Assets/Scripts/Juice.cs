using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : MonoBehaviour
{
	public static float maxjuice;
    // Start is called before the first frame update
    void Start()
    {
      maxjuice = 150f;
    }

    // Update is called once per frame
    void Update()
    {
		  gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3 ((Brain.juice/maxjuice)*1f, gameObject.transform.localScale.y, gameObject.transform.localScale.z), Time.time * 0.01f);
    }
}
