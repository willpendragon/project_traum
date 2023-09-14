using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        PlayerController currentPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentPlayerController.enabled = false;
    }

    // Update is called once per frame
    void OnDisable()
    {
        PlayerController currentPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentPlayerController.enabled = true;
    }
}
