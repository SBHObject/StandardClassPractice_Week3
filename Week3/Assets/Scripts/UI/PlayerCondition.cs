using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������̽��� Ư¡ : Ư�� ����� �̸��� �̸� ������ ��, ��ӹ��� Ŭ������ �ʼ������� �����ϰԸ����.
// - ���� ������ ���������� �ٸ� ���������� ����ؾ��� �� ����Ѵ�.
// - �ٸ� Ŭ�������� ���� ����� �� �ʿ䰡 ������, �������̽��� ����Ͽ� ������ �� �ִ�.
public interface IDamageable
{
    //����� ķ�����̾�κ��� �÷��̾ �������� �޴� ���븸 �����ǰ��ִ�.
    //NPC ���� ������ ��, �÷��̾ NPC�κ��� ������ �ްų�, NPC�� �÷��̾�κ��� ������ �޵��� ���� �� �ִ�.
    //ķ�����̾�� IDamageable�� �پ��ִ� ����̸� ���� ������ �������� �ش�, ���� NPC�� IDamageable�� ��ӹ����� ķ�����̾�κ��� �������� �޴´�.
    void TakeDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;

    private Condition health { get { return uiCondition.health; } }
    private Condition hunger { get { return uiCondition.hunger; } }
    private Condition stamina {  get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(hunger.curValue <= 0)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if(health.curValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�÷��̾� ���");
    }

    public void TakeDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

}
