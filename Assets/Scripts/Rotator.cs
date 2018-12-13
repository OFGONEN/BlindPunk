/*
Created By OFGONEN
*/
using UnityEngine;

public class Rotator : MonoBehaviour {

    #region Variables
    public Rigidbody2D rb;
    public float speed_tork;
	#endregion


	

	void FixedUpdate ()
	{
        rb.MoveRotation(rb.rotation + Time.deltaTime * speed_tork);
    }

	#region Methods
	#endregion
	}
