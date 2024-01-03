using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] private GameObject juiceblob;
    private GameObject WellPoints;
    public static bool[] isBlob;
    // Start is called before the first frame update
    void Start()
    {
        WellPoints = GameObject.Find("WellPoints");
        isBlob = new bool[WellPoints.gameObject.transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (Brain.wstates != Brain.WaveStates.paused)
        {
            time += Time.deltaTime;
            if (time >= 3f)
            {
                for (int i = 0; i < isBlob.Length;)
                {
                    if (isBlob[i] == false)
                    {
                        GameObject Blobs = Instantiate(juiceblob, WellPoints.gameObject.transform.GetChild(i).position, Quaternion.identity);
                        Blobs.GetComponent<Buckets>().setVal(i);
                        isBlob[i] = true;
                        time = 0f;
                        i = isBlob.Length;
                    }
                    else
                    {
                        i++;
                        if (isFull())
                        {
                            time = 0f;
                        }
                    }
                }
            }
        }
    }

    public bool isFull()
    {
        for (int i = 0; i < isBlob.Length; i++)
        {
            if (isBlob[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
