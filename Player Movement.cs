using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float superJumpPower;
    [SerializeField]  private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private float superJumpCooldown;
    private AudioSource sd_audioSource;
    public AudioClip sd_jumpClip;
    public AudioClip sd_wallAttachClip;
    private bool hasWallAttached = false;

    private void Awake()
    {
        //Grab references for rigidbody, animator and BoxCollider from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 

        // Gets the audio scource references so it can play audio.
        sd_audioSource = this.GetComponent<AudioSource>();
    }
    private void Update()
    {
        //Lets the player move left-right
       
        float horizontalInput = Input.GetAxis("Horizontal");

        
        if (isGrounded()) // if player is grounded
        {
            if (horizontalInput != 0) // If player is not idle
            {
                if (!sd_audioSource.isPlaying)
                {
                    sd_audioSource.Play(); // Play walking sound
                }
            }
            else
            {
                
                if (sd_audioSource.isPlaying)
                {
                    sd_audioSource.Stop(); // Stop walking sound if player not moving
                }
            }
            if (Input.GetButtonDown("Jump") && isGrounded()) // If player is pressing jump as is on ground
            {
                sd_audioSource.PlayOneShot(sd_jumpClip); // Play jump sound
            }
          
        }
        if (onWall() && !isGrounded()) // If the player is on the wall and not grounded
        {
            if (!hasWallAttached) // Check if player hasn't already attached to the wall
            {
                sd_audioSource.PlayOneShot(sd_wallAttachClip); // Play wall attach sound
                hasWallAttached = true; // Set the flag to true to not repeat
            }
            if (Input.GetButtonDown("Jump")) // If player is pressing jump
            {
                sd_audioSource.PlayOneShot(sd_jumpClip); // Play jump sound
            }
        }
        else
        {
            hasWallAttached = false; // Reset when not on the wall
        }



        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);




        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        //Wall Jump Logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;  
                body.velocity = Vector2.zero;  
                                               
                if (Input.GetKey(KeyCode.Space))
                {
                    Jump();  
                    wallJumpCooldown = 0f; 
                }
            }
            else
                body.gravityScale = 2.5f;
            

            if (Input.GetKey(KeyCode.Space))
                Jump(); // Normal Jump when grounded
         
        }
        else
            wallJumpCooldown += Time.deltaTime;

        //Super Jump Logic

        if (superJumpCooldown > 5f)
        {
            if (isGrounded())
            {
                if (Input.GetKey(KeyCode.W))
                    superJump();
    
            }

        }
        else
            superJumpCooldown += Time.deltaTime;

    }



    private void Jump()
    {
        if (isGrounded())// If player is grounded
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower); // Makes the player jump by changing y velocity by jump power, which we can change.
            anim.SetTrigger("jump"); // Starts jumping animation

            
        }
        else if (onWall() && !isGrounded()) // if player is on wall and is not grounded
        {
            if (horizontalInput == 0) // No horizontal input
            {
                // Push away from the wall horizontally
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, body.velocity.y); // Only change the x velocity

                // Flip the character
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) / 10 * 4, transform.localScale.y, transform.localScale.z);

                // Apply vertical velocity for the wall jump
                body.velocity = new Vector2(body.velocity.x, 6); // Set the y velocity for the jump
            }
            else
            {
                // No specific action on horizontal input, just set a cooldown or reset mechanics
                wallJumpCooldown = 0;
            }
        }
    }



    private void superJump()
    {
        if (isGrounded()) // if the player is grounded
        {
            body.velocity = new Vector2(body.velocity.x, superJumpPower); // Makes the player jump by changing y velocity. Keeps x velocity the same.
            anim.SetTrigger("SuperJump"); // Starts superjump animation. (Needs Fixing)

            superJumpCooldown = 0; // Resets jump cooldown
        }
    }



 
    // Boxcast for when player if on the ground, isGrounded() will return true as collider will not equal null
    private bool isGrounded()  
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    // Boxcast for detecting walls. If player is facing a wall, onWall() will return true as collider will not equal null
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0) , 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    // Player can only attack when 
    public bool canAttack()
    {
        return !onWall();
    }

    
}
