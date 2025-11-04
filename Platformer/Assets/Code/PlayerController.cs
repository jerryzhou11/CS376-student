using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;
    
    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    
    // dash variables
    private bool isDashing;
    private float dashTimeLeft;
    private float dashCooldownLeft;
    private float dashDirection;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // update dash cooldown
        if (dashCooldownLeft > 0)
        {
            dashCooldownLeft -= Time.deltaTime;
        }
        
        // don't process other input if dashing
        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
            return;
        }
        
        // get horizontal input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        // check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // jump
        bool jumpPressed = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W);
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        // dash
        if (Input.GetButtonDown("Dash") && dashCooldownLeft <= 0 && !isDashing)
        {
            StartDash();
        }
    }
    
    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }
    }
    
    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashCooldownLeft = dashCooldown;
        
        if (horizontalInput != 0)
        {
            dashDirection = horizontalInput;
        }
        else
        {
            dashDirection = 1;
        }
    }
}