using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject currentPlayerPrefab;
    [SerializeField] GameObject virtualCamera;
    [SerializeField] GameObject shakeCamera;
    [SerializeField] Text hyperJumpText;
    [SerializeField] Text chargedGunText;
    private static GameManager _instance;
    private void OnEnable()
    {
        PlayerController.OnPlayerCharacterDeath += PlayerCharacterDeathSequence;
        DoubleJumpUnlocker.OnUnlockDoubleJump += ShowHyperJumpOnUI;
        DoubleJumpUnlocker.OnUnlockChargedShot += ShowChargedGunOnUI;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerCharacterDeath -= PlayerCharacterDeathSequence;
        DoubleJumpUnlocker.OnUnlockDoubleJump -= ShowHyperJumpOnUI;
        DoubleJumpUnlocker.OnUnlockChargedShot -= ShowChargedGunOnUI;
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);

    }
    void Start()
    {
        if (currentPlayerPrefab == null)
        {
            Transform spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
            Instantiate(playerPrefab);
        }
        currentPlayerPrefab = GameObject.FindGameObjectWithTag("Player");
        //Assign Cameras to Player
        //virtualCamera.GetComponent<CinemachineBlendListCamera>().Follow = currentPlayerPrefab.transform;
        //shakeCamera.GetComponent<CinemachineBlendListCamera>().Follow = currentPlayerPrefab.transform;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlayerCharacterDeathSequence()
    {
        GameObject currentPlayerController = GameObject.FindGameObjectWithTag("Player");
        //Destroy(currentPlayerController);
        Transform spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        //Get Level Beginning Transform
        currentPlayerController.transform.position = spawnPoint.position;
        //Instantiate(playerPrefab, spawnPoint);
        //currentPlayerPrefab = GameObject.FindGameObjectWithTag("Player");
        //virtualCamera.GetComponent<CinemachineBlendListCamera>().Follow = currentPlayerPrefab.transform;
        //shakeCamera.GetComponent<CinemachineBlendListCamera>().Follow = currentPlayerPrefab.transform;
        StartCoroutine("RestorePlayerHealth");

        //Update UI
        //Show GameOver Sequence
        //Assign Character to Camera
    }

    IEnumerator RestorePlayerHealth()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Restoring Player Health");
        GameObject currentPlayerController = GameObject.FindGameObjectWithTag("Player");
        //currentPlayerPrefab = GameObject.FindGameObjectWithTag("Player");
        currentPlayerController.GetComponentInChildren<PlayerStats>().currentHealth = 20;
    }

    public void ShowHyperJumpOnUI()
    {
        hyperJumpText.enabled = true;
    }
    public void ShowChargedGunOnUI()
    {
        chargedGunText.enabled = true;
    }
}
