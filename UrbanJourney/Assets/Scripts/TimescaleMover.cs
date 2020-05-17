using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleMover : Mover
{
    Timescale timer;
    float lastTick;

    [SerializeField] float timescaleModifier = 1f; //allows for individualized velocity under the overall timescale

    void Awake()
    {
        timer = FindObjectOfType<Timescale>();
        m_rigidBody = FindObjectOfType<Rigidbody2D>();
        m_groundCheck = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Move(float move, bool jump)
    {
        base.Move(move, jump);
    }

    public override void Jump()
    {
        base.Jump();
    }

    public override void StopJump()
    {
        base.StopJump();
    }

    // Update is called once per frame
    void Update()
    {

    }

    float GetTickDelta()
    {
        return timer.GetTick() - lastTick;
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
                m_rigidBody.constraints = (RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation);
            }            
        }
        if(!m_grounded) {m_rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;}
        if (m_rigidBody.velocity != Vector2.zero) //whenever there is motion, update it according to the current timescale
        {
            Vector2 xDampedVelocity= new Vector2 (m_rigidBody.velocity.x/4, m_rigidBody.velocity.y);
            m_rigidBody.velocity += xDampedVelocity * Physics2D.gravity * (Time.fixedDeltaTime / timer.GetScale());
        }
        if(m_rigidBody.rotation != 0)
        {
            m_rigidBody.rotation = m_rigidBody.rotation * (Time.fixedDeltaTime/timer.GetScale());
        }
    }
}
