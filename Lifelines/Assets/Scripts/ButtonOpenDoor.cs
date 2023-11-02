using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenDoor : MonoBehaviour
{
    public Animator doorAnimator;
    private bool doorToggle = false;

    private void Start() => doorToggle = false;
    public void DoorOpen()
    {
        doorToggle = !doorToggle;

        if(doorToggle) doorAnimator.SetBool("Door", true);
        else doorAnimator.SetBool("Door", false);
    }
}
