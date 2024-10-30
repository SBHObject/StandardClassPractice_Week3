using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ٽɷ��� : DamageIndicator
//�÷��̾��� onTakeDamage �̺�Ʈ�� �����ϰ��ִ� Ŭ�����̴�.
//PlayerCondition ���� onTakeDamage �̺�Ʈ�� �߻��ϸ� Flash �Լ��� ȣ���Ѵ�.
//Flash �Լ��� ���� �ڽ��� �ڷ�ƾ�� ������� �ڷ�ƾ�� ������Ű��, �ڽ��� ���� Image ������Ʈ�� Ȱ��ȭ ��Ų��.
//Flash �Լ����� FadeAway �ڷ�ƾ�� �۵���Ű��, �ڽ� Ŭ���� ������ �����Ų��.
//FadeAway �ڷ�ƾ�� ���۽� �̹����� ���İ��� Ư�� ������ �����ϸ�, �ݺ��������ؼ� ���İ��� ���ҽ�Ű�� ���İ� 0 ���ϰ��Ǹ� ����ȴ�.
//�ڷ�ƾ�� ����Ǹ鼭 �̹��� ������Ʈ�� ��Ȱ��ȭ ��Ų��.
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
