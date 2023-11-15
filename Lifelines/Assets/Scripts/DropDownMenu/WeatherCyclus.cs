using UnityEngine;
using UnityEngine.UI;

public class WeatherCyclus : MonoBehaviour
{
    public Text displayText;
    public string[] items;
    public Material[] skyboxes;
    public GameObject[] gameObjects;

    private int currentIndex = 0;

    private void Start()
    {
        UpdateDisplay();
        UpdateSkybox();
        UpdateGameObjects();
    }

    private void UpdateDisplay()
    {
        if (displayText != null && items.Length > 0)
        {
            displayText.text = items[currentIndex];
        }
    }

    private void UpdateSkybox()
    {
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[currentIndex];
        }
    }

    private void UpdateGameObjects()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (i == currentIndex)
            {
                gameObjects[i].SetActive(true);
            }
            else
            {
                gameObjects[i].SetActive(false);
            }
        }
    }

    public void MoveLeft()
    {
        currentIndex = (currentIndex - 1 + items.Length) % items.Length;
        UpdateDisplay();
        UpdateSkybox();
        UpdateGameObjects();
    }

    public void MoveRight()
    {
        currentIndex = (currentIndex + 1) % items.Length;
        UpdateDisplay();
        UpdateSkybox();
        UpdateGameObjects();
    }
}
