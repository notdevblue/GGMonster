using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Skills �� Enum ������ ���ƾ� ��")]
    public List<Sprite> spr = new List<Sprite>();

    private void Awake()
    {
        for(int i = 0; i < (int)SkillListEnum.DEFAULTEND; ++i)
        {
            bool pass = i == (int)SkillListEnum.SEONHANEND || i == (int)SkillListEnum.HAEUNEND;
            skillSprite[(SkillListEnum)i] = spr[i];
        }
    }

    public Dictionary<SkillListEnum, Sprite> skillSprite = new Dictionary<SkillListEnum, Sprite>(); // ��ų ���� ������ ��������Ʈ
}
