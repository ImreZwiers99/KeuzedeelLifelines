using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

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

    void Start()
    {
        if (zipcodePrefab == null)
        {
            Debug.LogError("No prefab assigned to 'zipcodePrefab' in the Inspector.");
            return;
        }

        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError($"JSON file not found at: {jsonFilePath}");
            return;
        }

        string jsonContent = File.ReadAllText(jsonFilePath);
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
    }

    void InstantiateZipcodeGameObject(ZipcodeData zipcodeData, Vector3 position)
    {
        float ageValue = float.Parse(zipcodeData.AGE_T1.Replace(',', '.'));

        GameObject zipcodeObject = Instantiate(zipcodePrefab, position, Quaternion.identity, transform);

        string cityName = zipcodeCityMap.ContainsKey(zipcodeData.ZIPCODE)
            ? zipcodeCityMap[zipcodeData.ZIPCODE]
            : "Unknown City";

        zipcodeObject.name = $"{cityName} (ZIPCODE: {zipcodeData.ZIPCODE})";

        Debug.Log($"Instantiated prefab for ZIPCODE: {zipcodeData.ZIPCODE} at position {position}");

        TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = $"{cityName}\nZIPCODE: {zipcodeData.ZIPCODE}";
            Debug.Log($"Set TextMeshPro text for ZIPCODE: {zipcodeData.ZIPCODE}");
        }
        else
        {
            Debug.LogWarning($"No TextMeshPro component found in prefab for ZIPCODE: {zipcodeData.ZIPCODE}");
        }

        Vector3 scale = zipcodeObject.transform.localScale;
        scale.y = ageValue / 250f;
        zipcodeObject.transform.localScale = scale;

        Debug.Log($"Scaled prefab for {cityName} (ZIPCODE: {zipcodeData.ZIPCODE}) with AGE_T1: {zipcodeData.AGE_T1}");
    }

}
