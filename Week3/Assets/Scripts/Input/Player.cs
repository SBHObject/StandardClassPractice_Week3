using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� �÷��̾��� ������ ��� ����
//��, �÷��̾��� ��� ������ �ƴ�, �÷��̾ ����ϴ� Ŭ���� ������ ������ �ִ� ����
//�� Ŭ������ ���� �÷��̾ ���Ǵ� ������ Ŭ������ �����ϰԵ�
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
