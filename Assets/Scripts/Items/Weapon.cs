using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum eWeaponType
    {
        Flamethrower,
        Pickaxe,
        Slingshot,
        Sword
    }

    public Animator animator;
    public bool isEquipped = false;
    public float attackRange = 0.0f;
    public float attackSpeed = 0.0f;
    public float strengthModifier = 0.0f;
    public eWeaponType type = eWeaponType.Flamethrower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
