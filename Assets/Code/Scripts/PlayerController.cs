using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent ChargedBulletShot;
    public UnityEvent PlayerCharacterDeath;

    [SerializeField] float characterSpeed;
    [SerializeField] Rigidbody2D characterRigidbody;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator characterAnimator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] float bulletCharge;
    [SerializeField] float bulletMaximumCharge;
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Vector2 jumpForce;
    [SerializeField] Vector2 augmentedJumpForce;
    [SerializeField] float movementSmoothing;
    [SerializeField] Vector2 velocityRef;
    //[SerializeField] float jumpHeight = 1f;

    void Update()
    {
        if (playerAbilities.chargedShotActivated)
        {
            ChargeBulletAbility();
        }
        else if (playerAbilities.basicShotActivated)
        {
            BasicBulletAbility();
        }
        MoveCharacter();
        if (Input.GetButtonDown("Jump") && CharacterIsGrounded())
        {
            characterRigidbody.velocity = Vector3.zero;
            Jump();
        }
        if (playerStats.currentHealth <= 0)
        {
            PlayerCharacterDeath.Invoke();
        }
    }
    private void MoveCharacter()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        characterAnimator.SetFloat("horizontalInput", Mathf.Abs(horizontalInput));

        Vector2 targetVelocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
        characterRigidbody.velocity = Vector2.SmoothDamp(characterRigidbody.velocity, targetVelocity, ref velocityRef, movementSmoothing);
    }
    private void Jump()
    {
        //characterRigidbody.velocity = (Vector2.up * jumpHeight);
        if (playerAbilities.doubleJumpActivated == false)
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (playerAbilities.doubleJumpActivated == true)
        {
            characterRigidbody.AddForce(Vector2.up * augmentedJumpForce, ForceMode2D.Impulse);
        }
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
    void BasicBulletAbility()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            bulletPrefab.GetComponent<Bullet>().bulletChargedPower = bulletCharge * 2;
            Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
            Debug.Log("Shooting Basic Bullet");
        }
    }
    public void TakeDamage(float receivedDamage)
    {
        playerStats.currentHealth -= receivedDamage;
    }
}
