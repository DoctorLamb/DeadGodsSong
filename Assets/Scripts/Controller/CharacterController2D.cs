﻿using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [Range(0.5f, 5.0f)] [SerializeField] private float m_LadderCheckDistance = 0.5f;
    [SerializeField] private LayerMask m_WhatIsLadders;


    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private bool m_isClimbing = false;
    private bool m_isOverLadder = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
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
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    private void LateUpdate()
    {

    }

    public void Move(float move_x, float move_y, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        Vector3 ladderPos = LadderCheck();
        if (move_y > 0 && m_isOverLadder)
        {
            if (!m_isClimbing)
            {
                if (ladderPos != Vector3.zero)
                {
                    transform.position = new Vector3(ladderPos.x, transform.position.y, 0);
                }
                m_Rigidbody2D.velocity = Vector2.zero;
                m_Rigidbody2D.gravityScale = 0;
                m_isClimbing = true;
            }

        }
        else if(!m_isClimbing)
        {
            m_Rigidbody2D.gravityScale = 3;
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl || m_isClimbing)
        {
            // If crouching
            if (crouch && !m_isClimbing)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move_x *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = Vector2.zero;
            if (!m_isClimbing)
            {
                targetVelocity = new Vector2(move_x * 10f, m_Rigidbody2D.velocity.y);
            }
            else {
                ladderPos = LadderCheck();
                if (m_isOverLadder)
                {
                    transform.position = new Vector3(ladderPos.x, transform.position.y, 0);
                    targetVelocity = new Vector2(0, move_y * 4.0f);
                }
                else
                {
                    m_isClimbing = false;
                    m_Rigidbody2D.gravityScale = 3;
                }
            }
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move_x > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move_x < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
        if (m_isClimbing && jump) {
            m_Rigidbody2D.gravityScale = 3;
            m_isClimbing = false;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.AddForce(new Vector2(move_x * 5f, m_JumpForce/4));
        }
    }

    private Vector3 LadderCheck() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, m_LadderCheckDistance, m_WhatIsLadders);

        if (hitInfo.collider != null)
        {
            m_isOverLadder = true;
            return hitInfo.collider.gameObject.transform.position;
        }
        else {
            m_isOverLadder = false;
            return Vector3.zero;
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void Log(string s) {
        Debug.Log("Character Move Controller 2D: " + s);
    }
}