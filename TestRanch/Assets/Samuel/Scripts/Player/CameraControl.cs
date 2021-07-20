using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform player;
    private bool isLocked = true;

    private float xRotation = 0f;

    void Start()
    {
        LockCursor();
    }


    private void Update()
    {
        if (!isLocked)
            AxisRotation();

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isLocked)
                UnlockCamera();
            else
                LockCamera();
        }*/
    }

    private void AxisRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70, 70);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isLocked = true;
    }
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isLocked = false;
    }

    public bool IsCameraLocked()
    {
        return isLocked;
    }
    
}
