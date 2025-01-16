using UnityEngine;

public class OnObject : MonoBehaviour
{
    public void isDone()
	{
        RandomEvents.isPlaying = false;
        gameObject.SetActive(false);
	}
}
