﻿using System.Collections;
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
                print("Obstacle Collision - Audio");
                audioSource.PlayOneShot(explosionSound);
                break;
            default:
                print("Obstacle Collision - No Audio");
                break;
        }
    }
}
