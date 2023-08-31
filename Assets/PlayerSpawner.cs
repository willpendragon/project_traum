using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnPoint;
    private void Start()
    {
        Instantiate(playerPrefab, playerSpawnPoint);
    }
}
