using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    AudioSource audioSource;
    [SerializeField] AudioClip explosionSound;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":    // do nothing
                print("EXP Obstacle Collision - Audio");
                // audioSource.PlayOneShot(explosionSound);
                break;
            default:
                print("EXP Obstacle Collision - No Audio");
                break;
        }
    }
}
