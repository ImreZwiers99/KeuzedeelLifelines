using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityAge : MonoBehaviour
{
    public Toggle colorToggle;
    private InstantiateZipcodes manager;

    void Start()
    {
        manager = GetComponent<InstantiateZipcodes>();

        if (colorToggle == null)
        {
            Debug.LogError("No toggle assigned for coloring in the Inspector.");
            return;
        }

        colorToggle.onValueChanged.AddListener(OnDisableChildToggleChanged);
    }

    void OnDisableChildToggleChanged(bool isDisabled)
    {
        foreach (var (zipcodeObject, _, ageValue, _, _) in manager.zipcodeObjects)
        {
            Transform treeTransform = zipcodeObject.transform.GetChild(0);
            if (treeTransform == null) continue;

            Transform child0 = treeTransform.GetChild(0);
            Transform child1 = treeTransform.GetChild(1);

            if (child0 != null)
            {
                child0.gameObject.SetActive(!isDisabled);
            }

            if (child1 != null)
            {
                child1.gameObject.SetActive(isDisabled);

                if (isDisabled)
                {
                    float normalizedAge = (ageValue - manager.minAge) / (manager.maxAge - manager.minAge);
                    Color interpolatedColor = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(normalizedAge));

                    Renderer renderer = child1.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Material newMaterial = new Material(renderer.sharedMaterial);
                        newMaterial.color = interpolatedColor;
                        renderer.material = newMaterial;
                    }
                }
            }
            TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null)
            {
                if (isDisabled)
                {
                    textMesh.text += $"\nLeeftijd: {ageValue / 100f:F2} jaar";
                }
                else
                {
                    textMesh.text = textMesh.text.Replace($"\nLeeftijd: {ageValue / 100f:F2} jaar", "");
                }
            }
        }
    }
}
