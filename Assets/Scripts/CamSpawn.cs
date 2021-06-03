using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Cm Main").transform);
    }
}
