using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{

    public Transform target;

    Vector3 targetPos;
    public Vector3 offset;

    Vector2 mousePos2;
    private Camera cam;
    Vector3 mousePos3;
    public float cameraScrollToSpeed;

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        mousePos2.x = Input.mousePosition.x;
        mousePos2.y = Input.mousePosition.y;

        mousePos3 = cam.ScreenToWorldPoint(new Vector3(mousePos2.x, mousePos2.y, 10));
        targetPos = Vector3.Lerp(target.position, new Vector3(mousePos3.x, mousePos3.y + 0.5f, 10), 0.1f);
        transform.position = Vector3.Lerp(transform.position, targetPos + offset, cameraScrollToSpeed * Time.deltaTime);
        
    }
}
