using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class InstantiateZipcodes : MonoBehaviour
{
    [System.Serializable]
    public class ZipcodeData
    {
        public int ZIPCODE;
        public string HEIGHT_T1;
        public string AGE_T1;
        public string BMI_T1;
        public string DEPRESSION_T1;
    }

    [System.Serializable]
    public class ZipcodeList
    {
        public List<ZipcodeData> data;
    }

    public string jsonFilePath = "Assets/json/csvjson.json";
    public GameObject zipcodePrefab;

    [HideInInspector]
    public List<(GameObject, float, float, float, float)> zipcodeObjects = new List<(GameObject, float, float, float, float)>();

    [HideInInspector]
    public float minHeight = float.MaxValue, maxHeight = float.MinValue;
    [HideInInspector]
    public float minAge = float.MaxValue, maxAge = float.MinValue;
    [HideInInspector]
    public float minBMI = float.MaxValue, maxBMI = float.MinValue;
    [HideInInspector]
    public float minDEP = float.MaxValue, maxDEP = float.MinValue;

    public Dictionary<int, string> zipcodeCityMap = new Dictionary<int, string>
    {
        { 7741, "Hardenberg" },
        { 7742, "Heemse" },
        { 7751, "Dalen" },
        { 7761, "Emmen" },
        { 7764, "Nieuw-Amsterdam" },
        { 7765, "Veenoord" },
        { 7766, "Zandpol" },
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
        { 7828, "Klazienaveen" },
        { 7831, "Nieuw-Weerdinge" },
        { 7833, "Zwartemeer" },
        { 7901, "Hoogeveen" },
        { 7902, "Hoogeveen Centrum" },
        { 7903, "Erflanden" },
        { 7904, "De Weide" },
        { 7905, "Krakeel" },
        { 7906, "Alteveer" },
        { 7907, "De Schutlanden" },
        { 7908, "Trasselt" },
        { 7913, "Hollandscheveld" },
        { 7933, "Pesse" },
        { 7941, "Meppel" },
        { 7942, "Meppel Centrum" },
        { 7943, "Oosterboer" },
        { 7944, "Berggierslanden" },
        { 7946, "Koedijkslanden" },
        { 7948, "Nijeveen" },
        { 7961, "Ruinerwold" },
        { 7963, "Ruinen" },
        { 7964, "Ansen" },
        { 7965, "De Wijk" },
        { 7966, "IJhorst" },
        { 7971, "Havelte" },
        { 7973, "Darp" },
        { 7975, "Uffelte" },
        { 7981, "Diever" },
        { 7983, "Wapse" },
        { 7984, "Zorgvlied" },
        { 7985, "Vledder" },
        { 7986, "Wapserveen" },
        { 7991, "Dwingeloo" },
        { 7994, "Dieverbrug" },
        { 8021, "Zwolle Centrum" },
        { 8022, "Kamperpoort" },
        { 8023, "Assendorp" },
        { 8024, "Wipstrik" },
        { 8025, "Stadshagen" },
        { 8031, "Holtenbroek" },
        { 8032, "Aa-landen" },
        { 8041, "Frankhuis" },
        { 8042, "Voorst" },
        { 8043, "Berkum" },
        { 8044, "Spoolde" },
        { 8045, "Zwolle Zuid" },
        { 8051, "Hattem" },
        { 8052, "Hattem Noord" },
        { 8055, "Veessen" },
        { 8061, "Hasselt" },
        { 8064, "Zwartsluis" },
        { 8071, "Nunspeet" },
        { 8072, "Elspeet" },
        { 8075, "Uddel" },
        { 8081, "Elburg" },
        { 8082, "Doornspijk" },
        { 8084, "'t Harde" },
        { 8085, "Drontermeer" },
        { 8091, "Wezep" },
        { 8094, "Hattemerbroek" },
        { 8096, "Oldebroek" },
        { 8097, "Oosterwolde" },
        { 8101, "Raalte" },
        { 8102, "Raalte Zuid" },
        { 8111, "Heeten" },
        { 8112, "Nieuw-Heeten" },
        { 8121, "Olst" },
        { 8124, "Wesepe" },
        { 8131, "Wijhe" },
        { 8134, "Marle" },
        { 8141, "Heino" },
        { 8142, "Laag Zuthem" },
        { 8151, "Lemelerveld" },
        { 8161, "Epe" },
        { 8162, "Vaassen" },
        { 8166, "Emst" },
        { 8171, "Vaassen" },
        { 8172, "Epe Zuid" },
        { 8181, "Heerde" },
        { 8182, "Wapenveld" },
        { 8191, "Oene" },
        { 8194, "Veessen" },
        { 8201, "Lelystad Centrum" },
        { 8211, "Lelystad Haven" },
        { 8222, "Lelystad Noord" },
        { 8223, "Lelystad Zuid" },
        { 8231, "Lelystad West" },
        { 8232, "Lelystad Oost" },
        { 8241, "Lelystad Boswijk" },
        { 8242, "Lelystad Warande" },
        { 8251, "Dronten Centrum" },
        { 8252, "Dronten West" },
        { 8253, "Dronten Zuid" },
        { 8261, "Kampen Centrum" },
        { 8262, "Kampen Zuid" },
        { 8263, "Kampen West" },
        { 8264, "Kampen Noord" },
        { 8265, "IJsselmuiden" },
        { 8271, "Genemuiden" },
        { 8278, "Kraggenburg" },
        { 8301, "Emmeloord Centrum" },
        { 8302, "Emmeloord Oost" },
        { 8303, "Emmeloord West" },
        { 8304, "Espel" },
        { 8311, "Marknesse" },
        { 8313, "Rutten" },
        { 8321, "Urk" },
        { 8322, "Urk Zuid" },
        { 8331, "Steenwijk" },
        { 8332, "Steenwijk Noord" },
        { 8333, "Steenwijk Zuid" },
        { 8341, "Blokzijl" },
        { 8351, "Wanneperveen" },
        { 8352, "Giethoorn" },
        { 8355, "Giethoorn Noord" },
        { 8371, "Scheerwolde" },
        { 8372, "Steggerda" },
        { 8381, "Vledderveen" },
        { 8382, "Nijeberkoop" },
        { 8383, "Oude Willem" },
        { 8401, "Gorredijk" },
        { 8411, "Jubbega" },
        { 8421, "Oldeberkoop" },
        { 8431, "Oosterwolde" },
        { 8441, "Heerenveen Centrum" },
        { 8442, "Heerenveen Noord" },
        { 8443, "Heerenveen Zuid" }
    };


    void Start()
    {
        var allZipcodes = FindObjectsOfType<ZipcodeDataComponent>();

        minHeight = float.MaxValue;
        maxHeight = float.MinValue;

        foreach (var dataComponent in allZipcodes)
        {
            if (dataComponent.heightValue < minHeight) minHeight = dataComponent.heightValue;
            if (dataComponent.heightValue > maxHeight) maxHeight = dataComponent.heightValue;
        }

        if (zipcodePrefab == null)
        {
            Debug.LogError("No prefab assigned to 'zipcodePrefab' in the Inspector.");
            return;
        }

        if (!System.IO.File.Exists(jsonFilePath))
        {
            Debug.LogError($"JSON file not found at: {jsonFilePath}");
            return;
        }

        string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
        ZipcodeList zipcodeList = JsonUtility.FromJson<ZipcodeList>("{\"data\":" + jsonContent + "}");

        minHeight = float.MaxValue;
        maxHeight = float.MinValue;
        minAge = float.MaxValue;
        maxAge = float.MinValue;
        minBMI = float.MaxValue;
        maxBMI = float.MinValue;
        minDEP = float.MaxValue;
        maxDEP = float.MinValue;

        foreach (var zipcodeData in zipcodeList.data)
        {
            float heightValue = float.Parse(zipcodeData.HEIGHT_T1.Replace(',', '.'));
            float ageValue = float.Parse(zipcodeData.AGE_T1.Replace(',', '.'));
            float bmiValue = float.Parse(zipcodeData.BMI_T1.Replace(',', '.'));
            float DEPValue = float.Parse(zipcodeData.DEPRESSION_T1.Replace(',', '.'));

            if (heightValue < minHeight) minHeight = heightValue;
            if (heightValue > maxHeight) maxHeight = heightValue;

            if (ageValue < minAge) minAge = ageValue;
            if (ageValue > maxAge) maxAge = ageValue;

            if (bmiValue < minBMI) minBMI = bmiValue;
            if (bmiValue > maxBMI) maxBMI = bmiValue;

            if (DEPValue < minDEP) minDEP = DEPValue;
            if (DEPValue > maxDEP) maxDEP = DEPValue;
        }

        Debug.Log($"Min Height: {minHeight}, Max Height: {maxHeight}");
        Debug.Log($"Min Age: {minAge}, Max Age: {maxAge}");
        Debug.Log($"Min BMI: {minBMI}, Max BMI: {maxBMI}");
        Debug.Log($"Min DEP: {minDEP}, Max DEP: {maxDEP}");

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
        float heightValue = float.Parse(zipcodeData.HEIGHT_T1.Replace(',', '.'));
        float ageValue = float.Parse(zipcodeData.AGE_T1.Replace(',', '.'));
        float bmiValue = float.Parse(zipcodeData.BMI_T1.Replace(',', '.'));
        float DEPValue = float.Parse(zipcodeData.DEPRESSION_T1.Replace(',', '.'));

        // Check if the object for this city already exists
        string cityName = zipcodeCityMap.ContainsKey(zipcodeData.ZIPCODE)
            ? zipcodeCityMap[zipcodeData.ZIPCODE]
            : "Unknown City";

        string objectName = $"{cityName.ToLower()}";
        GameObject existingObject = GameObject.Find(objectName);

        if (existingObject != null)
        {
            Debug.Log($"Object for city '{cityName}' already exists, skipping instantiation.");
            return;
        }

        // Load the prefab and instantiate
        GameObject prefab = Resources.Load<GameObject>("Json/Cube");
        if (prefab == null)
        {
            Debug.LogError("Prefab not found! Ensure the prefab is in Resources/Json and the path is correct.");
            return;
        }

        GameObject zipcodeObject = Instantiate(prefab, position, Quaternion.identity, transform);
        zipcodeObject.name = objectName;

        var dataComponent = zipcodeObject.AddComponent<ZipcodeDataComponent>();
        dataComponent.zipcode = zipcodeData.ZIPCODE;
        dataComponent.heightValue = heightValue;
        dataComponent.ageValue = ageValue;
        dataComponent.bmiValue = bmiValue;
        dataComponent.depressionValue = DEPValue;

        // Update TextMeshPro text
        TextMeshPro textMesh = zipcodeObject.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = $"Stad: {cityName}";
        }

        // Add the object and its values to the list
        zipcodeObjects.Add((zipcodeObject, heightValue, ageValue, bmiValue, DEPValue));

        // Set default scale
        zipcodeObject.transform.localScale = new Vector3(1, 1, 1);

        Debug.Log($"Successfully instantiated object for city: {cityName}");
    }
}
