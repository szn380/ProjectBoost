using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadShip : MonoBehaviour {
    // This class supports phase 1 of the ship explosion
    // It will play the associated collision audio file
    // and also display the explosion particle effects
    // After a timed delay, this class initiates the transitions to phase 2
    // of the explosion sequence.

    public int counter = 60;         // number of frames to delay/wait before transition to phase 2
    public Transform RocketShipDead; // reference to phase 2 prefab for the exploded/dead rocket
    
    [SerializeField] ParticleSystem deathParticles;  // reference to the explosion particle system
    AudioSource audioSource;                         // reference to the explosion audio source

    // play explosion auido and particles at start up
    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        deathParticles.Play();
    }
	
	// After delay, initiate transition to phase 2 of exploded ship
	void Update () {
        counter--;
        if (counter <= 0) {
            Instantiate(RocketShipDead, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
	}
}
