using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityHeight : MonoBehaviour
{
    public Toggle heightToggle;
    private InstantiateZipcodes manager;

    void Start()
    {
        manager = GetComponent<InstantiateZipcodes>();

        if (heightToggle == null)
        {
            Debug.LogError("No toggle assigned for height scaling in the Inspector.");
            return;
        }

        if (manager.minHeight == float.MaxValue || manager.maxHeight == float.MinValue)
        {
            Debug.LogWarning("Height values not properly initialized in the InstantiateZipcodes manager.");
        }
        else
        {
            Debug.Log($"Min Height: {manager.minHeight}, Max Height: {manager.maxHeight}");
        }

        heightToggle.onValueChanged.AddListener(OnHeightToggleChanged);
    }

    void OnHeightToggleChanged(bool isScaled)
    {
        var allZipcodes = FindObjectsOfType<ZipcodeDataComponent>();

        foreach (var dataComponent in allZipcodes)
        {
            Transform treeTransform = dataComponent.transform.GetChild(0);
            if (treeTransform == null) continue;

            Transform child1 = dataComponent.transform.GetChild(1);

            Vector3 treeScale = treeTransform.localScale;

            Vector3 child1Position = child1.localPosition;

            TextMeshPro textMesh = dataComponent.GetComponentInChildren<TextMeshPro>();

            if (isScaled)
            {
                float normalizedHeight = 0f;
                if (manager.maxHeight != manager.minHeight)
                {
                    normalizedHeight = (dataComponent.heightValue - manager.minHeight) / (manager.maxHeight - manager.minHeight);
                }

                float scaleFactor = Mathf.Lerp(1f, 2f, Mathf.Clamp01(normalizedHeight));

                treeScale.y = scaleFactor;

                if (child1 != null)
                {
                    child1Position.y *= scaleFactor;
                }

                if (textMesh != null)
                {
                    textMesh.text += $"\nHeight: {dataComponent.heightValue / 100f:F2} cm";
                }
            }
            else
            {
                treeScale.y = 1f;

                if (child1 != null)
                {
                    child1Position.y = 7f;
                }

                if (textMesh != null)
                {
                    textMesh.text = textMesh.text.Replace($"\nHeight: {dataComponent.heightValue / 100f:F2} cm", "");
                }
            }

            treeTransform.localScale = treeScale;
            if (child1 != null)
            {
                child1.localPosition = child1Position;
            }
        }
    }

}
