using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject[] Obj;
    // Start is called before the first frame update
    void Start()
    {
        Obj[0].gameObject.GetComponent<Button>().onClick.AddListener( delegate {Begin();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        SceneManager.LoadScene("Main");
    }
}
