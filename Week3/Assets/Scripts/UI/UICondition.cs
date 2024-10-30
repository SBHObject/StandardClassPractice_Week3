using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    //�ٽɷ��� : UI ��ũ��Ʈ ����
    //UI ��ũ��Ʈ�� ������ Condition Ŭ������ �������ִ±����̴�.
    //Condition Ŭ������ �ڽ��� �ִ밪, ���簪 ���� ������������, �̸� UI�� ǥ���ϴ� ������ �Ѵ�.
    // -- �����ǰ� : ����å�� ��Ģ�� �����ϴµ� �ϴ�.
    // -- Condition �� ���� ĳ������ ���� ������ ������ ���ÿ� ���ݺ�ȭ�� ���� �޼��带 ������, UI�� ���� ������ ǥ���ϴ� �������Ѵ�.
    public Condition health;
    public Condition hunger;
    public Condition stamina;
    public Condition mana;

    //������ UI ��ũ��Ʈ�� ����� ����
    //����å�� ��Ģ�� �������, UI �� UI ���� ��ũ��Ʈ�� å���� �����Ѵ�.
    private void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
