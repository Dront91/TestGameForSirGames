using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public Vector2 ReferenceResolution = new Vector2(1920, 1080);
    private Camera componentCamera;
    private float targetAspect;
    private float cameraZoom = 1;
    private float initialSize;
    void Start()
    {
        componentCamera = GetComponent<Camera>();
        initialSize = componentCamera.orthographicSize;
        targetAspect = ReferenceResolution.x / ReferenceResolution.y;
    }
    void Update()
    {
        componentCamera.orthographicSize = initialSize * (targetAspect / componentCamera.aspect) / cameraZoom;
    }
}
