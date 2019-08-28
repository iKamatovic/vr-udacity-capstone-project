using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeDestroyer : MonoBehaviour {

    public UnityEvent CubeDestoryed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube") {
            Destroy(other.gameObject);
            CubeDestoryed.Invoke();
        }
    }
}
