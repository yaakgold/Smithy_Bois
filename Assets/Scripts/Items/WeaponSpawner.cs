using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public SpriteRenderer weaponSprite;
    public Transform spawner;
    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        weaponSprite.enabled = false;
        if(other.GetType() == weapon.GetType())
        {
            weapon.isEquipped = true;
            
        }
    }
}
