using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public int amount;
    public float rotSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Add to some total somewhere
            //collision.GetComponent<Player>().//TotalCoins += amount;

            //Currency noise
            AudioManager.Instance.Play("Coin Pickup");

            //Destroy
            Destroy(gameObject);
        }
    }
}
