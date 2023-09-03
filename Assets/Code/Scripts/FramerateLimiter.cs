using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Application.targetFrameRate = 25;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
