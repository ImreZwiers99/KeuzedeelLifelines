using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityHeight : MonoBehaviour
{
    public Toggle scaleToggle;
    private InstantiateZipcodes manager;

    void Start()
    {
        manager = GetComponent<InstantiateZipcodes>();

        if (scaleToggle == null)
        {
            Debug.LogError("No toggle assigned for scaling in the Inspector.");
            return;
        }

        scaleToggle.onValueChanged.AddListener(OnScaleToggleChanged);
    }

    void OnScaleToggleChanged(bool isScaled)
    {
        foreach (var (zipcodeObject, heightValue, _) in manager.zipcodeObjects)
        {
            Vector3 scale = zipcodeObject.transform.localScale;

            TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();
            if (isScaled)
            {
                float normalizedScale = Mathf.Lerp(1f, 2f, (heightValue - manager.minHeight) / (manager.maxHeight - manager.minHeight));
                scale.y = normalizedScale;

                if (textMesh != null)
                {
                    textMesh.text += $"\nLengte: {heightValue / 100f:F2} cm";
                }
            }
            else
            {
                scale.y = 1f;

                if (textMesh != null)
                {
                    textMesh.text = textMesh.text.Replace($"\nLengte: {heightValue / 100f:F2} cm", "");
                }
            }

            zipcodeObject.transform.localScale = scale;
        }
    }
}
