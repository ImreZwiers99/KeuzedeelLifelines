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
        foreach (var (zipcodeObject, _, ageValue) in manager.zipcodeObjects)
        {
            Transform firstChild = zipcodeObject.transform.GetChild(0);
            if (firstChild != null)
            {
                firstChild.gameObject.SetActive(!isDisabled);
            }

            Transform secondChild = zipcodeObject.transform.GetChild(1);
            if (secondChild != null)
            {
                secondChild.gameObject.SetActive(isDisabled);

                if (isDisabled)
                {
                    float normalizedAge = (ageValue - manager.minAge) / (manager.maxAge - manager.minAge);
                    Color interpolatedColor = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(normalizedAge));

                    Renderer renderer = secondChild.GetComponent<Renderer>();
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
