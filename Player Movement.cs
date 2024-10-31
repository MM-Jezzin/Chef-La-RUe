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


    private void Awake()
    {
        //Grab references for rigidbody, animator and BoxCollider from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 

    }
    private void Update()
    {
        //Lets the player move left-right
        horizontalInput = Input.GetAxis("Horizontal");


        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);




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
            }
            else
                body.gravityScale = 2.5f;

            if (Input.GetKey(KeyCode.Space))
                Jump();


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
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");

            
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }



    private void superJump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, superJumpPower);
            anim.SetTrigger("jump");

            superJumpCooldown = 0;
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
