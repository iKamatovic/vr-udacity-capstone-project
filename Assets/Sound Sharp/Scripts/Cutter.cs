using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    public static void Cut(Transform target, Vector3 hit, Color color) {
        Vector3 localScale = target.localScale;
        Vector3 localHitPoint = target.InverseTransformPoint(hit);
        Material mat = target.GetComponent<MeshRenderer>().material;
        

        float angle = Vector3.Angle(localHitPoint, target.transform.right);

        Vector3 topEdge = target.position + Vector3.up * localScale.y / 2;
        Vector3 rightEdge = target.position + Vector3.right * localScale.x / 2;
        Vector3 leftEdge = target.position + Vector3.left * localScale.x / 2;
        Vector3 bottomEdge = target.position + Vector3.down * localScale.y / 2;

        Vector3 leftSideCenter;
        Vector3 rightSideCenter;

        float targetWidth = rightEdge.x - leftEdge.x;
        float targetHeight = topEdge.y - bottomEdge.y;

        float box1Width;
        float box2Width;
        float box1Height;
        float box2Height;

         if (angle < 125f && angle > 55f)
        {
            box1Width = targetWidth * (0.5f + localHitPoint.x);
            box2Width = targetWidth - box1Width;

            box1Height = targetHeight;
            box2Height = targetHeight;

            leftSideCenter = new Vector3(hit.x - box1Width / 2, target.position.y, target.position.z);
            rightSideCenter = new Vector3(hit.x + box2Width / 2, target.position.y, target.position.z);
        }
        else {
            box1Width = targetWidth;
            box2Width = targetWidth;

            box2Height = targetHeight * (0.5f + localHitPoint.y);
            box1Height = targetHeight - box2Height;

            leftSideCenter = new Vector3(target.position.x, hit.y + box1Height / 2, target.position.z);
            rightSideCenter = new Vector3(target.position.x, hit.y - box2Height / 2, target.position.z);
        }


        Destroy(target.gameObject);

        mat.SetColor("_EmissionColor", color);

        GameObject box1Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box1Obj.transform.position = leftSideCenter;
        box1Obj.transform.localScale = new Vector3(box1Width, box1Height, localScale.z);
        box1Obj.AddComponent<Rigidbody>().mass = 100f;
        box1Obj.GetComponent<MeshRenderer>().material = mat;
        box1Obj.GetComponent<BoxCollider>().size = box1Obj.GetComponent<BoxCollider>().size * 1.2f;   
        Destroy(box1Obj, 0.5f);


        GameObject box2Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box2Obj.transform.position = rightSideCenter;
        box2Obj.transform.localScale = new Vector3(box2Width, box2Height, localScale.z);
        box2Obj.AddComponent<Rigidbody>().mass = 100f;
        box2Obj.GetComponent<MeshRenderer>().material = mat;
        box2Obj.GetComponent<BoxCollider>().size = box2Obj.GetComponent<BoxCollider>().size * 1.2f;
        Destroy(box2Obj, 0.5f);
    }
}
