using System;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float autoRotateSpeed = 2f;
    public float autoRotateDelay = 5f; 

    private bool isRotating = false;
    private Vector3 initialMousePosition;
    private Vector3 rotation = Vector3.zero;

    private float timeSinceLastRotation = 0f; 
    private bool isAutoRotating = false;

    public Camera rayCastCamera;
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = rayCastCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("UI")))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    isRotating = true;
                    isAutoRotating = false;
                    timeSinceLastRotation = 0f;
                    initialMousePosition = Input.mousePosition;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating && Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - initialMousePosition;
            rotation.y = -deltaMousePosition.x;

            transform.Rotate(rotation * rotationSpeed * Time.deltaTime, Space.Self);

            initialMousePosition = Input.mousePosition;
            timeSinceLastRotation = 0f; 
        }
        else
        {
            timeSinceLastRotation += Time.deltaTime;

            if (timeSinceLastRotation > autoRotateDelay)
            {
                isAutoRotating = true;
                rotation.y = 1f;
            }

            if (isAutoRotating)
            {
                transform.Rotate(rotation * autoRotateSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}