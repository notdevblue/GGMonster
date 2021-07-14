using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button btnPause = null;
    [SerializeField] private GameObject pausePannel = null;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private Button btnContinue;


    public static bool returned { get; private set; }

    private void Awake()
    {
        returned = false;

        btnPause.onClick.AddListener(OnESC);
        btnContinue.onClick.AddListener(OnESC);

        btnMainMenu.onClick.AddListener(() =>
        {
            returned = true;
            SceneHistory.sceneHistory.Clear();
            SceneHistory.sceneHistory.Push("ETM");
            DOTween.Clear();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            Time.timeScale = 1.0f;
        });
    }


    private void Update()
    {
        InputESC();
    }


    private void InputESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnESC();
        }
    }

    private void OnESC()
    {

        pausePannel.SetActive(!pausePannel.activeSelf);
        Time.timeScale = pausePannel.activeSelf ? 0.0f : 1.0f;
        BattleSoundManager.PlaySound(pausePannel.activeSelf ? SoundEnum.UISelect : SoundEnum.UIBack);
    }
}
