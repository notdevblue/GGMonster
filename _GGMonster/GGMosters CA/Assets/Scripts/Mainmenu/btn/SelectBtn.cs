using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectBtn : MonoBehaviour
{
    private MenuKeyInput input = null;

    // ��ư �̵� ����
    [SerializeField] private float           moveDur         = 0.2f;
    [SerializeField] private RectTransform[] targetLocations = new RectTransform[3];
                     private int             curPosIdx       = 0;

    // ��ư ���� ����
    [SerializeField] private MenuFunctions[] menuFunc = new MenuFunctions[3];
                     private delegate void   MenuFunctions();


    // ���� ���ϸ��̼� ����
    [SerializeField] private float selectMoveAmount = 0.4f;
                     public  bool  onAnimation      { get; private set; }

    // ���̵� �ƿ�
    [SerializeField] private Image fader = null;

    void Start()
    {
        input = FindObjectOfType<MenuKeyInput>();

        curPosIdx = 0;


        menuFunc[0] = () => { UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(input.konami ? "Loading" : "MainStory"); };
        menuFunc[1] = () => { };
        menuFunc[2] = () => { Application.Quit(0); };

        onAnimation = false;
    }

    void Update()
    {
        if (onAnimation) return;

        MoveCursor();
        Select();
    }

    // ��ư �̵� ����
    private void MoveCursor()
    {
        if(input.inputUp)
        {
            transform.DOMove(targetLocations[curPosIdx <= 0 ? curPosIdx = 2 : --curPosIdx % 3].position, moveDur);
        }
        if(input.inputDown)
        {
            transform.DOMove(targetLocations[++curPosIdx % 3].position, moveDur);
        }
    }

    // ��ư ���� ����
    private void Select()
    {
        if(input.inputSelect || input.inputRight)
        {
            onAnimation = true;
            
            SelectAnimation();
        }
    }

    // ���� ���ϸ��̼�
    private void SelectAnimation()
    {
        // �� ��ȯ ���ϸ��̼�
        if(curPosIdx % 3 == 0)
        {
            fader.DOFade(1, moveDur);
        }

        // ����
        transform.DOMoveX(transform.position.x + selectMoveAmount, moveDur / 2.0f).OnComplete(() =>
            { transform.DOMoveX(transform.position.x - selectMoveAmount, moveDur / 2.0f).OnComplete(() =>
            { onAnimation = false; menuFunc[curPosIdx % 3](); });
        });
    }


}
