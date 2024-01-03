using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERang : MonoBehaviour
{
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Brain.wstates != Brain.WaveStates.paused)
        {
            transform.RotateAround(transform.parent.position, transform.forward, 45 * Time.deltaTime);
        }
    }

    public float takeD()
    {
        return damage;
    }

}
