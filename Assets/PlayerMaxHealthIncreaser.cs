using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaxHealthIncreaser : MonoBehaviour
{
    public delegate void PlayerHealthIncrease();
    public static event PlayerHealthIncrease OnPlayerHealthIncrease;

    public delegate void PlayerHealthIncrease_2();
    public static event PlayerHealthIncrease OnPlayerHealthIncrease_2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHealthIncreaseMethod()
    {
        OnPlayerHealthIncrease();   
    }

    public void PlayerHealthIncreaseMethod_2()
    {
        OnPlayerHealthIncrease_2();
    }
}
