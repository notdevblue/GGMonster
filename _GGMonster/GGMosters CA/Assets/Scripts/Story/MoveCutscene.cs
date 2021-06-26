using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveCutscene : MonoBehaviour
{
    // �̵� ��ư
    [SerializeField] private Button       btnNext = null;
    [SerializeField] private Button       btnPrev = null;
                     private MenuKeyInput input   = null;

    [SerializeField] List<Transform> cutscenePos = new List<Transform>();

    // ī�޶� ���� ������Ʈ
    [SerializeField] private RectTransform followObject = null;

    // �̵� �ð�
    [SerializeField] private float moveDur = 0.3f;

    // �̷��� ¥�� �� ������ ������
    [SerializeField] private AudioSource swapSound = null;

    // ���
    private int currentStory = 0;
    private int maxStory = 0;

    private float moveDistance = -1;

    private Vector2 basePos;

    private void Start()
    {
        maxStory     = GameObject.FindGameObjectWithTag("CVSStory").transform.childCount;
        moveDistance = GameObject.FindGameObjectWithTag("CVSStory").GetComponent<GridLayoutGroup>().cellSize.x;
        input        = GetComponent<MenuKeyInput>();

        btnNext.onClick.AddListener(MoveRight);
        btnPrev.onClick.AddListener(MoveLeft);

        basePos = followObject.position;

        InitQueue();
    }

    private void Update()
    {
        if(input.inputRight || input.inputSelect)
        {
            MoveRight();
        }
        if(input.inputLeft)
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        if (currentStory + 1 > maxStory - 1) { UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2); return; }
        ++currentStory;
        followObject.transform.DOMoveX(cutscenePos[currentStory].position.x, moveDur);
        PlaySound();
    }

    private void MoveLeft()
    {
        if (currentStory - 1 < 0) return;
        --currentStory;
        followObject.transform.DOMoveX(cutscenePos[currentStory].position.x, moveDur);
        PlaySound();
    }


    private Queue<AudioSource> queue = new Queue<AudioSource>();
    private void InitQueue()
    {
        for (int i = 0; i < 3; ++i)
            queue.Enqueue(Instantiate(swapSound, transform));
    }


    private void PlaySound()
    {
        AudioSource temp;

        if(queue.Peek().isPlaying)
        {
            temp = Instantiate(swapSound, transform);
            temp.Play();
            queue.Enqueue(temp);
        }
        else
        {
            temp = queue.Dequeue();
            temp.Play();
            queue.Enqueue(temp);
        }
    }
}
