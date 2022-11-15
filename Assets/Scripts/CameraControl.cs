using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{

    public float mouseSensivity;
    float xAxisClamp=0;
    public Transform playerBody;
    public bool canMoveCam = true;
    public static CameraControl camControlCode;
    private Vector3 left;
    private Vector3 up;

    private void Awake()
    {
        LockCursor();
        camControlCode = this;
    }


    void Update()
    {
        if (canMoveCam)
        {
            CameraRotation();
        }
    }

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if(xAxisClamp>90)
        {
            xAxisClamp = 90;
            mouseY = 0;
            ClampxAxisRotationToValue(270);
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            mouseY = 0;
            ClampxAxisRotationToValue(90);
        }
        
        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }


    void ClampxAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
