using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundEnum
{
    LowDamaged = 0,
    HighDamaged,
    TickDamaged,
    Attacked,
    Healed,
    Dead,
    UISelect,
    UIBack,
    SOUNDEND
}

public class BattleSoundManager : MonoBehaviour
{
    // �ν��Ͻ�
    private static BattleSoundManager instance = null;

    // ���� ť ���� ����
    private Dictionary<SoundEnum, Queue<AudioSource>> soundDictionary = new Dictionary<SoundEnum, Queue<AudioSource>>();

    // Ǯ �޴�¡ ��
    private Queue<AudioSource>[] audioQueue;

    // ����� (Enum ������ ���ƾ� ��)
    public AudioSource[] audios = new AudioSource[1];

    private void Awake()
    {
        SetInstance();
        SetQueue();
        SetSound();
    }

    // �ν��Ͻ� �ʱ�ȭ
    private void SetInstance()
    {
        if (instance != null)
        {
            Debug.LogWarning("BattleSoundManager is running more than one in same scene.");
        }
        instance = this;
    }

    // Queue �迭 �ʱ�ȭ
    private void SetQueue()
    {
        audioQueue = new Queue<AudioSource>[(int)SoundEnum.SOUNDEND];

        // ť �迭 �� ���� �ʱ�ȭ
        for (int i = 0; i < (int)SoundEnum.SOUNDEND; ++i)
        {
            audioQueue[i] = new Queue<AudioSource>();
        }

        // �̸� �ν��Ͻ�ȭ ���� �׾��.
        for (int i = 0; i < (int)SoundEnum.SOUNDEND; ++i)
        {
            for (int objCount = 0; objCount < 3; ++objCount)
            {
                audioQueue[i].Enqueue(Instantiate(audios[i], this.transform));
            }
        }
    }

    // soundDictionary ���� �ʱ�ȭ
    private void SetSound()
    {
        for (int i = 0; i < (int)SoundEnum.SOUNDEND; ++i)
        {
            soundDictionary.Add((SoundEnum)i, audioQueue[i]);
        }
    }

    /// <summary>
    /// �Ű������� ���޹��� Enum �� ���� �Ҹ��� ����մϴ�.
    /// </summary>
    /// <param name="sound">����� �Ҹ��� Enum</param>
    public static void PlaySound(SoundEnum sound)
    {
        #region �Ű����� üũ
        if (!instance.soundDictionary.ContainsKey(sound))
        {
            Debug.LogError($"BattleSoundManager: Cannot find request sound\r\n{((int)sound < 1 || sound >= SoundEnum.SOUNDEND ? $"Requested key out of range, {(int)sound}" : $"Key: {sound}") }");
            return;
        }
        #endregion

        AudioSource audio = null;
        
        #region ����� �� �ִ� AudioSource ������Ʈ�� �ִ� �� Ȯ��
        if (instance.soundDictionary[sound].Peek().isPlaying)
        {
            audio = Instantiate(instance.audios[(int)sound], instance.transform);
        }
        else
        {
            audio = instance.soundDictionary[sound].Dequeue();
        }
        #endregion

        // �÷���
        audio.Play();
        instance.soundDictionary[sound].Enqueue(audio);
    }
}
