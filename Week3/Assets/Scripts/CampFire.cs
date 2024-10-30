using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//핵심로직 : CampFire
//캠프파이어 클래스는 지정한 범위 내에있으면 일정시간마다 데미지를 주는 역할만을 수행한다.
//damageRate 시간마다 한번씩, things 리스트에 있는 IDamageable 을 상속받은 객체에 TakeDamage()를 호출한다.
//things 리스트는 객체가 트리거 콜라이더에 진입할 때 해당 객체의 IDamageable 여부를 확인하며, IDamageable이 있으면 리스트에 추가한다.
//객체가 트리거 콜라이더에서 나갈 때, IDamageable 여부를 확인하며 IDamageable이 있으면 리스트에서 같은 객체를 찾아서 제거한다.
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
