using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoticeUI : MonoBehaviour
{
    [Header("ǥ�� ����")]
    [SerializeField] private Text   noticeText  = null; 
    [SerializeField] private Image  standing    = null;
    [SerializeField] private Button btnContinue = null;
    private bool             isOpen    = false;
    private bool             isAiUsing = false;
    private bool             endofturn = false;
    private SpriteRenderer[] sprites   = new SpriteRenderer[2];


    [Header("�̵� ����")]
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


    void Start()
    {
        noticeObj = transform.GetChild(0);
        noticeObj.position = closePos.position;

        sprites[0] = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprites[1] = GameObject.FindGameObjectWithTag("Enemy").transform.GetChild(0).GetComponent<SpriteRenderer>();

        btnContinue.onClick.AddListener(ContinueInfo);
    }

    // SetMsg�� �޼��� ���� �Ŀ� �ҷ��� ��
    public void CallNoticeUI(bool continues = false, bool calledByEnemy = false, bool calledAtEndOfTurn = false)
    {
        isAiUsing = calledByEnemy;
        endofturn = calledAtEndOfTurn;
        if (!continues)
        {
            OpenClose();
        }

        standing.sprite = calledByEnemy ? sprites[0].sprite : sprites[1].sprite;
        noticeText.text = msgQueue.Dequeue();
    }

    #region SetMsg()
    // queue �� msg �� ���������� ����
    public void SetMsg(string msg, NoticeTask task = null)
    {
        msgQueue.Enqueue(msg);
        taskQueue.Enqueue(task);
    }
    public void SetMsg(string[] msg, NoticeTask task = null)
    {
        for(int i = 0; i < msg.Length; ++i)
        {
            msgQueue.Enqueue(msg[i]);
            taskQueue.Enqueue(task);
        }
    }
    #endregion

    private void OpenClose()
    {
        noticeObj.DOMove(isOpen ? closePos.position : originPos.position, dur).SetEase(isOpen ? Ease.InCubic : Ease.OutCubic);
        isOpen = !isOpen;
    }

    private void ContinueInfo()
    {
        if(msgQueue.Peek() == null)
        {
            OpenClose();

            if (endofturn) { TurnManager.instance.EndTurn(); }
            return;
        }

        CallNoticeUI(true, isAiUsing);
    }
}
