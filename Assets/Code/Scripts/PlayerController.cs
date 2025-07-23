using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System;

public enum PlayerControllerDirection
{
    PlayerControllerUp,
    PlayerControllerDown,
    PlayerControllerLeft,
    PlayerControllerRight,
    PlayerControllerStill
}

public class PlayerController : MonoBehaviour
{
    public UnityEvent ChargedBulletShot;

    [SerializeField] float characterSpeed;
    [SerializeField] Rigidbody2D characterRigidbody;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator characterAnimator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] private float bulletCharge;
    [SerializeField] private float bulletMaximumCharge;
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Vector2 jumpForce;
    [SerializeField] Vector2 augmentedJumpForce;
    [SerializeField] float movementSmoothing;
    [SerializeField] Vector2 velocityRef;
    [SerializeField] Slider healthSlider;
    [SerializeField] RectTransform playerHealthBar;
    public Vector3 characterLastPosition;
    public PlayerControllerDirection currentPlayerControllerDirection;
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction chargeBullet;
    private InputAction shootBullet;
    private InputAction jump;
    bool charging;


    public delegate void PlayerCharacterDeath();
    public static event PlayerCharacterDeath OnPlayerCharacterDeath;

    public UnityEvent PlayerShotBasicBullet;

    private void Awake()
    {
        playerControls = new PlayerInputActions();

        DontDestroyOnLoad(this.gameObject);
    }
    public bool GetCharge()
    {
        return charging;
    }

    private void ChargeBulletCanceled(InputAction.CallbackContext context)
    {
        charging = false;
    }

    private void ChargeBulletPerformed(InputAction.CallbackContext context)
    {
        charging = true;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        chargeBullet = playerControls.Player.ChargeBullet;
        shootBullet = playerControls.Player.ShootBullet;
        jump = playerControls.Player.Jump;
        move.Enable();
        chargeBullet.Enable();
        shootBullet.Enable();
        jump.Enable();
        chargeBullet.performed += ChargeBulletPerformed;
        chargeBullet.canceled += ChargeBulletCanceled;
        shootBullet.performed += ShootBullet;
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        chargeBullet.Disable();
        chargeBullet.performed -= ChargeBulletPerformed;
        chargeBullet.canceled -= ChargeBulletCanceled;
        shootBullet.performed -= ShootBullet;
        jump.performed -= Jump;
    }

    private void Start()
    {
        SetHealthSlider();
        characterLastPosition = new Vector3(1, 1, 1);
        CinemachineVirtualCamera[] vcams = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var vcam in vcams)
        {
            vcam.Follow = this.transform;
        }
    }

    void Update()
    {
        healthSlider.value = playerStats.currentHealth;
        MoveCharacter();
        if (GetCharge() && bulletCharge < bulletMaximumCharge)
        {
            bulletCharge += Time.deltaTime;
        }
        //else
        //{
        //    bulletCharge -= Time.deltaTime;
        //}
        // Remember that some elements of the level design require the bullet charge to be unlocked.
        //if (Input.GetButtonDown("Jump") && CharacterIsGrounded())
        //{
        //    characterRigidbody.velocity = Vector3.zero;
        //    Jump();
        //}
    }
    public void SetHealthSlider()
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = playerStats.maximumHealth;
        healthSlider.value = playerStats.maximumHealth;
    }
    public void UpdateHealthSlider()
    {
        healthSlider.value = playerStats.currentHealth;
    }
    private void MoveCharacter()
    {
        var moveDirection = move.ReadValue<Vector2>();
        Debug.Log(moveDirection);

        characterAnimator.SetFloat("horizontalInput", Mathf.Abs(moveDirection.x));

        Vector2 targetVelocity = new Vector2(moveDirection.x * characterSpeed, characterRigidbody.velocity.y);
        characterRigidbody.velocity = Vector2.SmoothDamp(characterRigidbody.velocity, targetVelocity, ref velocityRef, movementSmoothing);

        if (moveDirection.y > 0.1)
        {
            currentPlayerControllerDirection = PlayerControllerDirection.PlayerControllerUp;
            Debug.Log("Player Controller Up");
        }
        else if (moveDirection.x < 0 && moveDirection.x != 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            characterLastPosition = transform.localScale;
            currentPlayerControllerDirection = PlayerControllerDirection.PlayerControllerLeft;
            playerHealthBar.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Player Controller Left");
        }
        else if (moveDirection.x > 0 && moveDirection.x != 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            characterLastPosition = transform.localScale;
            currentPlayerControllerDirection = PlayerControllerDirection.PlayerControllerRight;
            playerHealthBar.localScale = new Vector3(1, 1, 1);
            Debug.Log("Player Controller Right");
        }
        else if (moveDirection.x == 0)
        {
            currentPlayerControllerDirection = PlayerControllerDirection.PlayerControllerStill;
            Debug.Log(characterLastPosition);
            transform.localScale = characterLastPosition;
            Debug.Log("Player Controller Still");
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {

        //characterRigidbody.velocity = (Vector2.up * jumpHeight);
        if (CharacterIsGrounded())
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
        if (playerAbilities.doubleJumpActivated == false)
        {
        }
        //else if (playerAbilities.doubleJumpActivated == true)
        //{
        //    characterRigidbody.AddForce(Vector2.up * augmentedJumpForce, ForceMode2D.Force);
        //}
    }
    private bool CharacterIsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
        return groundCheck.collider != null && groundCheck.collider.IsTouchingLayers(groundLayer);
    }
    void ChargeBulletAbility()
    {
        if (bulletCharge >= bulletMaximumCharge)
        {
            bulletCharge = bulletMaximumCharge;
        }
        if (Input.GetButton("Fire1"))
        {
            bulletCharge += Time.deltaTime;
            Debug.Log("Charging Projectile");
        }
        if (Input.GetButtonUp("Fire1"))
        {
            bulletPrefab.GetComponent<Bullet>().bulletChargedPower = bulletCharge * 2;
            Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
            ChargedBulletShot.Invoke();
            Debug.Log("Shooting Charged Bullet");
        }
    }

    public void ResetChargedBulletShot()
    {
        bulletCharge = 0;
    }
    void ShootBullet(InputAction.CallbackContext context)
    {
        if (bulletCharge <= 0)
            return;
        bulletPrefab.GetComponent<Bullet>().bulletChargedPower = bulletCharge * 2;
        Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
        //Vector3 bulletSize = new Vector3(1, 1, 1);
        //bulletSize = new Vector3(1 * bulletCharge / 25, 1 * bulletCharge / 25, 1 * bulletCharge / 25);
        //bulletPrefab.transform.localScale = bulletSize;
        PlayerShotBasicBullet.Invoke();
        bulletCharge = 0;
        Debug.Log("Shooting Basic Bullet");
    }
    public void TakeDamage(float receivedDamage)
    {
        playerStats.currentHealth -= receivedDamage;
        characterAnimator.SetTrigger("mainCharacterHurt");
        UpdateHealthSlider();
    }
    public void CheckPlayerHealth()
    {
        Debug.Log("Checking Player health");
        if (playerStats.currentHealth <= 0)
        {
            Debug.Log("Player died");
            OnPlayerCharacterDeath();
        }
    }
    public void SetPlayerShootUpAnimation()
    {
        characterAnimator.SetTrigger("shootingUp");
    }

    public void SetPlayerBasicShootingAnimation()
    {
        characterAnimator.SetTrigger("basicShooting");
    }
}
