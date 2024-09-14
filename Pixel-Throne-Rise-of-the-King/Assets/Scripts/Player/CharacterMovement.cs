using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Singleton
    private static CharacterMovement _instance;
    public static CharacterMovement Instance { get { return _instance; } }
    #endregion

    #region Variables
    public float moveSpeed = 0f;
    public float extraSpeedFromApple = 5f;
    public float sprintMultiplier = 2f;
    public float jumpForce;
    public float extraJumpFromApple;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;

    [SerializeField] private Rigidbody2D rb;
    private bool isGrounded;
    private string GROUND_TAG = "Ground";
    private bool extraSpeedActive = false;
    private bool extraJumpActive = false;
    private Animator animator;
    public bool isDashing { get; private set; } = false;
    private float dashTime;
    private Collider2D characterCollider;
    [SerializeField] private Animator dashAnimation;
    public int playerLayer { get; set; }
    public int bossLayer { get; set; }
    [SerializeField] private GameObject dashEffect;
    #endregion

    #region Event Functions
    private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerLayer = LayerMask.NameToLayer("Player");
        bossLayer = LayerMask.NameToLayer("Boss");
        dashEffect.SetActive(false);
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (isDashing)
        {
            Dash();
        }
        else
        {
            Jump();
            Move();
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                StartDash();
            }
            else{
                dashEffect.SetActive(false);
            }
        }
    }
    #endregion

    #region Movement Functions
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float currentMoveSpeed = extraSpeedActive ? moveSpeed + extraSpeedFromApple : moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveSpeed *= sprintMultiplier;
        }

        Vector2 movement = new Vector2(horizontalInput * currentMoveSpeed, rb.velocity.y);
        rb.velocity = movement;
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashEffect.SetActive(true);
        animator.Play("smoke2");  // Replace with your animation name
        dashTime = Time.time + dashDuration;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        Physics2D.IgnoreLayerCollision(playerLayer, bossLayer, true);
    }

    private void Dash()
    {
        if (Time.time >= dashTime)
        {
            isDashing = false;
            
        }
    }
    #endregion

    #region Collision Functions
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PotionJump")
        {
            extraJumpActive = true;
            jumpForce = jumpForce + extraJumpFromApple;
            Destroy(collision.gameObject);
        }
    }
    #endregion

    #region Animation Functions
    public void PlayAnimation()
    {
        animator.SetTrigger("IsDoorTriggered");
    }
    #endregion
}