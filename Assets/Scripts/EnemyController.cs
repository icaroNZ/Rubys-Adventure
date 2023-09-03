using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    
    private Rigidbody2D _rigidbody2D;
    private float _timer;
    private int _direction = 1;

    private void Start(){
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = changeTime;
    }

    private void Update(){
        _timer -= Time.deltaTime;
        if (_timer > 0){
            return;
        }
        _direction *= -1;
        _timer = changeTime;
    }

    private void FixedUpdate(){
        var position = _rigidbody2D.position;
        if (vertical){
            position.y += speed * Time.deltaTime * _direction;
        }
        else{
            position.x += speed * Time.deltaTime * _direction;
        }
        _rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.TryGetComponent(out RubyController rubyController)){
            rubyController.DoDamage(1);
        }
    }
}
