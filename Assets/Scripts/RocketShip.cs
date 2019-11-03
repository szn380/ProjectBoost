using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float engineThrust = 100f;
    public Transform RocketShipExplosion;
    int loadSceneDelay = 0;
    public bool shipDestroyed = false; 

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();    // get access to the rigidbody component
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void Rotate()
    {
        rigidBody.freezeRotation = true;  // take manual control of rotation
        float rotaionThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {   
            transform.Rotate(Vector3.forward * rotaionThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotaionThisFrame);
        }

        rigidBody.freezeRotation = false;  // resume physics control
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":    // do nothing
                print("Rocket Harmless Collision");
                break;
            case "Finish":    // do nothing
                print("Rocket Finishes Level");
                SceneManager.LoadScene(1);
                break;
            default:
                print("Rocket Dead Collision");
                Instantiate(RocketShipExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                Destroy(gameObject);
                break;
        }
    }

    private void Thrust()
    {
        float thrustThisFrame = engineThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);   // use relative force so when rocket tilts force tilts as well
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
