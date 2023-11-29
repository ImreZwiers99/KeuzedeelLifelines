using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WeatherCyclus : MonoBehaviour
{
    public Text displayText;
    public string[] items;
    public Material[] skyboxes;
    public GameObject[] gameObjects;
    public Light directionalLight;

    private int currentIndex = 0, randomAnimNumber;

    public Animator player_Animator;
    public NavMeshAgent navMeshAgent;

    private bool idleToggleBool = false;

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

        if (currentIndex == 0) directionalLight.color = Color.white;
        else if (currentIndex == 1) directionalLight.color = Color.black;

        if (idleToggleBool)
		{
            if (currentIndex == 0)
            {
                player_Animator.SetInteger("Sad", 0);
                player_Animator.SetInteger("Happy", randomAnimNumber);
            }
            else if (currentIndex == 1)
            {
                player_Animator.SetInteger("Happy", 0);
                player_Animator.SetInteger("Sad", randomAnimNumber);
            }
        }
		else if (!idleToggleBool)
		{
            if (currentIndex == 0)
            {
                player_Animator.SetInteger("Happy", 0);
                player_Animator.SetInteger("Sad", 0);
            }
            else if (currentIndex == 1)
            {
                player_Animator.SetInteger("Happy", 3);
                player_Animator.SetInteger("Sad", 3);
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

    public void IdleOnlyToggle(bool idleToggle)
	{
        idleToggleBool = idleToggle;
        if (idleToggle)
		{
            navMeshAgent.speed = 0;
            randomAnimNumber = Random.Range(1, 3);
            if (currentIndex == 0)
			{
                player_Animator.SetInteger("Sad", 0);
                player_Animator.SetInteger("Happy", randomAnimNumber);
            }
            else if(currentIndex == 1)
			{
                player_Animator.SetInteger("Happy", 0);
                player_Animator.SetInteger("Sad", randomAnimNumber);
            }
        }
		else
		{
            navMeshAgent.speed = 2;
            if (currentIndex == 0)
			{
                player_Animator.SetInteger("Happy", 0);
                player_Animator.SetInteger("Sad", 0);
            }
            else if (currentIndex == 1)
			{
                player_Animator.SetInteger("Happy", 3);
                player_Animator.SetInteger("Sad", 3);
            }
        }
	}
}
