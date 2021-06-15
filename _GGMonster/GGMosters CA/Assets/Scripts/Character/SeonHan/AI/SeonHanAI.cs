using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAI : AIBase
{
    #region 변수
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("타입")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    private SeonHanAtk skill;

    #endregion

    private void Awake()
    {
        skill = GetComponent<SeonHanAtk>();

        #region null 체크
        if(skill == null)
        {
            Debug.LogError("SeonHanAI: Cannot GetComponent SenHanAtk.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endregion
    }

    private void Start()
    {
        Init(hp, myType, true); //<= AI 라서 먼저 적이 있어야 함
    }

    private void Update()
    {
        if(stat.myturn)
        {
            OnTurn();
        }
    }


    // 랜덤 스킬 사용이지만 추후 바꿔야 함
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
