using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//Movement controlls
	public float speed;// how fast the charachter moves
	public float jumpForce;// how far you can jump
	private float moveInput;// detects key press

	private Rigidbody2D rb; // Reference to the players rigid body
	private Animator animator;

	//flipping the sprite
	private bool facingRight = true;

	//Jump controlls
	private bool isGrounded; // is the player touching the ground
	public Transform groundCheck;// checks if the players transform position is touching the ground
	public float checkRadius;// radius of how far the ground check extends
	public LayerMask whatIsGround;//specifies what layer mask the ground is on so the player can only jump from ground surface

	private float jumpTimeCounter;
	public float jumpTime;
	private bool isJumping;

	private int extraJumps;//how many extra jumps the player has
	public int extraJumpValue;// input how many jumps you want the player to have

	bool isTouchingFront;
	public Transform frontCheck;
	bool wallSliding;
	public float wallSlidingSpeed;

	private void Start()
	{
		extraJumps = extraJumpValue;
		rb = GetComponent<Rigidbody2D>();// get character rigid body
		animator = GetComponent<Animator>();// gets player animator
	}
	private void FixedUpdate()
	{
		moveInput = Input.GetAxisRaw("Horizontal");// detects if right or left arrow keys have been pressed
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);// moves the character based on the key press

		if(moveInput == 0)
		{
			animator.SetBool("isRunning", false);
		}
		else
		{
			animator.SetBool("isRunning", true);
		}

		if (facingRight == false && moveInput > 0)
		{
			Flip();
		}else if(facingRight== true && moveInput < 0)
		{
			Flip();
		}
	}

	private void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

		if (isGrounded == true)
		{
			extraJumps = extraJumpValue;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
		{
			isJumping = true;
			jumpTimeCounter = jumpTime;
			animator.SetBool("isJumping", true);
			rb.velocity = Vector2.up * jumpForce;
			extraJumps--;
		}else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
		{
			animator.SetBool("isJumping", true);
			rb.velocity = Vector2.up * jumpForce;
		}
		if (Input.GetKey(KeyCode.UpArrow) && isJumping == true)
		{
			if (jumpTimeCounter > 0)
			{
				animator.SetBool("isJumping", true);
				rb.velocity = Vector2.up * jumpForce;
				jumpTimeCounter -= Time.deltaTime;
			}
			else
			{
				isJumping = false;
				animator.SetBool("isJumping", false);
			}
		}
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			isJumping = false;
			animator.SetBool("isJumping", false);
		}

		isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
		if(isTouchingFront == true && isGrounded == false && moveInput != 0)
		{
			wallSliding = true;
		}
		else
		{
			wallSliding = false;
		}
		if (wallSliding)
		{
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
		}
	}

	void Flip()// flips the player sprite when going right or left
	{
		facingRight = !facingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}
