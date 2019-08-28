using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;

	void Update () {
        GetComponent<Rigidbody>().velocity = -Vector3.forward * speed;		
	}
}
