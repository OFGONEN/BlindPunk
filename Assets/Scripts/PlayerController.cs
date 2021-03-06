/*
Created By OFGONEN
*/
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    public Rigidbody2D rb; // Rigidbody of player.
    public BoxCollider2D collider; // Collider of player.
    public Transform _cord_bottom; // Cordinate of feet of player.
    public Animator _anim_player; // Animator of the player.
    public float _amount_speed; // Speed of movement.
    public float _amount_jump; // Jump force. It decides of heigh player can jump.

    bool _is_grounded = true; // Is player on the ground and touches the ground.
    bool _is_facingRight = true; // Is player faces right.
    bool _is_cover_on; // Is visual black cover is on .

    bool _can_move = true; // Can player move.

    #endregion

    private void FixedUpdate()
    {
        AnimationControl();
    }

    #region Methods
    public void Move(float _horizontalMove)
    { 
        // If player cant move , either if it is dead or paused.
        if (_can_move)
        {
            rb.velocity = new Vector2(_horizontalMove * _amount_speed, rb.velocity.y); // Changes the horizontal speed of player without changing its vertical speed.
            
            if ( _is_cover_on && (Mathf.Approximately(rb.velocity.x, 0) && Mathf.Approximately(rb.velocity.y, 0)) ) // If we are stand still and there is visual cover on , we need to turn visual cover off.
            {
                //Uncover effect
                VisualCoverControl.instance.ChangeSign(); 
                _is_cover_on = false;
            }
            else if (rb.velocity != Vector2.zero && !_is_cover_on) // If we are moving and there is no visual cover , we need to turn visual cover on.
            {
                //Cover effect
                VisualCoverControl.instance.ChangeSign();
                _is_cover_on = true;
            }

            if (_horizontalMove > 0 && !_is_facingRight) // If we are facing left and we are moving to right we need to flip ourself to turn right side.
            {
                Flip();
            }
            else if (_horizontalMove < 0 && _is_facingRight) // If we are facing right and we are moving to left we need to flip ourself to turn left side.
            {
                Flip();
            }
        }

    }

    public void Jump()
    {
        if (_is_grounded && _can_move) // If we are touching the ground we can deffinitly jump.
        {
            _is_grounded = false;
            rb.AddForce(new Vector2(0f, _amount_jump)); // We are handling jumping with giving our player force in vertical.
        }
    }

    public void Crouch()
    {
        // If we are touching ground and we are standing above a platform , we can crouch and fall from that platform.
        if (_is_grounded && _can_move && Physics2D.OverlapBox(_cord_bottom.position , Vector2.one, 0 , 2048)) 
        {
            _is_grounded = false; // Since we are falling we need to declare ourself as airbone.
            collider.isTrigger = true;

        }
    }

     

    void Flip()
    {
        _is_facingRight = !_is_facingRight; // Prety basic.

        // Since we cant directly change a single value of a vector , we need to create a new one with values of the vector we want to change
        // in this situation we need transform.localScale.

        Vector3 scale = transform.localScale; 
        scale.x *= -1; // We need to change the sign of the x valuse , so we are multiplying with -1 everytime.
        transform.localScale = scale;
    }

    void Stop()
    {
        //Makes the player stop , even its in the air. So that it can fall with gravitys help.
        Debug.Log("Stoped");
        _can_move = false;
        rb.velocity = Vector2.zero;
    }

    void AnimationControl()
    {
        _anim_player.SetFloat("VerticalSpeed", Mathf.Abs(rb.velocity.y));

        if (Mathf.Approximately(rb.velocity.y, 0))
        {
            if (Mathf.Approximately(rb.velocity.x, 0))
            {
                _anim_player.SetTrigger("Idle");
            }
            else
            {
                _anim_player.SetTrigger("Running");
            }
        }
        else
        {

            if (transform.parent == null)
            {
                _anim_player.SetTrigger("Jumping");
            }
            else
            {
                if (Mathf.Approximately(rb.velocity.x, 0))
                {
                    _anim_player.SetTrigger("Idle");
                }
                else
                {
                    _anim_player.SetTrigger("Running");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if(col.gameObject.layer == 10) // If we collide with a trap , kill the player and end the game.
        {
            if (_can_move)
            {
                _can_move = false;
                Stop();
                _anim_player.SetTrigger("Dying"); // We are dead.
                // GameLogic Endgame
                Loader.instance.RestartAfterDelay(5);
            }
        }
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        //If we are on the air and we are colliding with groundd or a platfrom , which means that we are on the ground now.
        if (collision.gameObject.tag == "GroundReset")
        {
            _is_grounded = true;
        }
    }

    // This method works for player to get collide after the it fall down from a platform.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            collider.isTrigger = false;
        }
    }
    #endregion
}
