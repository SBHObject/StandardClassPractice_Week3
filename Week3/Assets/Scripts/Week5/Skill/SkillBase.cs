using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public void UseSkill();
}

public abstract class SkillBase : ISkill
{
    //패시브, 액티브, 토글 
    //? 이거 스크립터블 오브젝트로 만들어야하지 않나
    //아닌가...데이터베이스에 스킬 클래스등록
    //스킬 타입에 관한건 퀵슬롯에 전략패턴을 사용해야하는거 같은데
    //퀵슬롯에 전략 사용시, 패시브면 Update에 액티브면 입력시에, 토글이면 입력시 Update...
    //퀵슬롯 전략패턴이라....SkillManager 달고, 여기에 ISkillSlot? 그리고 Update에 ISkillSlot.SlotFunction()
    //패시브는 Update가 맞는데...액티브형이면 Enum 달아두고 if(SkillBase.skilType == SkillType.Active) 같은 느낌으로 제어?
    //스킬 슬롯 SetSkillSlot(int slotindex, SkillBase skill)
    //skillSlot[slotindex].SetSkill(skill);

    //문제는 이후에 어떻게 할 것인가...음...
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
