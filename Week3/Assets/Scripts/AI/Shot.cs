using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Transform targetPosition;
    private int damage;

    private void Update()
    {
        if(Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
        {
            CharacterManager.Instance.Player.controller.GetComponent<IDamagable>().TakeDamage(damage);
            Destroy(gameObject);
        }

        Vector3 target = targetPosition.position - transform.position;
        transform.position += target.normalized * 2f * Time.deltaTime;
    }

    public void SetTarget(Transform target, float damage)
    {
        targetPosition = target;
    }
}
