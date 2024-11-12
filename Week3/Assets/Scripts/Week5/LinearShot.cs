using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IRangeShot
{
    public void SetShotInfo(GameObject source, int damage);
}

public class LinearShot : MonoBehaviour, IRangeShot
{
    private float moveSpeed = 5f;
    private GameObject shoter;
    private int shotDamage;

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        Invoke("SelfDestroy", 1f);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shoter) return;

        IDamagable damagalbe;
        if(other.TryGetComponent(out damagalbe))
        {
            damagalbe.TakeDamage(shotDamage);
        }

        SelfDestroy();
    }

    public void SetShotInfo(GameObject source, int damage)
    {
        shoter = source;
        shotDamage = damage;
    }
}
