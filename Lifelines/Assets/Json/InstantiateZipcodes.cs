using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstantiateZipcodes : MonoBehaviour
{
    [System.Serializable]
    public class ZipcodeData
    {
        public int ZIPCODE;
        public string HEIGHT_T1;
        public string AGE_T1;
    }

    [System.Serializable]
    public class ZipcodeList
    {
        public List<ZipcodeData> data;
    }

    public string jsonFilePath = "Assets/json/csvjson.json";
    public GameObject zipcodePrefab;
    public Toggle scaleToggle;
    public Toggle disableChildToggle;

    private float minAge;
    private float maxAge;

    private Dictionary<int, string> zipcodeCityMap = new Dictionary<int, string>
    {
        { 7741, "Hardenberg" },
        { 7742, "Heemse" },
        { 7751, "Dalen" },
        { 7761, "Emmen" },
        { 7764, "Nieuw-Amsterdam" },
        { 7765, "Veenoord" },
        { 7811, "Emmen Centrum" },
        { 7812, "Emmermeer" },
        { 7813, "Rietlanden" },
        { 7814, "Emmerhout" },
        { 7815, "Houtweg" },
        { 7822, "Angelslo" },
        { 7823, "Barger-Oosterveld" },
        { 7824, "Delftlanden" },
        { 7826, "Parc Sandur" },
        { 7827, "Barger-Compascuum" },
        { 7901, "Hoogeveen" },
        { 7902, "Hoogeveen Centrum" },
        { 7903, "Erflanden" },
        { 7904, "De Weide" },
        { 7905, "Krakeel" },
        { 7906, "Alteveer" },
        { 7907, "De Schutlanden" },
        { 7908, "Trasselt" },
        { 9401, "Assen" },
        { 9402, "Assen Oost" },
        { 9403, "Marsdijk" },
        { 9404, "Kloosterveen" },
        { 9405, "Pittelo" },
        { 9406, "Peelo" }
    };

    public List<(GameObject, float, float)> zipcodeObjects = new List<(GameObject, float, float)>();

    void Start()
    {
        if (zipcodePrefab == null)
        {
            Debug.LogError("No prefab assigned to 'zipcodePrefab' in the Inspector.");
            return;
        }

        if (scaleToggle == null || disableChildToggle == null)
        {
            Debug.LogError("One or more toggles are not assigned in the Inspector.");
            return;
        }

        if (!System.IO.File.Exists(jsonFilePath))
        {
            Debug.LogError($"JSON file not found at: {jsonFilePath}");
            return;
        }

        string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
        ZipcodeList zipcodeList = JsonUtility.FromJson<ZipcodeList>("{\"data\":" + jsonContent + "}");

        float minHeight = float.MaxValue;
        float maxHeight = float.MinValue;
        minAge = float.MaxValue;
        maxAge = float.MinValue;

        foreach (var zipcodeData in zipcodeList.data)
        {
            float heightValue = float.Parse(zipcodeData.HEIGHT_T1.Replace(',', '.'));
            float ageValue = float.Parse(zipcodeData.AGE_T1.Replace(',', '.'));

            if (heightValue < minHeight) minHeight = heightValue;
            if (heightValue > maxHeight) maxHeight = heightValue;

            if (ageValue < minAge) minAge = ageValue;
            if (ageValue > maxAge) maxAge = ageValue;
        }

        Debug.Log($"Min Height: {minHeight}, Max Height: {maxHeight}");
        Debug.Log($"Min Age: {minAge}, Max Age: {maxAge}");

        Vector3 startPosition = new Vector3(0, 0, 0);
        float xOffset = 10f;
        float zOffset = 10f;
        int index = 0;

        foreach (var zipcodeData in zipcodeList.data)
        {
            Vector3 position = startPosition + new Vector3((index % 10) * xOffset, 0, (index / 10) * zOffset);
            InstantiateZipcodeGameObject(zipcodeData, position);
            index++;
        }

        scaleToggle.onValueChanged.AddListener(isScaled => OnScaleToggleChanged(isScaled, minHeight, maxHeight));
        disableChildToggle.onValueChanged.AddListener(OnDisableChildToggleChanged);
    }

    void InstantiateZipcodeGameObject(ZipcodeData zipcodeData, Vector3 position)
    {
        float heightValue = float.Parse(zipcodeData.HEIGHT_T1.Replace(',', '.'));
        float ageValue = float.Parse(zipcodeData.AGE_T1.Replace(',', '.'));

        GameObject zipcodeObject = Instantiate(zipcodePrefab, position, Quaternion.identity, transform);

        string cityName = zipcodeCityMap.ContainsKey(zipcodeData.ZIPCODE)
            ? zipcodeCityMap[zipcodeData.ZIPCODE]
            : "Unknown City";

        zipcodeObject.name = $"{cityName}";

        TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = $"{cityName}";
        }

        zipcodeObjects.Add((zipcodeObject, heightValue, ageValue));

        zipcodeObject.transform.localScale = new Vector3(1, 1, 1);
    }

    void OnScaleToggleChanged(bool isScaled, float minHeight, float maxHeight)
    {
        foreach (var (zipcodeObject, heightValue, _) in zipcodeObjects)
        {
            Vector3 scale = zipcodeObject.transform.localScale;

            TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();
            if (isScaled)
            {
                float normalizedScale = Mathf.Lerp(1f, 2f, (heightValue - minHeight) / (maxHeight - minHeight));
                scale.y = normalizedScale;

                if (textMesh != null)
                {
                    textMesh.text += $"\nHeight: {heightValue / 100f:F2} cm";
                }
            }
            else
            {
                scale.y = 1f;

                if (textMesh != null)
                {
                    textMesh.text = textMesh.text.Replace($"\nHeight: {heightValue / 100f:F2} cm", "");
                }
            }

            zipcodeObject.transform.localScale = scale;
        }
    }

    void OnDisableChildToggleChanged(bool isDisabled)
    {
        foreach (var (zipcodeObject, _, ageValue) in zipcodeObjects)
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
                    float normalizedAge = (ageValue - minAge) / (maxAge - minAge);

                    Color interpolatedColor = Color.Lerp(Color.green, Color.red, normalizedAge);

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
                    textMesh.text += $"\nAge: {ageValue / 100f:F2} jaar";
                }
                else
                {
                    textMesh.text = textMesh.text.Replace($"\nAge: {ageValue / 100f:F2} jaar", "");
                }
            }
        }
    }
}
