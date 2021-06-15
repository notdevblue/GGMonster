using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAI : AIBase
{
    #region ����
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("Ÿ��")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    private SeonHanAtk skill;

    #endregion

    private void Awake()
    {
        skill = GetComponent<SeonHanAtk>();

        #region null üũ
        if(skill == null)
        {
            Debug.LogError("SeonHanAI: Cannot GetComponent SenHanAtk.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endregion
    }

    private void Start()
    {
        Init(hp, myType, true); //<= AI �� ���� ���� �־�� ��
    }

    private void Update()
    {
        if(stat.myturn)
        {
            OnTurn();
        }
    }


    // ���� ��ų ��������� ���� �ٲ�� ��
    private void OnTurn()
    {
        int rndSkill = Random.Range(0, 4);

        switch (rndSkill)
        {
            case 0:
                skill.SkillA();
                break;

            case 1:
                skill.SkillB();
                break;

            case 2:
                skill.SkillC();
                break;

            case 3:
                skill.SkillD();
                break;
        }

    }
}
