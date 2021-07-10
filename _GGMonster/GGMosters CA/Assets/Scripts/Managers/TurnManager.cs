using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance = null;

    public delegate void TurnEndTasks();
    public List<TurnEndTasks> turnEndTasks = new List<TurnEndTasks>(); // ���⿡ ���ϰ��� �ް������� ���� �Լ��� �־��ָ� ���� ���� �� ���� ������ �ȴ�.
    public List<TurnEndTasks> midturnTasks = new List<TurnEndTasks>(); // ���⿡ ���ϰ��� �ް������� ���� �Լ��� �־��ָ� ���� ���� �� ���� ������ �ȴ�.

    // ���߿� �̰� ���� ���׸� Ŭ������ ���� �Ẹ�°͵� �����������
    // �׽�ũ�޴��� (�۾�������)

    public uint turn = 2;
    public bool playerTurn = false;
    public bool enemyTurn = false;

    private Stat stat;

    private ISkill skill = null;
    private GameObject player = null;
    private GameObject enemy = null;


    #region Init Functions. Includes Awake
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("TurnManager: There are more then one TurnManager running on same scene");
        }
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        enemy  = GameObject.FindGameObjectWithTag("Enemy");

        stat = player.GetComponent<Stat>();

        #region null üũ
#if UNITY_EDITOR
        bool bStop = false;
        if(player == null)
        {
            Debug.LogError("TurnManager: Player is null");
            bStop = true;
        }
        if(enemy == null)
        {
            Debug.LogError("TurnManager: Enemy is null");
            bStop = true;
        }       
        if(stat == null)
        {
            Debug.LogError("TurnManager: Stat is nul");
            bStop = true;
        }

        if(bStop) { UnityEditor.EditorApplication.isPlaying = false; }
#endif
        #endregion


        SetFirstPlayer();
        SetFirstTurnStatus();

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

        if (!stat.startFirst) AIBase.turnPlayed = true;

        Debug.Log($"TurnManager: Player starts first? {stat.startFirst}");

        playerTurn = stat.startFirst;
        enemyTurn = !stat.startFirst;
    }
    #endregion

    // ���� ������ �ܺο��� ȣ��Ǵ� �Լ�
    public void EndTurn()
    {
        NextTurn();
        CallPassiveSkill();
        SetTurnStatus();

        DoTurnEndTasks();

        MidTurn();
    }

    #region Task foreach
    // �� �߰��� ���� �ʿ��� �� ȣ��Ǵ� �Լ�
    public void MidTurn()
    {
        DoMidturnTasks();
    }

    private void DoTurnEndTasks()
    {
        foreach(TurnEndTasks tasks in turnEndTasks)
        {
            tasks();
        }
    }

    private void DoMidturnTasks()
    {
        foreach(TurnEndTasks tasks in midturnTasks)
        {
            tasks();
        }
    }
    #endregion

    private void SetTurnStatus()
    {
        stat.myturn = !stat.myturn;
        stat.enemyStat.myturn = !stat.enemyStat.myturn;

        playerTurn = stat.myturn;
        enemyTurn = stat.enemyStat.myturn;
    }

    private void NextTurn()
    {
        ++turn;

        if (turn % 2 == (stat.enemyStat.startFirst ? 0 : 1)) // TODO : ����� ���� ���� ��� �θ��°� ���ƾ� ��, TurnEnd() ����
        {
            AIBase.turnPlayed = true;
        }
    }

    // ��ú� ��ų ȣ��
    private void CallPassiveSkill()
    {
        if (stat.isDead || stat.enemyStat.isDead) return;

        skill = player.GetComponent<ISkill>();
        TurnTask(skill);

        skill = null;
        
        skill = enemy.GetComponent<ISkill>();
        TurnTask(skill);
    }

    // �� ���ư������� ����ϴ°͵� ������
    private void TurnTask(ISkill skill)
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
