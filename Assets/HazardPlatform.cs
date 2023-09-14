using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardPlatform : MonoBehaviour
{
    [SerializeField] float hazardPlatformDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(hazardPlatformDamage);
            Debug.Log("Tile is hurting the Player");
        }
    }
}
