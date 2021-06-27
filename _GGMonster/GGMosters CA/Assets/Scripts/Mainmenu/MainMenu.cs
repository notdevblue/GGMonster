using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private MenuKeyInput input = null;

    #region Easter Egg
    [SerializeField] private Image mainCharactorImage = null;
    [SerializeField] private Sprite shibaTeacher = null;

    [SerializeField] private GameObject ground = null;
    [SerializeField] private GameObject konamiText = null;
    #endregion

    private void Awake()
    {
        input = GetComponent<MenuKeyInput>();
    }

    private void FixedUpdate()
    {
        Konami();        
    }


    private void Konami()
    {
        if (input.konami)
        {
            mainCharactorImage.sprite = shibaTeacher;
            ground.SetActive(false);
            konamiText.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (input.konami)
        {
            SceneHistory.sceneHistory.Push("KONAMI");
        }
    }
}
