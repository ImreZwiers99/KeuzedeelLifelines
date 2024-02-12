using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatAreEmotions : MonoBehaviour
{
    public Animator canvasAnimator;
    public GameObject mainCamera, pageCamera, canvasParent;
    public bool cameraSwitch = false;

    void Start() => cameraSwitch = false;

    void Update() => CameraSwitchLogic();

    public void OnClickToPage() => canvasAnimator.SetBool("InfoPage", true);

    public void OnClickToMainCam() => canvasAnimator.SetBool("InfoPage", false);

    private void CameraSwitchLogic()
	{
        if (cameraSwitch)
		{
            mainCamera.SetActive(false);
            pageCamera.SetActive(true);
            canvasParent.SetActive(false);
		}
		else
		{
            mainCamera.SetActive(true);
            pageCamera.SetActive(false);
            canvasParent.SetActive(true);
		}
	}
}
