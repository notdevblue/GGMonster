using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectBtn : MonoBehaviour
{
    private MenuKeyInput input = null;
    private WindowEffects window = null;


    // ��ư �̵� ����
    [SerializeField] private float           moveDur         = 0.2f;
    [SerializeField] private RectTransform[] targetLocations = new RectTransform[3];
                     private int             curPosIdx       = 0;
    [SerializeField] private Button[]        btnMenus        = new Button[3];

    // ��ư ���� ����
    [SerializeField] private MenuFunctions[] menuFunc = new MenuFunctions[3];
                     private delegate void   MenuFunctions();


    // ���� ���ϸ��̼� ����
    [SerializeField] private float selectMoveAmount = 0.4f;
                     public  bool  onAnimation      { get; private set; }

    // ���̵� �ƿ�
    [SerializeField] private Image fader = null;

    // ���� ������ �ȵ�����
    // ��äȭ�� �ڵ�
    bool isFullscreen = true;


    void Start()
    {
        input  = FindObjectOfType<MenuKeyInput>();
        window = FindObjectOfType<WindowEffects>();

        curPosIdx   = 0;
        onAnimation = false;

        #region AddListener

        btnMenus[0].onClick.AddListener(() =>
        {
            if (curPosIdx % 3 != 0)
            {
                transform.DOMove(targetLocations[0].position, moveDur);
                curPosIdx = 0;
                return;
            }

            DOTween.Clear();
            menuFunc[0]();
        });
        
        btnMenus[1].onClick.AddListener(() =>
        {
            if (curPosIdx % 3 != 1)
            {
                transform.DOMove(targetLocations[1].position, moveDur);
                curPosIdx = 1;
                return;
            }

            menuFunc[1]();

        });

        btnMenus[2].onClick.AddListener(() =>
        {
            if (curPosIdx % 3 != 2)
            {
                transform.DOMove(targetLocations[2].position, moveDur);
                curPosIdx = 2;
                return;
            }
            menuFunc[2]();
        });
        #endregion
        #region Delegate Init

        menuFunc[0] = () => { DOTween.Clear(); UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(input.konami ? "Loading" : "MainStory"); };
        menuFunc[1] = () => { isFullscreen = !isFullscreen;  Screen.SetResolution(isFullscreen ? 1920 : 1280, isFullscreen ? 1080 : 720, isFullscreen); };
        menuFunc[2] = () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit(0); 
        };
        #endregion

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
