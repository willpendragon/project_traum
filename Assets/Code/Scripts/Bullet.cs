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
    public float bulletChargedPower;


    void Start()
    {
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
