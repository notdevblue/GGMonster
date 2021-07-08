using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoticeUI : MonoBehaviour
{
    [Header("표현 관련")]
    [SerializeField] private Text     noticeText  = null; 
    [SerializeField] private Image    standing    = null;
    [SerializeField] private Image    standBG     = null;
    [SerializeField] private Button   btnContinue = null;
                     private bool     isOpen      = false;
                     private bool     isAiUsing   = false;
                     private bool     endofturn   = false;
                     private bool     forcePlayer = false;
                     private bool     forceEnemy  = false;
                     private Color    enemyColor  = new Color(1.0f, 0.333f, 0.0f);
                     private Color    playerColor = new Color(0.0f, 1.0f,   0.6f);
    [SerializeField] private Sprite[] sprites     = new Sprite[2];


    [Header("이동 관련")]
    [SerializeField] private Transform originPos = null;
    [SerializeField] private Transform closePos  = null;
    [SerializeField] private float     dur       = 1.0f;

    private Queue<string> msgQueue = new Queue<string>();
    private Queue<NoticeTask> taskQueue = new Queue<NoticeTask>();

    private Transform noticeObj = null;

    public delegate void NoticeTask();

    #region singleton
    public static NoticeUI instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private bool isClosed = true;
    private bool closing  = false;
    // 이미 올라와 있는 상태에서 또 불린 경우
    private bool onMsg = false;

    void Start()
    {
        noticeObj = transform.GetChild(0);
        noticeObj.position = closePos.position;

        btnContinue.onClick.AddListener(ContinueInfo);
    }

    // SetMsg로 메세지 설정 후에 불러야 함
    private Sprite lastSprite;

    public void CallNoticeUI(bool calledAtEndOfTurn = false, bool firstCall = false, bool calledByEnemy = false, bool forcePlayer = false, bool forceEnemy = false, Sprite spr = null, bool forceOpen = false)
    {
        if (firstCall)
        {
            if(forceOpen || isClosed)
            {
                OpenClose();
            }
            else if (!isClosed)
            {
                onMsg = true;
            }
        }

        isClosed = false;

        lastSprite       = spr;
        isAiUsing        = calledByEnemy;
        endofturn        = calledAtEndOfTurn;
        this.forcePlayer = forcePlayer;
        this.forceEnemy  = forceEnemy;

        DoNoticeTask();

        if (onMsg) return;
        
        if(calledByEnemy)
        {
            standBG.color = enemyColor;
        }
        else
        {
            standBG.color = playerColor;
        }

        #region Sets charactor sprite
        if (spr != null)
        {
            // 대부분 스킬 스프라이트
            standing.sprite = spr;
        }
        else if (forcePlayer)
        {
            standing.sprite = sprites[0];
        }
        else if(forceEnemy)
        {
            standing.sprite = sprites[1];
        }
        else
        {
            standing.sprite = TurnManager.instance.playerTurn ? sprites[0] : sprites[1];
        }
        #endregion
    }

    // queue 에 msg 가 없을때까지 돌림
    public void SetMsg(string msg, NoticeTask task = null)
    {
        msgQueue.Enqueue(msg);
        taskQueue.Enqueue(task);
    }

    // TODO : UI 가 올라와 있는 상태에서 이걸 한번 더 부르면 강재로 하나가 넘어가게 됨
    private void DoNoticeTask()
    {
        // Msg
        if (msgQueue.Count != 0 && !onMsg)
        {
            noticeText.text = msgQueue.Dequeue();
        }
        else
        {
            onMsg = false;
            return;
        }

        // Task
        if (taskQueue.Count == 0) { }
        else if (taskQueue.Peek() != null)
        {
            taskQueue.Dequeue()();
        }
        else
        {
            taskQueue.Dequeue();
        }
    }

    private void OpenClose()
    {
        btnContinue.enabled = false;
        closing = true;
        noticeObj.DOMove(isOpen ? closePos.position : originPos.position, dur).SetEase(isOpen ? Ease.InCubic : Ease.OutCubic).OnComplete(() => { closing = false; btnContinue.enabled = true; });
        isOpen = !isOpen;
    }

    private void ContinueInfo()
    {
        if (closing) return;

        if(msgQueue.Count == 0)
        {
            OpenClose();
            msgQueue.Clear();
            if (endofturn && !isClosed) { TurnManager.instance.EndTurn(); isClosed = true;}
            else { isClosed = true; }
            return;
        }
        CallNoticeUI(endofturn, false, isAiUsing, forcePlayer, forceEnemy, lastSprite);
    }
}
