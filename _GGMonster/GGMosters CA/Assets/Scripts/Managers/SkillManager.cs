using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Skills 의 Enum 순서와 같아야 함")]
    public List<Sprite> spr = new List<Sprite>();

    private void Awake()
    {
        for(int i = 0; i < (int)SkillListEnum.DEFAULTEND; ++i)
        {
            bool pass = i == (int)SkillListEnum.SEONHANEND || i == (int)SkillListEnum.HAEUNEND;
            skillSprite[(SkillListEnum)i] = spr[i];
        }
    }

    public Dictionary<SkillListEnum, Sprite> skillSprite = new Dictionary<SkillListEnum, Sprite>(); // 스킬 사용시 나오는 스프라이트
}
