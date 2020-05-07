using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//thanks to BoardToBits for the jump mechanic: https://www.youtube.com/watch?v=7KiK0Aqtmzc
//And Brackeys for smooth horizontal movement: https://www.youtube.com/watch?v=dwcT-Dch0bA
public class Mover : MonoBehaviour
{    
    protected Vector3 m_moveDirection = Vector3.zero;
    protected Rigidbody2D m_rigidBody;
    protected Collider2D m_collider;
    [Range(0, 10f)][SerializeField] protected float m_jumpSpeed = 1.5f;
    [Range(0, .3f)] [SerializeField] protected float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] protected float lowJumpMultiplier = 2f;
    [SerializeField] protected float fallMultiplier = 2.5f;
    [SerializeField] protected bool m_grounded;
    [SerializeField] protected bool m_airControl;
    [SerializeField] protected float groundOffset =0.01f;
    [SerializeField] protected float groundedRayLength =0.1f;
    [SerializeField] protected private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] protected Transform m_groundCheck;
    [SerializeField] protected Transform m_ceilingCheck;
    protected bool isJumping;
    protected bool m_facingRight = true;
    protected const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    protected bool m_Grounded;            // Whether or not the player is grounded.
    protected const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    [SerializeField] protected UnityEvent onLandEvent;

      private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        if (onLandEvent == null)
            onLandEvent = new UnityEvent();
              
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public virtual void Move(float move, bool jump)
    {
        if(m_grounded || m_airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_rigidBody.velocity.y);
            // And then smoothing it out and applying it to the character
            m_rigidBody.velocity = Vector3.SmoothDamp(m_rigidBody.velocity, targetVelocity, ref m_moveDirection, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_grounded && jump)
        {
            // Add a vertical force to the player.
            m_grounded = false;
           Jump();
        }    
    }

   
    public virtual void Jump()
    {
        m_rigidBody.velocity += Vector2.up * m_jumpSpeed;
        isJumping = true;
    }

    public virtual void StopJump()
    {
        isJumping = false;
    }
    // Update is called once per frame
    void Update()
    {
       

    }

    void FixedUpdate()
    {
        bool wasGrounded = m_grounded;
        m_grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_grounded = true;
                if (!wasGrounded)
                    onLandEvent.Invoke();
            }
        }
        if (m_rigidBody.velocity.y < 0)
        {
            m_rigidBody.velocity += Vector3.up * (Physics2D.gravity * (fallMultiplier - 1)) * Time.deltaTime;
        }
        else if (m_rigidBody.velocity.y > 0 && !isJumping)
        {
            m_rigidBody.velocity += Vector3.up * (Physics2D.gravity * (lowJumpMultiplier - 1)) * Time.deltaTime;
        }
    }

    protected virtual void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_facingRight = !m_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

