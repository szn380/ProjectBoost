using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadShip : MonoBehaviour {
    public int counter = 30;
    public Transform RocketShipDead;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter--;
        if (counter <= 0) {
            Instantiate(RocketShipDead, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
	}
}
