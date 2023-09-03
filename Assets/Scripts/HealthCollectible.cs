using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.TryGetComponent(out RubyController rubyController)){
            if (rubyController.ChangeCurrentHealthIfNeed(1)){
                Destroy(gameObject);
            }
        }
    }
}
