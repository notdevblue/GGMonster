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
            Debug.LogError("SeonHanAtk: Stat �� ã�� �� �����ϴ�.");
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
    

    // �� ó������ �����ϸ� ��
    private Stat.ClassType CheckType()
    {
        return Stat.ClassType.TEACHER;
    }

    
}
