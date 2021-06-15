using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance = null;

    public uint turn = 0;
    public bool playerTurn = false;
    public bool enemyTurn = false;

    [SerializeField] private Stat stat;

    private ISKill skill = null;
    private GameObject player = null;
    private GameObject enemy = null;


    #region Init Functions. Includes Awake
    private void Start()
    {
        SingleTon();
        SetFirstPlayer();
        SetFirstTurnStatus();

        player = GameObject.FindGameObjectWithTag("Player");
        enemy  = GameObject.FindGameObjectWithTag("Enemy");

        #region null üũ
        if(player == null)
        {
            Debug.LogError("Player is null");
        }
        if(enemy == null)
        {
            Debug.LogError("Enemy is null");
        }       
        #endregion
    }

    private void SingleTon()
    {
        if (instance != null)
        {
            Debug.LogWarning("TurnManager: I exists more than one");
        }
        instance = this;
    }

    // ���� ���� ����. (�Ʒ� �Լ��� ���� ����)
    private void SetFirstPlayer()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                stat.startFirst = false;
                break;

            case 1:
                stat.startFirst = true;
                break;

            default:
                Debug.LogError("TurnManager: Undefined Turn status");
                break;
        }
    }

    // ���� ó������ �����ϴ���
    private void SetFirstTurnStatus()
    {
        stat.myturn = stat.startFirst;
        stat.enemyStat.myturn = !stat.startFirst;

        Debug.Log($"TurnManager: Player starts first? {stat.startFirst}");
    }
    #endregion

    public void EndTurn()
    {
        NextTurn();
        CallPassiveSkill();
        SetTurnStatus();
    }

    private void SetTurnStatus()
    {
        stat.myturn = !stat.myturn;
        stat.enemyStat.myturn = !stat.enemyStat.myturn;
    }

    private void NextTurn()
    {
        ++turn;
    }

    // ��ú� ��ų ȣ��
    private void CallPassiveSkill()
    {
        skill = player.GetComponent<ISKill>();
        TurnTask(skill);

        skill = null;
        
        skill = enemy.GetComponent<ISKill>();
        TurnTask(skill);
    }

    // �� ���ư������� ����ϴ°͵� ������
    private void TurnTask(ISKill skill)
    {
        if (skill != null)
        {
            if(!skill.CheckSP())
            {
                skill.SkillE();
                return;
            }
            skill.Passive();
        }
    }
}
