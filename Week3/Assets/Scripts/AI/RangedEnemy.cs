using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Npc
{
    public GameObject attackPrefab;
    public Transform shotPosition;

    protected override void Attack()
    {
        GameObject shot = Instantiate(attackPrefab, shotPosition.position, Quaternion.identity);
        shot.GetComponent<Shot>().SetTarget(CharacterManager.Instance.Player.transform, damage);
    }
}
