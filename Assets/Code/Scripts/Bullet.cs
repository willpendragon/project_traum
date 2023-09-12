using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D bulletRigidbody;
    [SerializeField] float bulletLifeTime;
    [SerializeField] float bulletAttackPower;
    [SerializeField] Vector2 bulletForce;
    [SerializeField] Collider2D bulletCollider;
    private PlayerController playerController;
    public float bulletChargedPower;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PlayerControllerDirection currentPlayerControllerDirection = playerController.currentPlayerControllerDirection;

        if (currentPlayerControllerDirection == PlayerControllerDirection.PlayerControllerUp)
        {
            bulletForce = new Vector2(0, 2);
        }
        else if (currentPlayerControllerDirection == PlayerControllerDirection.PlayerControllerRight)
        {
            bulletForce = new Vector2(2, 0);
        }
        else if (currentPlayerControllerDirection == PlayerControllerDirection.PlayerControllerLeft)
        {
            bulletForce = new Vector2(-2, 0);
        }
        else if (currentPlayerControllerDirection == PlayerControllerDirection.PlayerControllerDown)
        {
            bulletForce = new Vector2(0, -2);
        }
        else if (currentPlayerControllerDirection == PlayerControllerDirection.PlayerControllerStill)
        {
            if (playerController.characterLastPosition == new Vector3(1, 1, 1))
            {
                bulletForce = new Vector2(2, 0);
            }
            else if (playerController.characterLastPosition == new Vector3(-1, 1, 1))
            {
                bulletForce = new Vector2(-2, 0);
            }
        }
        bulletRigidbody.AddForce(bulletForce, ForceMode2D.Impulse);
        Destroy(this.gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyHitbox")
        {
            Debug.Log("Enemy was hit");
            bulletCollider.enabled = false;
            other.gameObject.GetComponent<EnemyHitbox>().ReceiveHit(this.bulletAttackPower + this.bulletChargedPower * 2.5f);
        }
    }
}
