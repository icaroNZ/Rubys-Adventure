using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;
    public float speed = 5.0f;
    public int maxHealth = 5;
    public float invencibleDuration = 2.0f;
    
    private int _currentHealth;
    private float _horizontal;
    private float _vertical;
    private bool _isInvencible;
    private float _invencibleTimer;

    private void Start(){
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (_isInvencible){
            _invencibleTimer -= Time.deltaTime;
            if (_invencibleTimer < 0){
                _isInvencible = false;
            }
        }
    }

    private void FixedUpdate(){
        var position = transform.position;
        position.x = position.x + speed * _horizontal * Time.deltaTime;
        position.y = position.y + speed * _vertical * Time.deltaTime;
        _rigidbody2D.MovePosition(position);
    }

    public void DoDamage(int amount){
        if (_isInvencible){
            return;
        }
        _isInvencible = true;
        _invencibleTimer = invencibleDuration;
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, maxHealth);
        Debug.Log(_currentHealth + "/" + maxHealth);
    }
    public bool ChangeCurrentHealthIfNeed(int amount){
        if (_currentHealth == maxHealth){
            return false;
        }
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log(_currentHealth + "/" + maxHealth);
        return true;
    }
}
