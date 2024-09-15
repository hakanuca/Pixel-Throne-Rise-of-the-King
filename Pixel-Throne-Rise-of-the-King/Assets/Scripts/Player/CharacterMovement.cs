using UnityEngine;
using UnityEngine.SceneManagement;

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

    // New sprint-related variables
    private bool isSprinting = false;
    private float sprintEndTime;
    private float sprintCooldownEndTime;
    private bool sprintOnCooldown = false;
    public float sprintDuration = 2f;  // Sprint duration
    public float sprintCooldown = 2f;  // Sprint cooldown

    // New dash cooldown variable
    private float dashCooldownEndTime;
    public float dashCooldown = 2f;  // Dash cooldown
    private bool dashOnCooldown = false;
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
            HandleSprint(); // Handle sprint logic

            // Check for dash input and cooldown
            if (Input.GetKeyDown(KeyCode.LeftControl) && !dashOnCooldown)
            {
                StartDash();
            }
            else
            {
                dashEffect.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                DoorAnim();
            }
        }

        // Check if dash is on cooldown
        if (Time.time >= dashCooldownEndTime)
        {
            dashOnCooldown = false;
        }
    }
    #endregion

    #region Movement Functions
    public float fallMultiplier = 2.5f;  // Multiplier for faster fall
    public float lowJumpMultiplier = 2f; // Multiplier for lower jump when jump button is released early

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
            isGrounded = false;
        }

        if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float currentMoveSpeed = extraSpeedActive ? moveSpeed + extraSpeedFromApple : moveSpeed;

        // Handle sprint
        if (isSprinting)
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

    private void HandleSprint()
    {
        // Sprint activation and cooldown
        if (Input.GetKey(KeyCode.LeftShift) && !isSprinting && !sprintOnCooldown)
        {
            isSprinting = true;
            sprintEndTime = Time.time + sprintDuration;
        }

        // Check if sprint should end
        if (isSprinting && Time.time >= sprintEndTime)
        {
            isSprinting = false;
            sprintOnCooldown = true;
            sprintCooldownEndTime = Time.time + sprintCooldown;
        }

        // Check if sprint cooldown is over
        if (sprintOnCooldown && Time.time >= sprintCooldownEndTime)
        {
            sprintOnCooldown = false;
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashEffect.SetActive(true);
        animator.Play("smoke2");  
        dashTime = Time.time + dashDuration;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        Physics2D.IgnoreLayerCollision(playerLayer, bossLayer, true);

        // Set dash on cooldown
        dashOnCooldown = true;
        dashCooldownEndTime = Time.time + dashCooldown;
    }

    private void Dash()
    {
        if (Time.time >= dashTime)
        {
            isDashing = false;
        }
    }

    private void DoorAnim()
    {
        animator.Play("Main_Character_DoorIn");  
    }

    public void DeactivatePlayer()
    {
        gameObject.SetActive(false);
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

    #region Collider Disabling Functions
    public void DisableCurrentColliders()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
    #endregion

    #region Scene Restart Function
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    #endregion

    #region Rigidbody Freeze Function
    public void FreezeRigidbodyX()
    {
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    public void UnfreezeRigidbodyX()
    {
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None; 
        }
    }
    #endregion
}
