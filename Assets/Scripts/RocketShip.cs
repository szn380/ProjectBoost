using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Vertical  and lateral movement controled by physics and applied forces (Rigidbody)
//    Z axis movement frozen using inspector rigidbody constraints; only move on X & Y plane
// Rotation controlled by direct assignment of rotation (transform values)
//    X & Y rotation frozen using inspector rigidbody constraints; only rotate around Z axis

public class RocketShip : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float engineThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip finishLevel;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem finishLevelParticles;


    public Transform RocketShipExplosion;
    int loadSceneDelay = 0;
    int sceneCounter = 0;
    int sceneMax = 5;  // number of last scene/level
    public bool shipDestroyed = false; 

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();    // get access to the rigidbody component
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
	}

    private void RespondToRotateInput()
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
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)   // gameObject is what we are colliding with
        {
            case "Friendly":    // do nothing
                break;
            case "Finish":    // do nothing
                finishLevelSequence();
                break;
            default:
                if (state != State.Dying)
                {
                    Instantiate(RocketShipExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    Invoke("LoadStartLevel", 1f);
                    Destroy(gameObject);
                }
                state = State.Dying;
                break;
        }
    }

    private void finishLevelSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(finishLevel);
        Invoke("LoadNextScene", 1f);  // delay starting this routine
        finishLevelParticles.Play();
    }

    private void LoadStartLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        sceneCounter = SceneManager.GetActiveScene().buildIndex;
        sceneCounter++;
        if (sceneCounter >= sceneMax) { sceneCounter = sceneMax;  }
        SceneManager.LoadScene(sceneCounter);  // todo: allow for more than two levels
    }

    private void RespondToThrustInput()
    {
        float thrustThisFrame = engineThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(thrustThisFrame);
        }
        else
        {
            audioSource.Stop(); 
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);   // use relative force so when rocket tilts force tilts as well
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
}
