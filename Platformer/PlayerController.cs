using System;
using UnityEngine;


public class PlayerController_move : MonoBehaviour
{
    // скорости перемещения
    public float jumpForce = 5f;
    
    
    // стоит ли на земле
    public float checkRadius = 0.5f;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    // тело
    private Rigidbody2D _rb;
    
    // анимация
    private Animator _animator;
    
    
    public float minScale = 0.5f; // минимальный размер объекта
    public float scaleSpeed = 0.5f; // скорость изменения размера
    public float forceSpeed = 100f; // скорость нарастания силы
    private Vector3 originalScale; // исходный размер
    private float timeMouseDown; // время нажатия кнопки мыши

    // кнопка рестарт
    public GameObject button;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start() 
    {
        _rb = GetComponent<Rigidbody2D>();
        
        
        //
        originalScale = transform.localScale;
        //
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void Update() 
    {
        
        JumpZombie();
        //удаляем
        if (transform.position.y <= -6.0f)
        {
            button.SetActive(true);
            Destroy(gameObject);
            
        }
    }
    
    

    private void JumpZombie()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            timeMouseDown = 0; // Сброс таймера
        }

        // пока кнопка мыши зажата
        if (Input.GetMouseButton(0))
        {
            timeMouseDown += Time.deltaTime;


            // уменьшаем зомби
            float scale = Mathf.Max(minScale, originalScale.x - scaleSpeed * timeMouseDown);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        // кнопка мыши отпущена
        if (Input.GetMouseButtonUp(0))
        {
          
            Vector2 jumpDirection = new Vector2(1, 1).normalized * jumpForce * timeMouseDown;
            _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
            _animator.SetBool("Jump" , true);
            
            

            // исходный размер
            transform.localScale = originalScale; 
            
            return;
            
        }
        
        // проверяю летит он или приземлился (чтобы тормозил при приземлении)
        var current = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (current == false && isGrounded == true)
        {
            isGrounded = false;
        }
        
        if (current == true && isGrounded == false)
        {
            isGrounded = true;
            _rb.velocity = new Vector2(0, 0);
            _animator.SetBool("Jump" , false);
        }
        //

    }
    

  

    

}



