using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    private float randomTimer;
    private int eventObjectsIndex, randomNum, randomEvent;
    public GameObject[] eventObjects;
    public static bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        randomTimer = Random.Range(15, 45);
        eventObjectsIndex = eventObjects.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaying) RandomEventBehaviour();
    }

    private void RandomEventBehaviour()
	{
        randomTimer -= Time.deltaTime;
        if(randomTimer < 0)
		{
            randomNum = Random.Range(0, 10);
            if(randomNum == 1)
			{
                randomEvent = Random.Range(0, eventObjectsIndex);
                eventObjects[randomEvent].SetActive(true);
                randomNum = 0;
                randomTimer = Random.Range(15, 45);
                isPlaying = true;
			}
			else randomTimer = Random.Range(15, 45);
        }
	}
}
