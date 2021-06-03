using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public Animator animator;
    public bool isEquipped = false;
    public float AttackRange { get; set; } = 0.0f;
    public float AttackSpeed { get; set; } = 0.0f;
    public float StrengthModifier { get; set; } = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
