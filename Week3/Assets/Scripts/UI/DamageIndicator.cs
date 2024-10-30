using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//핵심로직 : DamageIndicator
//플레이어의 onTakeDamage 이벤트를 구독하고있는 클래스이다.
//PlayerCondition 에서 onTakeDamage 이벤트가 발생하면 Flash 함수를 호출한다.
//Flash 함수는 현재 자신의 코루틴이 있을경우 코루틴을 중지시키며, 자신이 가진 Image 컴포넌트를 활성화 시킨다.
//Flash 함수에서 FadeAway 코루틴을 작동시키고, 자신 클래스 변수에 저장시킨다.
//FadeAway 코루틴은 시작시 이미지의 알파값을 특정 값으로 변경하며, 반복문을통해서 알파값을 감소시키다 알파가 0 이하가되면 종료된다.
//코루틴이 종료되면서 이미지 컴포넌트를 비활성화 시킨다.
public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    public void Flash()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while(a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 105 / 255f, 105 / 255f, a);

            yield return null;
        }

        image.enabled = false;
    }
}
