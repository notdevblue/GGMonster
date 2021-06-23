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
    private SpriteRenderer[] sprites   = new SpriteRenderer[2];


    [Header("이동 관련")]
    [SerializeField] private Transform originPos = null;
    [SerializeField] private Transform closePos  = null;
    [SerializeField] private float     dur       = 1.0f;

    private Queue<string> msgQueue = new Queue<string>();

    private Transform noticeObj = null;

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

    // SetMsg로 메세지 설정 후에 불러야 함
    public void CallNoticeUI(bool continues = false, bool calledByEnemy = false)
    {
        isAiUsing = calledByEnemy;
        if (!continues)
        {
            OpenClose();
        }

        standing.sprite = calledByEnemy ? sprites[0].sprite : sprites[1].sprite;
        noticeText.text = msgQueue.Dequeue();
    }

    #region SetMsg()
    // queue 에 msg 가 없을때까지 돌림
    public void SetMsg(string msg)
    {
        msgQueue.Enqueue(msg);
    }
    public void SetMsg(string[] msg)
    {
        for(int i = 0; i < msg.Length; ++i)
        {
            msgQueue.Enqueue(msg[i]);
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
            return;
        }

        CallNoticeUI(true, isAiUsing);
    }
}
