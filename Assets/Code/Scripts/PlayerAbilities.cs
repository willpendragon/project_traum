using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static DoubleJumpUnlocker;
public class PlayerAbilities : MonoBehaviour
{
    public bool basicShotActivated;
    public bool chargedShotActivated;
    public bool plasmaShotActivated;
    public bool doubleJumpActivated;

    void OnEnable()
    {
        DoubleJumpUnlocker.OnUnlockDoubleJump += UnlockDoubleJump;
        DoubleJumpUnlocker.OnUnlockChargedShot += UnlockChargedShot;
    }
    private void OnDisable()
    {
        DoubleJumpUnlocker.OnUnlockDoubleJump -= UnlockDoubleJump;
        DoubleJumpUnlocker.OnUnlockChargedShot -= UnlockChargedShot;
    }
    void UnlockDoubleJump()
    {
        doubleJumpActivated = true;
    }
    void UnlockChargedShot()
    {
        chargedShotActivated = true;
    }
}

