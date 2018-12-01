using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {
	public Camera cam;
	public GameObject player;
	public float speed;

    //  Follows the player with smooth moving.
	private void FixedUpdate()
	{
		cam.transform.position = Vector3.Lerp( cam.transform.position, new Vector3( player.transform.position.x, player.transform.position.y, cam.transform.position.z ), Time.deltaTime * speed );
	}
}
