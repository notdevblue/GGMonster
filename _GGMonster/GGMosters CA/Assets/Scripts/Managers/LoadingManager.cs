using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingObj     = null;
    [SerializeField] private Button     nextScene      = null;
    [SerializeField] private Text       loadingText    = null;
    [SerializeField] private Text       loadingEndText = null;

    private AsyncOperation load;

    private void Start()
    {
        nextScene.gameObject.SetActive(false);
        loadingEndText.gameObject.SetActive(false);
        StartCoroutine(LoadingStatus());
    }

    private void Update()
    {
        if (loadingEndText.gameObject.activeSelf && Input.anyKeyDown)
        {
            load.allowSceneActivation = true;
        }
    }

    IEnumerator LoadingStatus()
    {
        string scene = "Title";

        if(SceneHistory.sceneHistory.Count != 0)
        {
            switch (SceneHistory.sceneHistory.Peek()) // TODO : ��ä���� ��Ģ�� ��߳��� �ڵ�
            {
                case "MainStory":
                    scene = "SeonhanBattle";
                    break;

                case "SeonhanBattle":
                    scene = "MainMenu";
                    break;

                case "KONAMI":
                    scene = "ShivaBattle";
                    break;

                case "SeonHanScene":
                    scene = "Title"; // TODO : ���� ���������� �̵��ؾ� ��
                    break;

            }
        }
        else
        {
            Debug.LogWarning("No previous scene detected");
            scene = "Title";
        }

        Debug.Log(scene);
        load = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
        load.allowSceneActivation = false;
        while (load.progress >= 0.9f)
        {
            loadingObj.transform.eulerAngles += Vector3.forward;
            yield return null;
        }
        nextScene.gameObject.SetActive(true);
        loadingEndText.gameObject.SetActive(true);
        loadingText.text = "Loaded!";
    }
}
