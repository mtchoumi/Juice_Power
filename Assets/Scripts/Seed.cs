using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    private GameObject tower;
    GameObject Tower;
    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).gameObject.transform.localScale = new Vector2(Brain.Towers[tower.name][3] * 8,Brain.Towers[tower.name][3] * 8);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }

    void OnMouseDown() 
    {
        if (!transform.GetChild(0).gameObject.GetComponent<CollisionCheck>().isCol)
        {
            Tower = Instantiate(tower, transform.position, transform.rotation);
            Brain.juice -= Mathf.Min(Brain.juice, Brain.Towers[tower.name][0]);
            Tower.transform.position = transform.position;
            Tower.name = tower.name;
            Tower.transform.localScale = new Vector2(1f,1f);
            Tower.transform.SetParent(GameObject.Find("Towers").transform);
            Destroy(gameObject);
            SpawnTower.done = false;
            Tower.GetComponent<Tower>().changeP(true);
        }
        else
        {
            Destroy(gameObject);
            Destroy(Tower);
            SpawnTower.done = false;
        }
    }
}
