using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]  // allow only one script for the gameobject the script is assigned to
public class Oscillator : MonoBehaviour {
	[SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
	[SerializeField] float period = 2f;

	[Range(0,1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved
	Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		float cycles = Time.time / period;
		const float tau = Mathf.PI * 2;  // 6.28...
		float rawSinWave = Mathf.Sin(cycles * tau);

		// print(rawSinWave);
		movementFactor = rawSinWave / 2f + 0.5f;
		Vector3 offset = movementVector * movementFactor;

		transform.position = startingPosition + offset;
		
	}
}
