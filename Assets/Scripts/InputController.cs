/*
Created By OFGONEN
*/
using UnityEngine;

public class InputController : MonoBehaviour {
    //Detects all the inputs and passes to PlayerController


    #region Variables
    public static InputController instance = null; // Singleton
    public PlayerController playerController; // Refence for player controller.
    public Joystick joystick;

    float _horizontalMove = 0f; // Initial value

    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {

        // Gets the raw value of input for horizontal. We want an arcadey feeling so we dont want acceleration.
        if(joystick.Horizontal >= .2f)
        {
            _horizontalMove = 1;
        }
        else if(joystick.Horizontal <= -.2f)
        {
            _horizontalMove = -1;
        }
        else
        {
            _horizontalMove = 0;
        }

        playerController.Move(_horizontalMove); // Passases the value to player controller to move.

        //// You cant jump and fall down at the same time. So we handeled it with else if so  that only one input can passeses to player controller.
        //if (Input.GetButtonDown("Jump"))
        //{
        //    Debug.Log("Jump");
        //    playerController.Jump(); // Lets player controller to jump.
        //}
        //else if (Input.GetButtonDown("Crouch"))
        //{
        //    Debug.Log("Crouch");
        //    playerController.Crouch(); // Lets player controller to crouch so it can fall down from a platform.
        //}
    }

    #region Methods

    public void Jump()
    {
        playerController.Jump();
    }

    public void Crouch()
    {
        playerController.Crouch();
    }
    #endregion
}
