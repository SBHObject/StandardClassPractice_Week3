using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearShot : MonoBehaviour
{
    private float moveSpeed = 1f;

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        Invoke("SelfDestroy", 1f);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
