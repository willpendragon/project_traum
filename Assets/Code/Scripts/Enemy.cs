using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyAlertStatus
{
    enemyAlertIsActive,
    enemyAlertIsNotActive
}

public class Enemy : MonoBehaviour
{
    public float enemyHP;
    [SerializeField] Rigidbody2D enemyRigidbody;
    [SerializeField] float enemyPower;
    [SerializeField] float enemyChaseVelocity = 1.5f;
    public EnemyAlertStatus currentEnemyAlertStatus;
    [SerializeField] Vector2 smoothDampVelocity;
    [SerializeField] float smoothDampTime;
    [SerializeField] bool shootingVariant;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator enemyVFXAnimator;
    [SerializeField] EnemyBossLogic enemyBossLogic;
    private bool shootingCooldownIsActive;
    public AudioSource enemyDeathSound;

    public UnityEvent EnemyHurt;
    public UnityEvent EnemyDyingScream;

    public void Start()
    {
        enemyVFXAnimator.GetComponentInParent<SpriteRenderer>().enabled = false;
        currentEnemyAlertStatus = EnemyAlertStatus.enemyAlertIsNotActive;
    }
    public void Update()
    {
        if (currentEnemyAlertStatus == EnemyAlertStatus.enemyAlertIsActive)
        {
            MoveTowardsPlayer();
        }
        if (enemyBossLogic != null)
        {
            if (enemyHP < 150)
            {
                enemyBossLogic.UpdateSprite();
            }
        }
    }

    public void ActivateEnemyAlertStatus()
    {
        currentEnemyAlertStatus = EnemyAlertStatus.enemyAlertIsActive;
    }
    public void TakeDamage(float incomingDamage)
    {
        Debug.Log("Applying damage to Enemy");
        Debug.Log(incomingDamage);
        enemyHP -= incomingDamage;
        CheckEnemyStatus();
        playerAnimator.SetTrigger("enemyHurt");
        EnemyHurt.Invoke();
    }
    public void CheckEnemyStatus()
    {
        if (enemyHP <= 0)
        {
            EnemyDies();
        }
    }
    public void EnemyDies()
    {
        enemyVFXAnimator.SetTrigger("enemyHasDied");
        enemyVFXAnimator.GetComponentInParent<SpriteRenderer>().enabled = true;
        //enemyDeathSound.Play();
        EnemyDyingScream.Invoke();
        Destroy(this.gameObject, 2f);
    }
    public void MoveTowardsPlayer()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Calculate the direction to the player
        Vector2 directionToPlayer = (playerPosition - transform.position).normalized;

        // Set the desired velocity to the chase velocity
        Vector2 targetVelocity = directionToPlayer * enemyChaseVelocity;

        // Smoothly adjust the current velocity toward the target velocity
        enemyRigidbody.velocity = Vector2.SmoothDamp(enemyRigidbody.velocity, targetVelocity, ref smoothDampVelocity, smoothDampTime);

        // Align the enemy's forward direction with the movement direction
        if (enemyRigidbody.velocity.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(enemyRigidbody.velocity.y, enemyRigidbody.velocity.x) * Mathf.Rad2Deg;
            enemyRigidbody.rotation = 1;
        }
    }
    public void HurtPlayer()
    {
        enemyDeathSound.Play();
        Debug.Log("Attacking player");
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.TakeDamage(enemyPower);
    }

    public void ShootProjectileAtThePlayer()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    public void ShootPlayer()
    {
        if (shootingVariant == true)
        {
            if (shootingCooldownIsActive == false)
            {
                Debug.Log("Bullet shooting delay running");
            }
            else if (shootingCooldownIsActive == true)
            {
                ShootProjectileAtThePlayer();
                StartCoroutine("ShootingCooldown");
            }
        }
    }
    IEnumerator ShootingCooldown()
    {
        shootingCooldownIsActive = true;
        yield return new WaitForSeconds(5);
        shootingCooldownIsActive = false;
    }
}
