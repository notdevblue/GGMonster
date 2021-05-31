using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAtk : MonoBehaviour, ISKill
{
    private Stat stat = null;

    private void Awake()
    {
        stat = GetComponent<Stat>();
#if UNITY_EDITOR
        if(stat == null)
        {
            Debug.LogError("SeonHanAtk: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
    }



    public void SkillA()
    {

    }
    public void SkillB()
    {

    }
    public void SkillC()
    {

    }
    public void SkillD()
    {

    }
    

    // 맨 처음에만 실행하면 됨
    private Stat.ClassType CheckType()
    {
        return Stat.ClassType.TEACHER;
    }

    
}
