using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public GameObject optionUIObject;

    private void Start()
    {
        CharacterManager.Instance.Player.controller.toggleUI += ToggleUI;
    }

    public void ToggleUI(bool active)
    {
        optionUIObject.SetActive(active);

        if(active == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ClickResumeButton()
    {
        ToggleUI(false);
        CharacterManager.Instance.Player.controller.ToggleCursor(false);
    }
}
