using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Handles Input and sends necessary commands to the active character controller
    float horiz = 0.0f;
    float vert = 0.0f;
    bool Jump = false;
    bool Crouch = false;
    public float characterSpeedMultiplier = 100;
    public bool acceptInput = true;

    public CharacterController2D activeController;

    void Update()
    {
        if (acceptInput)
        {
            horiz = Input.GetAxisRaw("Horizontal") * characterSpeedMultiplier; // Multiply by the active characters speed
            vert = Input.GetAxisRaw("Vertical") * characterSpeedMultiplier;

            if (Input.GetButtonDown("Jump"))
            {
                Jump = true;
            }
            if (Input.GetButtonDown("Crouch"))
            {
                Crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                Crouch = false;
            }
        }
    }

    public void SetAcceptInput(bool val) {
        acceptInput = val;
        if (!val) {
            horiz = 0.0f;
            vert = 0.0f;
            Jump = false;
            Crouch = false;
        }
    }

    private void FixedUpdate()
    {
        activeController.Move(horiz * Time.fixedDeltaTime, vert * Time.fixedDeltaTime, Crouch, Jump);
        Jump = false;
    }
}
