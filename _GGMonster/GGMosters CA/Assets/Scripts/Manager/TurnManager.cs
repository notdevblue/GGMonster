using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance = null;

    public delegate void TurnEndTasks();
    public List<TurnEndTasks> turnEndTasks = new List<TurnEndTasks>(); // 여기에 리턴값과 메개변수가 없는 함수를 넣어주면 턴이 끝날 때 마다 실행이 된다.
    public List<TurnEndTasks> midturnTasks = new List<TurnEndTasks>(); // 여기에 리턴값과 메개변수가 없는 함수를 넣어주면 턴이 끝날 때 마다 실행이 된다.

    // 나중에 이걸 따로 제네릭 클래스로 만들어서 써보는것도 재미있을듯함
    // 테스크메니저 (작업관리자)

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("TurnManager: There are more then one TurnManager running on same scene");
        }
        instance = this;
    }

    public uint turn = 1;
    public bool playerTurn = false;
    public bool enemyTurn = false;

    private Stat stat;

    private ISkill skill = null;
    private GameObject player = null;
    private GameObject enemy = null;


    #region Init Functions. Includes Awake
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy  = GameObject.FindGameObjectWithTag("Enemy");

        stat = player.GetComponent<Stat>();

        #region null 체크
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
    // 본인 스텟 설정. (아레 함수와 연관 있음)
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

    // 누가 처음으로 시작하는지
    private void SetFirstTurnStatus()
    {
        stat.myturn = stat.startFirst;
        stat.enemyStat.myturn = !stat.startFirst;

        Debug.Log($"TurnManager: Player starts first? {stat.startFirst}");
    }
    #endregion

    // 턴이 끝날때 외부에서 호출되는 함수
    public void EndTurn()
    {
        NextTurn();
        Debug.LogWarning($"턴 바뀜. 턴: {turn}");
        CallPassiveSkill();
        SetTurnStatus();

        DoTurnEndTasks();

        MidTurn();
    }

    #region Task foreach
    // 턴 중간에 무언가 필요할 때 호출되는 함수
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
    }

    private void NextTurn()
    {
        ++turn;
    }

    // 페시브 스킬 호출
    private void CallPassiveSkill()
    {
        skill = player.GetComponent<ISkill>();
        TurnTask(skill);

        skill = null;
        
        skill = enemy.GetComponent<ISkill>();
        TurnTask(skill);
    }

    // 턴 돌아갈때마다 헤야하는것들 모음집
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
