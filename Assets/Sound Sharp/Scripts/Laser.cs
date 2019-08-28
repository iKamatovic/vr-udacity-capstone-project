using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnHitEvent : UnityEvent<GameObject> {} 

public class Laser : MonoBehaviour {

    public bool visibleByDefault = false;

    private LineRenderer line;
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        gameObject.SetActive(visibleByDefault);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        line.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            line.SetPosition(1, hit.point);
            target = hit.transform.gameObject;
        } else {
            line.SetPosition(1, transform.forward * 100);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public GameObject getTarget() {
        return target; 
    }

}
