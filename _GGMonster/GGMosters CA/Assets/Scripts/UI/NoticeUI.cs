using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoticeUI : MonoBehaviour
{
    [Header("표현 관련")]
    [SerializeField] private Text   noticeText  = null; 
    [SerializeField] private Image  standing    = null;
    [SerializeField] private Button btnContinue = null;
    private bool             isOpen    = false;
    private bool             isAiUsing = false;
    private bool             endofturn = false;
    [SerializeField] private SpriteRenderer[] sprites   = new SpriteRenderer[2];


    [Header("이동 관련")]
    [SerializeField] private Transform originPos = null;
    [SerializeField] private Transform closePos  = null;
    [SerializeField] private float     dur       = 1.0f;

    private Queue<string> msgQueue = new Queue<string>();
    private Queue<bool> boolQueue = new Queue<bool>();
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


    void Start()
    {
        noticeObj = transform.GetChild(0);
        noticeObj.position = closePos.position;

        btnContinue.onClick.AddListener(ContinueInfo);
    }

    // SetMsg로 메세지 설정 후에 불러야 함
    public void CallNoticeUI(bool calledAtEndOfTurn = false, bool continues = false, bool calledByEnemy = false)
    {
        isAiUsing = calledByEnemy;
        endofturn = calledAtEndOfTurn;
        if (!continues)
        {
            OpenClose();
        }

        standing.sprite = calledByEnemy ? sprites[0].sprite : sprites[1].sprite;


        if (taskQueue.Peek() != null)
        {
            taskQueue.Dequeue()();
        }
        else
        {
            taskQueue.Dequeue();
        }
        noticeText.text = msgQueue.Dequeue();
        
    }

    // queue 에 msg 가 없을때까지 돌림
    public void SetMsg(string msg, NoticeTask task = null)
    {
        msgQueue.Enqueue(msg);
        taskQueue.Enqueue(task);
    }

    private void OpenClose()
    {
        noticeObj.DOMove(isOpen ? closePos.position : originPos.position, dur).SetEase(isOpen ? Ease.InCubic : Ease.OutCubic);
        isOpen = !isOpen;
    }

    private void ContinueInfo()
    {
        if(msgQueue.Count == 0)
        {
            OpenClose();

            if (endofturn) { TurnManager.instance.EndTurn(); }
            return;
        }

        CallNoticeUI(endofturn, true, isAiUsing);
    }
}
