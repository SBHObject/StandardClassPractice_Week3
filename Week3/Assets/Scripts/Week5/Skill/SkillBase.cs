using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public void UseSkill();
}

public abstract class SkillBase : ISkill
{
    //�нú�, ��Ƽ��, ��� 
    //? �̰� ��ũ���ͺ� ������Ʈ�� ���������� �ʳ�
    //�ƴѰ�...�����ͺ��̽��� ��ų Ŭ�������
    //��ų Ÿ�Կ� ���Ѱ� �����Կ� ���������� ����ؾ��ϴ°� ������
    //�����Կ� ���� ����, �нú�� Update�� ��Ƽ��� �Է½ÿ�, ����̸� �Է½� Update...
    //������ ���������̶�....SkillManager �ް�, ���⿡ ISkillSlot? �׸��� Update�� ISkillSlot.SlotFunction()
    //�нú�� Update�� �´µ�...��Ƽ�����̸� Enum �޾Ƶΰ� if(SkillBase.skilType == SkillType.Active) ���� �������� ����?
    //��ų ���� SetSkillSlot(int slotindex, SkillBase skill)
    //skillSlot[slotindex].SetSkill(skill);

    //������ ���Ŀ� ��� �� ���ΰ�...��...
    //

    public abstract void UseSkill();
}

public class ActiveSkillFunction : SkillBase
{
    public override void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}

public class PassiveSkillFunction : SkillBase
{
    public override void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}
