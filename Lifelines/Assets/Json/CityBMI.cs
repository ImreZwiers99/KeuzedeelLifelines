using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityBMI : MonoBehaviour
{
    public Toggle bmiToggle;
    private InstantiateZipcodes manager;

    void Start()
    {
        manager = GetComponent<InstantiateZipcodes>();

        if (bmiToggle == null)
        {
            Debug.LogError("No toggle assigned for BMI scaling in the Inspector.");
            return;
        }

        if (manager.minBMI == float.MaxValue || manager.maxBMI == float.MinValue)
        {
            Debug.LogWarning("BMI values not properly initialized in the InstantiateZipcodes manager.");
        }
        else
        {
            Debug.Log($"Min BMI: {manager.minBMI}, Max BMI: {manager.maxBMI}");
        }

        bmiToggle.onValueChanged.AddListener(OnBMIToggleChanged);
    }

    void OnBMIToggleChanged(bool isScaled)
    {
        foreach (var (zipcodeObject, _, _, bmiValue, _) in manager.zipcodeObjects)
        {
            Transform treeTransform = zipcodeObject.transform.GetChild(0);
            if (treeTransform == null) continue;

            Vector3 scale = treeTransform.localScale;
            TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();

            if (isScaled)
            {
                float normalizedBMI = 0f;
                if (manager.maxBMI != manager.minBMI)
                {
                    normalizedBMI = (bmiValue - manager.minBMI) / (manager.maxBMI - manager.minBMI);
                }

                float scaleFactor = Mathf.Lerp(1f, 2f, Mathf.Clamp01(normalizedBMI));
                scale.x = scaleFactor;
                scale.z = scaleFactor;

                if (textMesh != null)
                {
                    textMesh.text += $"\nBMI: {bmiValue / 100f:F2}";
                }
            }
            else
            {
                scale.x = 1f;
                scale.z = 1f;

                if (textMesh != null)
                {
                    textMesh.text = textMesh.text.Replace($"\nBMI: {bmiValue / 100f:F2}", "");
                }
            }

            treeTransform.localScale = scale;
        }
    }
}
