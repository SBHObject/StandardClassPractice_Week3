using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ٽɷ��� : CampFire
//ķ�����̾� Ŭ������ ������ ���� ���������� �����ð����� �������� �ִ� ���Ҹ��� �����Ѵ�.
//damageRate �ð����� �ѹ���, things ����Ʈ�� �ִ� IDamageable �� ��ӹ��� ��ü�� TakeDamage()�� ȣ���Ѵ�.
//things ����Ʈ�� ��ü�� Ʈ���� �ݶ��̴��� ������ �� �ش� ��ü�� IDamageable ���θ� Ȯ���ϸ�, IDamageable�� ������ ����Ʈ�� �߰��Ѵ�.
//��ü�� Ʈ���� �ݶ��̴����� ���� ��, IDamageable ���θ� Ȯ���ϸ� IDamageable�� ������ ����Ʈ���� ���� ��ü�� ã�Ƽ� �����Ѵ�.
public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamageable> things = new List<IDamageable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    private void DealDamage()
    {
        for(int i = 0; i < things.Count; i++)
        {
            things[i].TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            things.Add(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            things.Remove(damageable);
        }
    }
}
