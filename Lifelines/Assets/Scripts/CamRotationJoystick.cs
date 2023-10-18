using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotationJoystick : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystickRotation;

    public float moveSpeed;

    public Transform mainCamera;

    // Update is called once per frame
    void Update()
    {
        RotationLogic();
    }

    private void RotationLogic()
    {
        float horizontalInput = joystickRotation.Horizontal;
        float verticalInput = joystickRotation.Vertical;

        Vector3 rotationInput = new Vector3(-verticalInput, horizontalInput, 0);
        mainCamera.Rotate(rotationInput * moveSpeed * Time.deltaTime);

        Vector3 currentRotation = mainCamera.localEulerAngles;
        currentRotation.z = 0;
        mainCamera.localEulerAngles = currentRotation;
    }
}
