using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoulder : MonoBehaviour {

	public GameObject boulderPrefab; 
	public float boulderThrowRate; // Interval that boulder will be thrown at.
    public Vector3 position; 
	float counter = 0;

	void Update () {
		
        //Basic cooldown.
		if(counter + boulderThrowRate < Time.time)
		{
			counter = Time.time;
			Instantiate( boulderPrefab, position, Quaternion.identity);
		}


	}
}
