using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightSaber : MonoBehaviour {

    private LineRenderer line;
    private bool traced = false;
    

    public LayerMask cubeLayer;
    public Transform start;
    public Transform end;
    public Color cutColor;
    public UnityEvent OnHit;
    public UnityEvent OnMiss;

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        Material material = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (Physics.Raycast(start.position, (end.position - start.position), out hit, Vector3.Distance(start.position, end.position), cubeLayer))
        {
            if (!traced)
            {
                if (hit.transform.InverseTransformPoint(hit.point).y > 0.4f)
                {
                    OnHit.Invoke();
                }
                else {
                    OnMiss.Invoke();
                }

                Cutter.Cut(hit.transform, hit.point, cutColor);

                traced = true;
            }
        }
        else {
            traced = false;
        }

        


        line.SetPosition(0, start.position);
        line.SetPosition(1, end.position);
    }
}
