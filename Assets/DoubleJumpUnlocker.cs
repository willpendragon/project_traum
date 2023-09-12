using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DoubleJumpUnlocker : MonoBehaviour
{
    public delegate void UnlockDoubleJump();
    public static event UnlockDoubleJump OnUnlockDoubleJump;
    public delegate void UnlockChargedShot();
    public static event UnlockChargedShot OnUnlockChargedShot;
    public bool doubleJump;
    public bool chargedShot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && doubleJump == true)
        {
            Debug.Log("Double Jump unlocked");
            OnUnlockDoubleJump();
        }
        else if (collision.tag == "Player" && chargedShot == true)
        {
            Debug.Log("Charged Shot unlocked");
            OnUnlockChargedShot();
            //Setting the variable for Chargin Gun collected inside Dialogue System database
            DialogueLua.SetVariable("playerCollectedChargingGun", true);
        }
    }
}
