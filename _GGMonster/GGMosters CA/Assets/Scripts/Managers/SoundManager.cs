using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 매뉴 이동은 소리가 겹칠 수 있다고 생각했어요.
    private Queue<AudioSource> changeQueue = new Queue<AudioSource>();

    [SerializeField] private AudioSource select = null;
    [SerializeField] private AudioSource change = null;

    private MenuKeyInput input;

    private void Awake()
    {
        input = GetComponent<MenuKeyInput>();

        InitQueue();
    }

    private void Update()
    {
        if(!SelectBtn.onAnimation && (input.inputUp || input.inputDown))
        {
            PlaySound();
        }

        if(!select.isPlaying && !SelectBtn.onAnimation && (input.inputSelect || input.inputRight))
        {
            select.Play();
        }
    }

    private void PlaySound()
    {
        AudioSource temp;

        if (changeQueue.Peek().isPlaying)
        {
            temp = Instantiate(change, transform);
            temp.Play();
            changeQueue.Enqueue(temp);
            return;
        }

        temp = changeQueue.Dequeue();
        temp.Play();
        changeQueue.Enqueue(temp);
    }



    private void InitQueue()
    {
        for(int i = 0; i < 3; ++i)
        {
            changeQueue.Enqueue(Instantiate(change, transform));
        }

        select = Instantiate(select, transform);
    }
}
