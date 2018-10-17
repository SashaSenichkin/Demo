using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Toucher : MonoBehaviour {
    [SerializeField]private float rotateMult = 1;
    Vector2? savedPositionRotate;
    float? savedDistance;
    void Update () {
        Vector3 point;
        if (Input.touches.Length == 1|| (Application.isEditor && Input.anyKey))
        {
            if (savedPositionRotate == null)
            {
                savedPositionRotate = Application.isEditor? new Vector2( Input.mousePosition.y , Input.mousePosition.x) : Input.touches[0].position;
            }
            else
            {
                Vector2 newPosition = Application.isEditor ? new Vector2(Input.mousePosition.y, Input.mousePosition.x) : Input.touches[0].position;
                Vector2 rotate = (Vector2)savedPositionRotate - newPosition;
                this.transform.rotation = Quaternion.Euler(rotate.x* rotateMult, rotate.y* rotateMult, this.transform.rotation.z);
            }
        }
        else if (Input.touches.Length == 2)
        {
            if (savedDistance == null)
            {
                savedDistance = (Input.touches[0].position - Input.touches[1].position).magnitude;
            }
            else
            {
                float newDistance = (Input.touches[0].position - Input.touches[1].position).magnitude;
                this.transform.localScale *= newDistance/ (float)savedDistance;
            }
        }
        else
        {
            savedDistance = null;
            savedPositionRotate = null;
        }
	}
}
