using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인터페이스의 특징 : 특정 기능의 이름을 미리 정의한 뒤, 상속받은 클래스가 필수적으로 구현하게만든다.
// - 같은 시점에 동작하지만 다른 구현내용을 사용해야할 때 사용한다.
// - 다른 클래스여도 같은 기능을 할 필요가 있을때, 인터페이스를 사용하여 묶어줄 수 있다.
public interface IDamageable
{
    //현재는 캠프파이어로부터 플레이어가 데미지를 받는 내용만 구현되고있다.
    //NPC 등을 구현할 때, 플레이어가 NPC로부터 공격을 받거나, NPC가 플레이어로부터 공격을 받도록 만들 수 있다.
    //캠프파이어는 IDamageable이 붙어있는 대상이면 조건 충족시 데미지를 준다, 따라서 NPC도 IDamageable을 상속받으면 캠프파이어로부터 데미지를 받는다.
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
        Debug.Log("플레이어 사망");
    }

    public void TakeDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

}
