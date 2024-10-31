using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//실제 플레이어의 정보를 담고 있음
//단, 플레이어의 모든 정보가 아닌, 플레이어가 사용하는 클래스 정보를 가지고 있는 형태
//이 클래스를 통해 플레이어에 사용되는 각각의 클래스에 접근하게됨
public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
