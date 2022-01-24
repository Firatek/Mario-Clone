using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;

    public float moveSpeed;
    public float jumpforce;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool grounded;
    public LayerMask whatIsGround;
    public bool stoppedJumping;

    public Transform groundCheck;
    public float groundCheckRadius;

    private Rigidbody2D myBody;

    private Animator animator;



    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        grounded = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if(grounded){
            jumpTimeCounter = jumpTime;
        }
        if(Input.GetKeyDown("space")){
            if(grounded){
                animator.SetBool("IsJumping", true);
                myBody.velocity = new Vector2(myBody.velocity.x, jumpforce);
                stoppedJumping = false;
            }
        }
        if(Input.GetKeyDown("space") && !stoppedJumping){
            if(jumpTimeCounter > 0){
                myBody.velocity = new Vector2(myBody.velocity.x, jumpforce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp("space")){
            jumpTimeCounter = 0;
            stoppedJumping = true;
            animator.SetBool("IsJumping", false);
        }
    }


   /*DEBUG GROUND COLLISION
    private void OnDrawGizmos() {
     Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere (groundCheck.position, groundCheckRadius);
    }**/

    private void FixedUpdate() {
        Vector2 horizontalSpeed = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed,
        myBody.velocity.y);
        myBody.velocity = horizontalSpeed;
        animator.SetFloat("Speed", horizontalSpeed.x);

    }
}
