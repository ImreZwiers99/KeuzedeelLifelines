using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityDEP : MonoBehaviour
{
    public Toggle DEPToggle;
    private InstantiateZipcodes manager;
    public GameObject Rain;
    public Material rainSky;
    public Material normalSky;

    void Start()
    {
        manager = GetComponent<InstantiateZipcodes>();

        DEPToggle.onValueChanged.AddListener(OnDEPToggleChanged);
    }

    void OnDEPToggleChanged(bool isToggled)
    {
        foreach (var (zipcodeObject, _, _, _, DEPValue) in manager.zipcodeObjects)
        {
            TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();

            if (isToggled)
            {
                if (textMesh != null)
                {
                    textMesh.text += $"\nDepressief: {DEPValue / 100f:F2}%";
                }
            }
            else
            {
                if (textMesh != null)
                {
                    textMesh.text = textMesh.text.Replace($"\nDepressief: {DEPValue / 100f:F2}%", "");
                }
            }
        }

        if (isToggled)
        {
            Rain.SetActive(true);
            RenderSettings.skybox = normalSky;
        }
        else
        {
            Rain.SetActive(false);
            RenderSettings.skybox = rainSky;
        }
    }
}
