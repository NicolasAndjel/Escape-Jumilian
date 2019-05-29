using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public enum CameraState
    {
        ON,
        OFF
    }
    public Transform[] targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    public Camera cam;
    private Vector3 velocity;
    public CameraState state;
    public float minZoom = 7f;
    public float maxZoom = 3f;

    private void Start()
    {
        state = CameraState.ON;
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                {   
                    state = CameraState.OFF;
                }
            }
        if (state == CameraState.ON)
        {
            if (targets.Length == 0) return;

            Move();
            Zoom();
        }
        
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 15);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    private void Move()
    {
        Vector3 centerPiont = GetCenterPoint();

        Vector3 newPosition = centerPiont + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        
        if (targets.Length == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

}
