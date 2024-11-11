using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangeAttack
{
    public void Attack(GameObject projectile, Transform shotPosition);
}

public enum RangeAttackType
{
    Linear,
    Howitzer,
    CircleMagic
}

public class RangedWeapon : Equip
{
    public RangeAttackType attackType;
    public IRangeAttack attackStrategy;

    private bool attacking = false;
    [SerializeField]
    private float attackRate = 1f;

    public GameObject projectile;
    public Transform shotPosition;

    private void Awake()
    {
        switch(attackType)
        {
            case RangeAttackType.Linear:
                attackStrategy = new Strategy_LinearShot();
                break;
            case RangeAttackType.Howitzer:
                attackStrategy = new Strategy_Howitzer();
                break;
            case RangeAttackType.CircleMagic:
                attackStrategy = new Strategy_Magic();
                break;
        }
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;

            attackStrategy.Attack(projectile, shotPosition);
            Invoke("OnCanAttack", attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }
}
