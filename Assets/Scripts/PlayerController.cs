using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float speed = 2.5f;
    public float jumpForce = 2.5f;
   
    //Variables for Jump
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Transform _firePoint;
    private PlayerHealth _playerHealth;

    //Movement
    private Vector2 _movement;
    private bool _isGrounded;

    

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _firePoint = transform.Find("FirePoint");
        _playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        //--- HORIZONTAL MOVEMENT ---
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //Devuelve el valor del movimiento horizonal final -1 o 1
        _movement = new Vector2(horizontalInput, 0f);

        //Flip Character
        if (horizontalInput < 0f) { // Left
            transform.localScale = new Vector3 (-1.0f, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput > 0f) { // Right
            transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
        }
        //--- END ---

        //--- JUMP ---
        //isGrounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //--- END ---

        // --- SHOOT ---
        if (Input.GetButtonDown("Fire1")){
            if (bulletPrefab != null && _firePoint != null){
                Shoot();
            }
        }
        //--- END ---
    }

    private void FixedUpdate() // FixedUpdate es adecuado para mover objetos por físicas
    {
        float horizontalVelocity = _movement.normalized.x * speed;
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
    }

    private void LateUpdate()
    {
        _animator.SetBool("idle", _movement == Vector2.zero);
    }

    private void Shoot() {
        GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;

        Bullet bulletComponent = myBullet.GetComponent<Bullet>();
        bulletComponent.direction = new Vector2(transform.localScale.x, 0f);
    }

    private void OnDisable()
    {
        _playerHealth.RestoreHealth();
    }
}
