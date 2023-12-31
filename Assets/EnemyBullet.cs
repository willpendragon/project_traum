using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRigidbody;
    private Vector2 playerPosition;
    PlayerController player;
    [SerializeField] float enemyBulletPower;
    [SerializeField] LayerMask playerBulletLayer;

    private void OnEnable()
    {
        //subscribe to PlayerController
    }

    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().transform.position;

        // Calculate the direction to the player
        Vector2 directionToPlayer = (playerPosition - transform.position).normalized;

        // Set the desired velocity to the chase velocity
        Vector2 targetVelocity = directionToPlayer * 2f;

        var smoothDampVelocity = new Vector2(0, 0);
        // Smoothly adjust the current velocity toward the target velocity
        bulletRigidbody.velocity = Vector2.SmoothDamp(bulletRigidbody.velocity, targetVelocity, ref smoothDampVelocity, 0.1f);

        // Align the enemy's forward direction with the movement direction
        if (bulletRigidbody.velocity.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(bulletRigidbody.velocity.y, bulletRigidbody.velocity.x) * Mathf.Rad2Deg;
            bulletRigidbody.rotation = 1;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            Debug.Log("Player Collided with Enemy Bullet");
            HitPlayer();
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Destroying Player bullet");
            Destroy(this.gameObject, 0.1f);
            
        }
    }
    public void HitPlayer()
    {
        Debug.Log("Attacking player with Bullet");
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.TakeDamage(enemyBulletPower);
    }
}
