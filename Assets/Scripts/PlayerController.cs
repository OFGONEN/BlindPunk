/*
Created By OFGONEN
*/
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public Transform _cord_bottom;

    public float _amount_speed;
    public float _amount_jump;

    bool _is_grounded = true;
    bool _is_facingRight = true;
    bool _is_cover_on;

    bool _can_move = true;

	#endregion

    #region Methods
    public void Move(float _horizontalMove)
    {
        if (_can_move)
        {
            rb.velocity = new Vector2(_horizontalMove * _amount_speed, rb.velocity.y);

            if (rb.velocity == Vector2.zero && _is_cover_on)
            {
                //remove cover.
                _is_cover_on = false;
            }
            else if (rb.velocity != Vector2.zero && !_is_cover_on)
            {
                // open cover
                _is_cover_on = true;
            }

            if (_horizontalMove > 0 && !_is_facingRight)
            {
                Flip();
            }
            else if (_horizontalMove < 0 && _is_facingRight)
            {
                Flip();
            }
        }

    }

    public void Jump()
    {
        if (_is_grounded)
        {
            _is_grounded = false;
            rb.AddForce(new Vector2(0f, _amount_jump));
        }
    }

    public void Crouch()
    { 
        if( _is_grounded && Physics2D.OverlapBox(_cord_bottom.position, Vector2.one, LayerMask.NameToLayer("Platform")))
        {
            _is_grounded = false;
            collider.isTrigger = true;
        }
    }

     

    void Flip()
    {
        _is_facingRight = !_is_facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_is_grounded && (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform") && rb.velocity.y == 0)
        {
            _is_grounded = true;
        }
        else if(collision.gameObject.tag == "Trap")
        {
            _can_move = false;
            Stop();
            // Endgame
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            collider.isTrigger = false;
        }
    }
    #endregion
}
