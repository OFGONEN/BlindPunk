/*
Created By OFGONEN
*/
using UnityEngine;

public class InputController : MonoBehaviour {

    #region Variables
    public static InputController instance = null;
    public PlayerController playerController;

    float _horizontalMove = 0f;

    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
	
	void Update () 
	{
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        
         playerController.Move(_horizontalMove);
        

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            playerController.Jump();
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            Debug.Log("Crouch");
            playerController.Crouch();
        }



    }

    #region Methods
    #endregion
}
