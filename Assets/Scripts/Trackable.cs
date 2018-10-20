using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Trackable : DefaultTrackableEventHandler
{
    public trackableType myType;
    [SerializeField]private float rotateMult = 1;
    [SerializeField]
    private float scaleMax = 3;
    [SerializeField]
    private float scaleMin = 0.5f;
    Vector2? savedPositionRotate;
    float? savedDistance;
    [SerializeField]
    private GameObject modelPrefab;
    private GameObject modelInstance;
    void Update () {
        if (modelInstance == null)
        {
            return;
        }
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
                modelInstance.transform.localRotation = Quaternion.Euler(rotate.x * rotateMult, rotate.y * rotateMult, modelInstance.transform.localRotation.z);
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
                float calcMult = (Input.touches[0].position - Input.touches[1].position).magnitude;
                float newScale = modelInstance.transform.localScale.x *calcMult / (float)savedDistance;
                print("newDistance " + newScale);
                if (newScale > scaleMin && newScale < scaleMax)
                {

                    modelInstance.transform.localScale = new Vector3(newScale, newScale, newScale) ;
                }                
            }
        }
        else
        {
            savedDistance = null;
            savedPositionRotate = null;
        }
	}
    protected override void OnTrackingFound()
    {
        if (modelInstance != null)
        {
            Destroy(modelInstance);
        }
        base.OnTrackingFound();
        modelInstance = Instantiate(modelPrefab, this.transform);
        Manager.Instance.OnTrackableDetect();
        Manager.Instance.SetTextByType(myType);
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if (modelInstance == null)
        {
            return;
        }
        Destroy(modelInstance);
        modelInstance = null;
        Manager.Instance.OnTrackableLost();
    }
    public void PositionReset()
    {
        if (modelInstance == null)
        {
            return;
        }
        Destroy(modelInstance);
        modelInstance = Instantiate(modelPrefab, this.transform);
    }
}
