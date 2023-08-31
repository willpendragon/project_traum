using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHitbox : MonoBehaviour
{
    public UnityEvent<float> ReceivedHit;

    public void ReceiveHit(float incomingDamage)
    {
        ReceivedHit.Invoke(incomingDamage);
    }
}
