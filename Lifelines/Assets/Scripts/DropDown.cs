using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public Animator uiAnimator;
    private bool dropDownBool = false;
    public GameObject dropDownPanel;
    // Start is called before the first frame update
    void Start()
    {
        dropDownBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        DropDownLogic();
    }

    private void DropDownLogic()
	{
		if (dropDownBool)
		{
            uiAnimator.SetBool("DropDown", true);
            dropDownPanel.SetActive(true);
		}
		else
		{
            uiAnimator.SetBool("DropDown", false);
            dropDownPanel.SetActive(false);
        }
	}

    public void DropDownToggle() => dropDownBool = !dropDownBool;
}
