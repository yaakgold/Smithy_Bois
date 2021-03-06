using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum eWeaponType
    {
        Drill,
        Flamethrower,
        Pickaxe,
        Sword
    }

    public Animator animator;
    public bool isEquipped = false;
    public float attackRange = 0.0f;
    public float attackSpeed = 0.0f;
    public float strengthModifier = 0.0f;
    public eWeaponType type = eWeaponType.Flamethrower;

    public void Attack()
    {
        animator.SetTrigger("Attack");
        //Play death audio
        AudioManager.Instance.Play($"{type} Attack");
    }

    public void DisableAnim()
    {
        animator.SetTrigger("StopFlame");
    }
}
