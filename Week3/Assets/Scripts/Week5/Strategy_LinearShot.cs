using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strategy_LinearShot : IRangeAttack
{
    public void Attack(GameObject projectile, Transform shotPosition)
    {
        Object.Instantiate(projectile, shotPosition.position, shotPosition.rotation);
        Debug.Log("АјАн");
    }
}

public class Strategy_Howitzer : IRangeAttack
{
    float shotPower = 5f;
    public void Attack(GameObject projectile, Transform shotPosition)
    {
        GameObject shot = Object.Instantiate(projectile, shotPosition.position, shotPosition.rotation);
        shot.GetComponent<Rigidbody>().AddForce(shotPosition.position.normalized * shotPower, ForceMode.Impulse);
    }
}

public class Strategy_Magic : IRangeAttack
{
    [SerializeField]
    private int shotAmount = 5;

    private readonly float baseAngle = 360;

    public void Attack(GameObject projectile, Transform shotPosition)
    {
        float preAngle = 0;

        float anglePerShot = baseAngle / shotAmount;

        for(int i = 0; i < shotAmount; i++)
        {
            Vector3 shotAngle = new Vector3(0, preAngle, 0);
            Quaternion qShot = Quaternion.Euler(shotAngle);

            Object.Instantiate(projectile, shotPosition.position, qShot);
            preAngle += anglePerShot;
        }
    }
}
