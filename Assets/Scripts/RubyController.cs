using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private void Start(){
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        var position = transform.position;
        position.x = position.x + 1f * horizontal * Time.deltaTime;
        position.y = position.y + 1f * vertical * Time.deltaTime;
        transform.position = position;
    }
}
