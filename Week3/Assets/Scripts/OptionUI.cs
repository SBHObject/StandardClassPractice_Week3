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
    }
}
