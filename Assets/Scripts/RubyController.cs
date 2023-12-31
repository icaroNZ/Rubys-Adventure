using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RubyController : MonoBehaviour, IPlaySound
{
    public GameObject projectilePrefab;
    public AudioClip hit;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private AudioSource _audioSource;
    private Vector2 _lookDirection = new Vector2(1, 0);
    public float speed = 5.0f;
    public int maxHealth = 5;
    public float invencibleDuration = 2.0f;
    
    private int _currentHealth;
    private float _horizontal;
    private float _vertical;
    private bool _isInvencible;
    private float _invencibleTimer;
    private static readonly int Launch1 = Animator.StringToHash("Launch");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Start(){
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _currentHealth = maxHealth;
    }

    void Update(){
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.C)){
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X)){
            var hit = Physics2D.Raycast(_rigidbody2D.position + Vector2.up * 0.2f, _lookDirection, 1.5f,
                LayerMask.GetMask("NPC"));
            if(hit.collider != null){
               if (hit.collider.gameObject.TryGetComponent(out NonPlayerCharacter rubyController)){
                   rubyController.DisplayDialog();
               }
                
            }
        }

        var move = new Vector2(_horizontal, _vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }

        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);



        if (_isInvencible){
            _invencibleTimer -= Time.deltaTime;
            if (_invencibleTimer < 0){
                _isInvencible = false;
            }
        }
    }

    private void FixedUpdate(){
        var position = transform.position;
        position.x += speed * _horizontal * Time.deltaTime;
        position.y += speed * _vertical * Time.deltaTime;
        _rigidbody2D.MovePosition(position);
    }
    
    public void PlaySound(AudioClip clip){
        _audioSource.PlayOneShot(clip);
    }

    public void DoDamage(int amount){
        if (_isInvencible){
            return;
        }
        PlaySound(hit);
        _animator.SetTrigger(Hit);
        _isInvencible = true;
        _invencibleTimer = invencibleDuration;
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, maxHealth);
        UIHealthBar.Instance.SetValue(_currentHealth / (float)maxHealth);

    }
    public bool ChangeCurrentHealthIfNeed(int amount){
        if (_currentHealth == maxHealth){
            return false;
        }
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log(_currentHealth + "/" + maxHealth);
        UIHealthBar.Instance.SetValue(_currentHealth / (float)maxHealth);
        return true;
        
    }
    
    public void Launch(){
        var projectileGameObject = Instantiate(projectilePrefab,_rigidbody2D.position + Vector2.up * 0.5f,
            Quaternion.identity);
        var projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300, this);
        _animator.SetTrigger(Launch1);
    }
}
