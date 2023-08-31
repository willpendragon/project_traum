using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySensor : MonoBehaviour
{
    public UnityEvent PlayerDetected;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerDetected.Invoke();
            Debug.Log("Player detected");
        }
    }
}
