using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public bool basicShotActivated;
    public bool chargedShotActivated;
    public bool plasmaShotActivated;
    public bool doubleJumpActivated;

    void OnEnable()
    {
        DoubleJumpUnlocker.OnUnlockDoubleJump += UnlockDoubleJump;
    }

    void UnlockDoubleJump()
    {
        doubleJumpActivated = true;
    }
}


