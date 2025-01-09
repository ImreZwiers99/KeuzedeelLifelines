using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For UI Toggle
using TMPro; // For TextMeshPro

public class InstantiateZipcodes : MonoBehaviour
{
    [System.Serializable]
    public class ZipcodeData
    {
        public int ZIPCODE;
        public string AGE_T1;
    }

    [System.Serializable]
    public class ZipcodeList
    {
        public List<ZipcodeData> data;
    }

    public string jsonFilePath = "Assets/json/csvjson.json";
    public GameObject zipcodePrefab;
    public Toggle scaleToggle; // Reference to the UI Toggle

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

    private List<GameObject> zipcodeObjects = new List<GameObject>();

    void Start()
    {
        if (zipcodePrefab == null)
        {
            Debug.LogError("No prefab assigned to 'zipcodePrefab' in the Inspector.");
            return;
        }

        if (scaleToggle == null)
        {
            Debug.LogError("No Toggle assigned to 'scaleToggle' in the Inspector.");
            return;
        }

        if (!System.IO.File.Exists(jsonFilePath))
        {
            Debug.LogError($"JSON file not found at: {jsonFilePath}");
            return;
        }

        string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
        ZipcodeList zipcodeList = JsonUtility.FromJson<ZipcodeList>("{\"data\":" + jsonContent + "}");

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

        // Add listener to toggle
        scaleToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void InstantiateZipcodeGameObject(ZipcodeData zipcodeData, Vector3 position)
    {
        float ageValue = float.Parse(zipcodeData.AGE_T1.Replace(',', '.'));

        GameObject zipcodeObject = Instantiate(zipcodePrefab, position, Quaternion.identity, transform);
        zipcodeObjects.Add(zipcodeObject);

        string cityName = zipcodeCityMap.ContainsKey(zipcodeData.ZIPCODE)
            ? zipcodeCityMap[zipcodeData.ZIPCODE]
            : "Unknown City";

        zipcodeObject.name = $"{cityName} (ZIPCODE: {zipcodeData.ZIPCODE})";

        TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = $"{cityName}\nZIPCODE: {zipcodeData.ZIPCODE}";
        }

        // Store the original scale
        zipcodeObject.transform.localScale = new Vector3(1, 1, 1);
    }

    void OnToggleValueChanged(bool isScaled)
    {
        foreach (var zipcodeObject in zipcodeObjects)
        {
            float ageValue = float.Parse(zipcodeObject.name.Split('(')[1].Split(':')[1].Replace(")", "").Trim());
            Vector3 scale = zipcodeObject.transform.localScale;

            if (isScaled)
            {
                // Scale based on AGE_T1
                scale.y = ageValue / 250f;
            }
            else
            {
                // Reset to original scale
                scale.y = 1f;
            }

            zipcodeObject.transform.localScale = scale;
        }
    }
}
