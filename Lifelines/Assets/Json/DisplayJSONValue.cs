using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DisplayJSONValue : MonoBehaviour
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

    // Path to your JSON file in the Unity project
    [Tooltip("Relative path to the JSON file from the Assets folder")]
    private string jsonFilePath = "json/csvjson.json";

    void Start()
    {
        string fullPath = Path.Combine(Application.dataPath, jsonFilePath);

        if (!File.Exists(fullPath))
        {
            Debug.LogError($"File not found at path: {fullPath}");
            return;
        }

        Debug.Log($"Reading JSON file from: {fullPath}");

        try
        {
            string jsonContent = File.ReadAllText(fullPath);
            ZipcodeList zipcodeList = JsonUtility.FromJson<ZipcodeList>("{\"data\":" + jsonContent + "}");

            foreach (var zipcodeData in zipcodeList.data)
            {
                Debug.Log($"ZIPCODE: \"{zipcodeData.ZIPCODE}\" = AGE_T1: {zipcodeData.AGE_T1}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error reading or processing JSON file: {ex.Message}");
        }
    }
}
