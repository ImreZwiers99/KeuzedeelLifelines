using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TPCity : MonoBehaviour
{
    public Camera Camera;
    public TMP_InputField inputField;
    public Transform cityTree;
    public string city;

    private void Start()
    {
        
    }

    private void Update()
    {
        city = inputField.text.ToLower();
    }

    public void TpToCity()
    {
        cityTree = GameObject.Find(city).transform;
        
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, cityTree.position, Time.deltaTime);
    }
}
