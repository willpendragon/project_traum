using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthSlider : MonoBehaviour
{
    [SerializeField] Enemy currentEnemy;
    [SerializeField] Slider enemyHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealthSlider.value = currentEnemy.enemyHP;
        enemyHealthSlider.minValue = 0;
        enemyHealthSlider.maxValue = currentEnemy.enemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthSlider.value = currentEnemy.enemyHP;
    }

    public void CheckEnemyHealth()
    {

        if (currentEnemy.enemyHP <= 0)
        {
            Debug.Log("Boss defeated");
            Destroy(currentEnemy.gameObject);
        }
    }
}
