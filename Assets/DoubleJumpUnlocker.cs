using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUnlocker : MonoBehaviour
{
    public delegate void UnlockDoubleJump();
    public static event UnlockDoubleJump OnUnlockDoubleJump;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player trigger");
            OnUnlockDoubleJump();
        }
    }
}
