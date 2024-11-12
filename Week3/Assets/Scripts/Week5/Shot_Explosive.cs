using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Explosive : MonoBehaviour
{
    public LayerMask hitLayer;

    private int shotDamage;
    private GameObject shoter;

    public void SetShotInfo(GameObject source, int damage)
    {
        shoter = source;
        shotDamage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shoter) return;
        var hits = Physics.OverlapSphere(transform.position, 10f, hitLayer);

        foreach(var hit in hits)
        {
            IDamagable damagable;
            if(hit.TryGetComponent(out damagable))
            {
                damagable.TakeDamage(shotDamage);
            }
        }

        Destroy(gameObject);
    }
}
