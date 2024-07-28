using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    #region Singleton
    // Singleton instance for easy access from other scripts
    private static CharacterMovement _instance;
    public static CharacterMovement Instance { get { return _instance; } }
    #endregion
    
    #region Variables
    // Movement speed variables
    public float moveSpeed = 0f;
    public float extraSpeedFromApple = 5f;
    public float sprintMultiplier = 2f; // Sprint speed multiplier

    // Jumping variables
    public float jumpForce;
    public float extraJumpFromApple;

    // Rigidbody component for physics interactions
    [SerializeField] private Rigidbody2D rb;

    // Jumping control variables
    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    public Vector2 direction;
    public GameObject[] sides;

    // Power-up status variables
    private bool extraSpeedActive = false;
    private bool extraJumpActive = false;

    // Animator component for character animations
    public Animator animator;
    public bool isDoorTriggered;
    
    #endregion

    #region Event Functions

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Ensure only one instance of the class exists
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Called before the first frame update
    private void Start()
    {
        // Get the Rigidbody component attached to the character
        rb = GetComponent<Rigidbody2D>();
    }

    // Called once per frame
    private void Update()
    {
        Jump();
        Move();
    }

    #endregion

    #region Movement Functions

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void Move()
    {
        // Read input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate current movement speed, considering power-up status and sprinting
        float currentMoveSpeed = extraSpeedActive ? moveSpeed + extraSpeedFromApple : moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveSpeed *= sprintMultiplier;
        }

        // Apply horizontal movement to the character
        Vector2 movement = new Vector2(horizontalInput * currentMoveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Update animator parameter for character speed
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Flip character sprite based on movement direction
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    #endregion

    #region Collision Functions

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag(GROUND_TAG))
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

