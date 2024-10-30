using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    //핵심로직 : UI 스크립트 구조
    //UI 스크립트는 각각의 Condition 클래스를 가지고있는구조이다.
    //Condition 클래스는 자신의 최대값, 현재값 등을 가지고있으며, 이를 UI에 표기하는 역할을 한다.
    // -- 개인의견 : 단일책임 원칙을 위배하는듯 하다.
    // -- Condition 은 현재 캐릭터의 스텟 정보를 가짐과 동시에 스텟변화를 위한 메서드를 가지며, UI에 현재 스텟을 표기하는 역할을한다.
    public Condition health;
    public Condition hunger;
    public Condition stamina;
    public Condition mana;

    //별도의 UI 스크립트를 만드는 이유
    //단일책임 원칙을 기반으로, UI 는 UI 관련 스크립트가 책임을 져야한다.
    private void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
