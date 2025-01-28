using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class CityAddInScroldown : MonoBehaviour
{
    public TMP_InputField searchBar;
    public Transform scrollViewContent;
    public GameObject buttonPrefab;
    public GameObject noResultsText;

    [SerializeField] private Transform cityHolder;
    [SerializeField] private List<GameObject> cityList = new List<GameObject>();


    void Start()
    {
        StartCoroutine(FillCityListWithDelay());

        searchBar.onValueChanged.AddListener(FilterCities);
    }

    private IEnumerator FillCityListWithDelay()
    {
        if (cityHolder == null)
        {
            Debug.LogError("ParentObject is niet ingesteld in de Inspector!");
            yield break;
        }
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Aantal kinderen gevonden: " + cityHolder.childCount);

        if (cityHolder.childCount == 0)
        {
            Debug.LogWarning("Geen kinderen gevonden onder het parentObject!");
            yield break;
        }

        foreach (Transform child in cityHolder)
        {
            cityList.Add(child.gameObject);
        }

        foreach (GameObject city in cityList)
        {
            Debug.Log("City toegevoegd: " + city.name);
        }

        InstantiateButtons();
    }

    public void InstantiateButtons()
    {
        foreach (Transform child in cityHolder)
        {
            GameObject newButton = Instantiate(buttonPrefab, scrollViewContent);

            TMP_Text buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            Button button = newButton.GetComponent<Button>();
            if (buttonText != null)
            {
                buttonText.text = child.name;
            }

            if (button != null)
            {
                button.onClick.AddListener(() => OnCityButtonClicked(child.gameObject));
            }
        }
    }

    private void OnCityButtonClicked(GameObject city)
    {
        Transform snapPoint = city.transform.Find("SnapPoint");
        if (snapPoint != null)
        {
            Camera.main.transform.position = snapPoint.position;
            Camera.main.transform.LookAt(city.transform);
            Debug.Log("Camera geteleporteerd naar: " + city.name);
        }
        else
        {
            Debug.LogWarning("Geen SnapPoint gevonden voor stad: " + city.name);
        }
    }
    private void FilterCities(string input)
    {
        input = input.ToLower(); // Zet de input in kleine letters voor een case-insensitive vergelijking.
        bool hasResults = false;

        foreach (Transform button in scrollViewContent)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                // Controleer of de tekst begint met dezelfde letters in de juiste volgorde.
                bool isVisible = string.IsNullOrEmpty(input) || buttonText.text.ToLower().StartsWith(input);
                button.gameObject.SetActive(isVisible);

                if (isVisible)
                {
                    hasResults = true;
                }
            }
        }

        // Toon of verberg de "Geen resultaten"-tekst.
        if (noResultsText != null)
        {
            noResultsText.SetActive(!hasResults);
        }
    }
    
}
