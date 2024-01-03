using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingTowers : MonoBehaviour
{
	private bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (on)
		{
			transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2( 7.9f, 0f), Time.time * 0.01f);
		}
		else if (!on)
		{
			transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2( 9.4f, 0f), Time.time * 0.01f);
		}
    }
	
	void OnMouseDown() 
    {
		if (on)
		{
			on = false;
		}
		else
		{
			on = true;
		}
    }
}
