using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRigidbody;
    private Vector2 playerPosition;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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
}
