using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Start()
    {
        currentEnemyAlertStatus = EnemyAlertStatus.enemyAlertIsNotActive;
    }
    public void Update()
    {
        if (currentEnemyAlertStatus == EnemyAlertStatus.enemyAlertIsActive)
        {
            MoveTowardsPlayer();
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
        Destroy(this.gameObject, 0.1f);
    }
    public void MoveTowardsPlayer()
    {
        /*
        Debug.Log("Enemy moves towards Player");
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 finalPlayerPosition = new Vector2(playerPosition.x, transform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(transform.position, finalPlayerPosition, enemyChaseVelocity * Time.deltaTime);
        enemyRigidbody.MovePosition(newPosition);
        */
        Debug.Log("Enemy moves towards Player");
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Calculate the direction to the player
        Vector2 directionToPlayer = (playerPosition - transform.position).normalized;

        // Set the desired velocity to the chase velocity
        Vector2 targetVelocity = directionToPlayer * enemyChaseVelocity;

        // Smoothly adjust the current velocity toward the target velocity
        enemyRigidbody.velocity = Vector2.SmoothDamp(enemyRigidbody.velocity, targetVelocity, ref smoothDampVelocity, smoothDampTime);

        // Optional: Align the enemy's forward direction with the movement direction
        if (enemyRigidbody.velocity.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(enemyRigidbody.velocity.y, enemyRigidbody.velocity.x) * Mathf.Rad2Deg;
            enemyRigidbody.rotation = 1;

        }
    }
    public void HurtPlayer()
    {
        Debug.Log("Attacking player");
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); 
        playerController.TakeDamage(enemyPower);
    }
}
