using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{

    
    public float smoothness;

    private Vector3 velocity = Vector3.zero;

    private Transform target;
    private Camera camera;
    private CameraBoundPoints camBound;
    private Vector3 boundLength;

    void Start()
    {
        target = null;
        if (target == null)
            target = GameObject.Find("CarPlaceholder").transform;

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        camBound = GameObject.Find("CameraBoundPoints").GetComponent<CameraBoundPoints>();


        //calculate the distance in world space of top and left from the centre
        Vector3 lowerLeftBounds = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.transform.position.y));
        boundLength = new Vector3(transform.position.x - lowerLeftBounds.x, 0, transform.position.z - lowerLeftBounds.z);

    }

        // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 targetPosCopy = new Vector3(target.position.x, transform.position.y, target.position.z);

        #region Calculate Bounds

        //Calculate the bottomLeft Point for the screen bounds for the camera
        //which in world space will be used for left and lower bounds for the camera
        Vector3 lowerLeftBounds = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.transform.position.y));

        //Calculate the upper right Point for the screen bounds for the camera
        //which in world space will be used for right and upper bounds for the camera
        Vector3 upperRightBounds = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.transform.position.y));


        //distance of camera from bounds
        float leftBoundCam = (camBound.GetLeft() - lowerLeftBounds.x);
        float bottomBoundCam = (camBound.GetBottom() - lowerLeftBounds.z);
        float rightBoundCam = (upperRightBounds.x - camBound.GetRight());
        float topBoundCam = (upperRightBounds.z - camBound.GetTop());


        //if car is in bounds
        float distCarFromBoundsHorizontal = Mathf.Min(target.position.x - camBound.GetLeft(), camBound.GetRight() - target.position.x);
        float distCamFromBoundsHorizontal = Mathf.Min(transform.position.x - camBound.GetLeft(), camBound.GetRight() - transform.position.x);

        float distCarFromBoundsVertical = Mathf.Min(target.position.z - camBound.GetBottom(), camBound.GetTop() - target.position.z);
        float distCamFromBoundsVertical = Mathf.Min(transform.position.z - camBound.GetBottom(), camBound.GetTop() - transform.position.z);

        /*
        if (leftBoundCam >= 0 || rightBoundCam >= 0)
            targetPos.x = transform.position.x;

        if (distCarFromBoundsHorizontal > distCamFromBoundsHorizontal)
            targetPos.x = targetPosCopy.x;

        if ((topBoundCam >= 0 || bottomBoundCam >= 0))
            targetPos.z = transform.position.z;

        if(distCarFromBoundsVertical > distCamFromBoundsVertical)
            targetPos.z = targetPosCopy.z;
        */

        #endregion


        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothness);

        if (leftBoundCam >= 0 && distCarFromBoundsHorizontal < distCamFromBoundsHorizontal)
            transform.position = new Vector3(lowerLeftBounds.x + boundLength.x, transform.position.y, transform.position.z);
        if (rightBoundCam >= 0 && distCarFromBoundsHorizontal < distCamFromBoundsHorizontal)
            transform.position = new Vector3(upperRightBounds.x - boundLength.x, transform.position.y, transform.position.z);

        if (bottomBoundCam >= 0 && distCarFromBoundsVertical < distCamFromBoundsVertical)
            transform.position = new Vector3( transform.position.x, transform.position.y, lowerLeftBounds.z + boundLength.z);
        if (topBoundCam >= 0 && distCarFromBoundsVertical < distCamFromBoundsVertical)
            transform.position = new Vector3(transform.position.x, transform.position.y, upperRightBounds.z - boundLength.z);

    }

}
