using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectBtn : MonoBehaviour
{
    private MenuKeyInput input = null;
    private WindowEffects window = null;


    // 버튼 이동 관련
    [SerializeField] private float           moveDur         = 0.2f;
                     private RectTransform   originPos;
    [SerializeField] private RectTransform[] targetLocations = new RectTransform[3];
                     private int             curPosIdx       = 0;
    [SerializeField] private Button[]        btnMenus        = new Button[3];

    // 버튼 선택 관련
    [SerializeField] private MenuFunctions[] menuFunc = new MenuFunctions[3];
                     private delegate void   MenuFunctions();


    // 선택 에니메이션 관련
    [SerializeField] private float selectMoveAmount = 0.4f;
    [SerializeField] private float animSpeed        = 2.0f;
    [SerializeField] private float bounceAmount     = 100.0f;
              static public  bool  onAnimation      { get; private set; }


    // 페이드 아웃
    [SerializeField] private Image fader = null;

    float temp = 0.0f;
    void Start()
    {
        input       = FindObjectOfType<MenuKeyInput>();
        window      = FindObjectOfType<WindowEffects>();
        originPos   = GetComponent<RectTransform>();
        curPosIdx   = 0;
        onAnimation = false;


        #region Delegate Init

        menuFunc[0] = () => { DOTween.Clear(); window.ToFullScreen(10.0f, false, () => { onAnimation = false; UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(input.konami ? "Loading" : "MainStory"); }); };
        menuFunc[1] = () => {  };
        menuFunc[2] = () =>
        {
            #region Quit

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit(0); 
            #endregion
        };
        #endregion

        window.ToWindowed(1280, 720, 0, true);
        window.ResetPositionVar();
        window.Middle(0, true);

        // 선택 버튼을 광클하는 경우 조금씩 오른쪽으로 가는 버그가 있기 때문
        temp = originPos.position.x;
    }

    void Update()
    {
        if (onAnimation) return;

        MoveCursor();
        Select();
    }

    // 버튼 이동 관련
    private void MoveCursor()
    {
        if(input.inputUp)
        {
            onAnimation = true;

            if (curPosIdx <= 0)
            {
                curPosIdx = 2;
                transform.DOMove(targetLocations[curPosIdx].position, moveDur);
                window.BounceUp(animSpeed, bounceAmount, () => { window.BounceDown(animSpeed, bounceAmount, () => { window.Middle(0, true); onAnimation = false; }); });
            }
            else
            {
                transform.DOMove(targetLocations[--curPosIdx % 3].position, moveDur);
                window.BounceUp(animSpeed, bounceAmount, () => { window.Middle(0, true); onAnimation = false; });
            }
        }
        if(input.inputDown)
        {
            onAnimation = true;

            if (curPosIdx >= 2)
            {
                curPosIdx = 0;
                transform.DOMove(targetLocations[curPosIdx].position, moveDur);
                window.BounceDown(animSpeed, bounceAmount, () => { window.BounceUp(animSpeed, bounceAmount, () => { window.Middle(0, true); onAnimation = false; }); });
            }
            else
            {
                transform.DOMove(targetLocations[++curPosIdx % 3].position, moveDur);
                window.BounceDown(animSpeed, bounceAmount, () => { window.Middle(0, true); onAnimation = false; });
            }


        }
    }

    // 버튼 선택 관련
    private void Select()
    {
        if(input.inputSelect || input.inputRight)
        {
            onAnimation = true;
            
            SelectAnimation();
        }
    }

    // 선택 에니메이션
    private void SelectAnimation()
    {
        // 신 전환 에니메이션
        if (curPosIdx % 3 == 0)
        {
            fader.DOFade(1, moveDur);
        }

        // 창 움직임
        window.BounceRight(animSpeed, bounceAmount / 2.0f, () => { onAnimation = false; window.Middle(0, true); menuFunc[curPosIdx % 3](); });

        // 선택
        transform.DOMoveX(temp + selectMoveAmount, moveDur / 2.0f).OnComplete(() =>
        {
                transform.DOMoveX(temp, moveDur / 2.0f);
        });
    }


}
