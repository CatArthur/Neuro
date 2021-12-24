using UnityEngine;
using System;
using DefaultNamespace;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	private float m_JumpForce=600f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.

	private SpriteRenderer m_SpriteRenderer;
	
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	
	
	public UnityEvent OnLandEvent;
	public UnityEvent OnJumpEvent;

	[System.Serializable] public class BoolEvent : UnityEvent<bool> { }
	
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_SpriteRenderer = GetComponent<SpriteRenderer>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		if (OnJumpEvent == null)
			OnJumpEvent = new UnityEvent();
	}


	private void FixedUpdate()
	{
		UpdatingEmotions();	
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}
		
		
		if (!wasGrounded&&m_Grounded)
		{
			OnLandEvent.Invoke();
		}
		if (wasGrounded&&!m_Grounded)
		{
			OnJumpEvent.Invoke();
		}
	}


	public void Move(float move, bool jump)
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

			if ((move > 0 && !m_FacingRight)||(move < 0 && m_FacingRight))
			{
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// m_SpriteRenderer.flipX=m_FacingRight;
		m_FacingRight = !m_FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void UpdatingEmotions() {
		if (GlobalData.activeEmo==3)
			m_JumpForce = 800f;
		else
			m_JumpForce = 600f;
	}
}
 