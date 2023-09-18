using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyBossLogic : MonoBehaviour
{
    public bool laserIsActive;
    public BoxCollider2D enemyDamageArea;
    public float enemyLaserLifetime;
    public SpriteRenderer enemyLaserGraphics;
    public Light2D enemyLaserLight;
    public float enemyLaserAttackBeginningTime;
    public float enemyLaserAttackTimeInterval;
    public float enemyLaserLightIntensity;
    public Vector2 enemyLaserGraphicsLocalScale;
    public Vector3 enemyBossDamageAreaSize;
    public ParticleSystem laserParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootLaser", enemyLaserAttackBeginningTime, enemyLaserAttackTimeInterval);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void ShootLaser()
    {
        {
            laserParticleSystem.Play();
            Debug.Log("Activating Laser");
            enemyDamageArea.size = enemyBossDamageAreaSize;
            enemyLaserGraphics.transform.localScale = enemyLaserGraphicsLocalScale;
            enemyLaserLight.intensity = enemyLaserLightIntensity;
            StartCoroutine("StopLaser");
        }
    }
    IEnumerator StopLaser()
    {
        yield return new WaitForSeconds(enemyLaserLifetime);
        laserParticleSystem.Stop();    
        enemyDamageArea.size = new Vector2(0, 0);
        enemyLaserLight.intensity = 0;
        enemyLaserGraphics.transform.localScale = new Vector3(0, 0, 0);
    }
}
