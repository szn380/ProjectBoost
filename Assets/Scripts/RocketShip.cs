using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
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
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip finishLevelSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem finishLevelParticles;


    public Transform RocketShipExplosion;
    public bool shipDestroyed = false;
    bool collisionMode = true;       // set to true if collisions should be recognized / processed

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
        if (state == State.Alive)
        {
            if (Debug.isDebugBuild)
            {
                respondToDebugKeys();
            }
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void respondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))  // load next level
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.C))   // disable / enable debug code
        {
            collisionMode = !collisionMode;
        }
    }

    private void RespondToRotateInput()
    {
        // rigidBody.freezeRotation = true;  // take manual control of rotation
        rigidBody.angularVelocity = Vector3.zero;  // remove rotation due to physics
        float rotaionThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {   
            transform.Rotate(Vector3.forward * rotaionThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotaionThisFrame);
        }

        // rigidBody.freezeRotation = false;  // resume physics control
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state == State.Alive && collisionMode)
        {
            switch (collision.gameObject.tag)   // gameObject is what we are colliding with
            {
                case "Friendly":    // do nothing
                    break;
                case "Finish":    // do nothing
                    finishLevelSequence();
                    break;
                default:
                    if (state == State.Alive)
                    {
                        Instantiate(RocketShipExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                        Destroy(gameObject);
                    }
                    state = State.Dying;
                    break;
            }
        }
    }

    private void finishLevelSequence()
    {
        state = State.Transcending; 
        audioSource.Stop();
        audioSource.PlayOneShot(finishLevelSound);
        Invoke("LoadNextScene", levelLoadDelay);  // delay starting this routine
        finishLevelParticles.Play();
    }

    private void LoadNextScene()
    {
        int sceneCounter = SceneManager.GetActiveScene().buildIndex;
        int sceneMax = SceneManager.sceneCountInBuildSettings;
        int sceneMaxFlightSchool = SceneManager.sceneCountInBuildSettings;
        GameData.playerFinalLevel = SceneManager.GetActiveScene().buildIndex;
        sceneCounter++;
        if (GameData.flightSchool)
        {
            if (sceneCounter > sceneMaxFlightSchool)
            {
                SceneManager.LoadScene(sceneCounter-1);
            }
            else
            {
                SceneManager.LoadScene(sceneCounter);
            }
        }
        if (sceneCounter >= sceneMax) { sceneCounter = 0;  }
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
            audioSource.PlayOneShot(mainEngineSound);
        }
        mainEngineParticles.Play();
    }
}
