using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _timer;
    private int _direction = 1;
    private bool isBroken = true;
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    private void Start(){
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _timer = changeTime;
    }

    private void Update(){
        if (!isBroken){
            return;
        }
        
        _timer -= Time.deltaTime;
        if (_timer > 0){
            return;
        }
        _direction *= -1;
        _timer = changeTime;
    }

    private void FixedUpdate(){
        if (!isBroken){
            return;
        }
        
        var position = _rigidbody2D.position;
        if (vertical){
            position.y += speed * Time.deltaTime * _direction;
            _animator.SetFloat(MoveX, 0);
            _animator.SetFloat(MoveY, _direction);
        }
        else{
            position.x += speed * Time.deltaTime * _direction;
            _animator.SetFloat(MoveX, _direction);
            _animator.SetFloat(MoveY, 0);
        }
        _rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.TryGetComponent(out RubyController rubyController)){
            rubyController.DoDamage(1);
        }
    }
    
    public void Fix(){
        isBroken = false;
        _rigidbody2D.simulated = false;
        _animator.SetTrigger("Fixed");
    }
}
