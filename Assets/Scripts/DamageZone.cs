using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other){
        if (other.TryGetComponent(out RubyController rubyController)){
            rubyController.DoDamage(1);
        }
    }
}
