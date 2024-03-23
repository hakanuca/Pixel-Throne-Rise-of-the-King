//------------------------------------
//      HAKAN UCA
//  GITHUB:https://github.com/HakanUca
//------------------------------------

using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Singleton instance for easy access from other scripts
    private static CharacterMovement _instance;
    public static CharacterMovement Instance { get { return _instance; } }

    // Movement speed variables
    public float moveSpeed = 0f;
    public float extraSpeedFromApple = 5f;

    // Jumping variables
    public float jumpForce = 10f;
    public float extraJumpFromApple = 3f;

    // Rigidbody component for physics interactions
    [SerializeField] private Rigidbody2D rb;

    // Jumping control variables
    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    
    // Power-up status variables
    private bool extraSpeedActive = false;
    private bool extraJumpActive = false;

    // Animator component for character animations
    public Animator animator;

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
        // Read input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate current movement speed, considering power-up status
        float currentMoveSpeed = extraSpeedActive ? moveSpeed + extraSpeedFromApple : moveSpeed;

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

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            
        animator.SetBool("IsJumping", true);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

    
        }


        // Check for attack input
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            
        }
    }


    // This method should be changed for the better optimize version of the process.
    // Called when the Collider2D enters a trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with speed power-up
        if (collision.gameObject.name == "Potion")
        {
            extraSpeedActive = true;
            Destroy(collision.gameObject);
        }
        // Check for collision with jump power-up
        if (collision.gameObject.name == "PotionJump")
        {
            extraJumpActive = true;
            Destroy(collision.gameObject);
        }
    }
}


