using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerController.OnPlayerCharacterDeath += PlayerCharacterDeathSequence;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayerCharacterDeathSequence()
    {
        GameObject currentPlayerController = GameObject.FindGameObjectWithTag("Player");
        Destroy(currentPlayerController);
        Transform spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;

        //Get Level Beginning Transform
        Instantiate(playerPrefab, spawnPoint);

        //Update UI
        //Show GameOver Sequence
        //Assign Character to Camera
    }
}
