using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawner : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public bool isChosen = false;
    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChosen && canSpawn)
        {
            var e = Instantiate(enemies[Random.Range(0, 2)], transform.position, Quaternion.identity);
            transform.parent.GetComponent<Room>().enemiesInRoom.Add(e);
            e.GetComponent<Enemy>().room = transform.parent.GetComponent<Room>();
            isChosen = false;
            canSpawn = false;
        }
    }
}
