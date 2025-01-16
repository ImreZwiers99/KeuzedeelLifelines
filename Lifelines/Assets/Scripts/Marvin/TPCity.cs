using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TPCity : MonoBehaviour
{
    public Camera mainCamera;
    public TMP_InputField inputField;

    private Transform snapPoint, cityTree;
    private string city;

    private void Start()
    {
        
    }

    private void Update()
    {
        city = inputField.text;
    }

    public void TpToCity()
    {
        CamRotationJoystick.currentRotation = new Vector3(50, 0, mainCamera.transform.rotation.z);

        cityTree = GameObject.Find(city).transform;
        snapPoint = cityTree.FindChild("SnapPoint");

        mainCamera.transform.position = snapPoint.position;
    }
}
