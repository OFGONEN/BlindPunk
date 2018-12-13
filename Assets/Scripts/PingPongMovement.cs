/*
Created By OFGONEN
*/
using UnityEngine;

public class PingPongMovement : MonoBehaviour {

    public delegate void PingPongMovementFunction();

    #region Variables
    public enum MovementDirection { Vertical , Horizontal};

    public Rigidbody2D rb;
    public MovementDirection Direction;
    public float speed_movement;
    public float distance_movement;

    Vector2 pos_start;
    Vector2 pos_end;

    PingPongMovementFunction f;

	#endregion


	void Start ()
	{
        pos_start = transform.position;

        if (Direction == 0)
        {
            pos_end = pos_start + Vector2.up * distance_movement;
            f = Vertical;
            rb.velocity = Vector2.up * speed_movement;
        }
        else
        {
            pos_end = pos_start + Vector2.right * distance_movement;
            f = Horizontal;
            rb.velocity = Vector2.right * speed_movement;
        }

        Debug.Log(pos_start);
        Debug.Log(pos_end);
        Debug.Log(rb.velocity);
    }

    private void FixedUpdate()
    {
        f();
    }

    #region Methods
    

    void Vertical()
    {
        if(rb.position.y >= pos_end.y)
        {
            rb.velocity = Vector2.down * speed_movement;
        }
        else if(rb.position.y <= pos_start.y)
        {
            rb.velocity = Vector2.up * speed_movement;
        }
    }

    void Horizontal()
    {
        if (rb.position.x >= pos_end.x)
        {
            rb.velocity = Vector2.left * speed_movement;
        }
        else if (rb.position.x <= pos_start.x)
        {
            rb.velocity = Vector2.right * speed_movement;
        }
    }

    #endregion
}
