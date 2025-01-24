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

    [SerializeField] private Transform cityHolder;
    [SerializeField] private List<GameObject> cityList = new List<GameObject>();


    void Start()
    {
        StartCoroutine(FillCityListWithDelay());
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
            GameObject newButton = Instantiate(buttonPrefab, scrollViewContent) as GameObject;

            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = child.name;
            }

            Button button = newButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnCityButtonClicked(child.gameObject));
            }
        }
    }

    private void OnCityButtonClicked(GameObject city)
    {
       
    }
}
