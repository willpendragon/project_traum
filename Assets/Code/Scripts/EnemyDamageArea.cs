using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamageArea : MonoBehaviour
{
    public UnityEvent PlayerEnteredDamageArea;
    public void OnTriggerStay2D(UnityEngine.Collider2D collider)
    {
        if (collider.tag == "PlayerHitbox")
        {
            Debug.Log("Player collided with Enemy Damage area");
            PlayerEnteredDamageArea.Invoke();
        }
    }
}
