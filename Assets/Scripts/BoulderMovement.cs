using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMovement : MonoBehaviour {

	public Rigidbody2D rb;

    public float speed; // The min speed that we want our boulder to travel
	public float lifespan; // Life span of our bolder.

	private float lifeCounter = 0;

    private void Update()
    {
        lifeCounter += Time.deltaTime; 

        if (lifeCounter > lifespan)
            Destroy(gameObject); //If it exits in the world more then its life span , destroy it.
    }

    void FixedUpdate () {

        // When ever our boulder is slower then our intented speed , assign intented speed to boulder.
        // This way we solve the problem of vertical falling.When ever a boulder falls directly vertical , it has 0 horizontal speed
        // But this way even if its fall flat , this method assing a horizontal speed immediately
        if (rb.velocity.x < speed )
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
        }

    }


}
