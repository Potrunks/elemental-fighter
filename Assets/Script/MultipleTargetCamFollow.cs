using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamFollow : MonoBehaviour
{
    public List<Transform> players;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime;
    public float minZoom;
    public float maxZoom;
    public float zoomLimiter;
    private Camera cam;
    public static MultipleTargetCamFollow instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (players.Count == 0)
        {
            return;
        }
        CameraMove();
        CameraZoom();
    }

    private void CameraZoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        //cam.fieldOfView = newZoom;
        //cam.orthographicSize = newZoom;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private float GetGreatestDistance()
    {
        return NewBoundsAndEncapsulate().size.x;
    }

    private void CameraMove()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }
        return NewBoundsAndEncapsulate().center;
    }

    private Bounds NewBoundsAndEncapsulate()
    {
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform player in players)
        {
            bounds.Encapsulate(player.position);
        }
        return bounds;
    }
}
