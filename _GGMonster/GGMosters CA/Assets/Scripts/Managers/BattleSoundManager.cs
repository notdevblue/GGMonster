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
    // 인스턴스
    private static BattleSoundManager instance = null;

    // 사운드 큐 담은 사전
    private Dictionary<SoundEnum, Queue<AudioSource>> soundDictionary = new Dictionary<SoundEnum, Queue<AudioSource>>();

    // 풀 메니징 용
    private Queue<AudioSource>[] audioQueue;

    // 사운드들 (Enum 순서와 같아야 함)
    public AudioSource[] audios = new AudioSource[1];

    private void Awake()
    {
        SetInstance();
        SetQueue();
        SetSound();
    }

    // 인스턴스 초기화
    private void SetInstance()
    {
        if (instance != null)
        {
            Debug.LogWarning("BattleSoundManager is running more than one in same scene.");
        }
        instance = this;
    }

    // Queue 배열 초기화
    private void SetQueue()
    {
        audioQueue = new Queue<AudioSource>[(int)SoundEnum.SOUNDEND];

        // 큐 배열 속 원소 초기화
        for (int i = 0; i < (int)SoundEnum.SOUNDEND; ++i)
        {
            audioQueue[i] = new Queue<AudioSource>();
        }

        // 미리 인스턴스화 시켜 뒀어요.
        for (int i = 0; i < (int)SoundEnum.SOUNDEND; ++i)
        {
            for (int objCount = 0; objCount < 3; ++objCount)
            {
                audioQueue[i].Enqueue(Instantiate(audios[i], this.transform));
            }
        }
    }

    // soundDictionary 사전 초기화
    private void SetSound()
    {
        for (int i = 0; i < (int)SoundEnum.SOUNDEND; ++i)
        {
            soundDictionary.Add((SoundEnum)i, audioQueue[i]);
        }
    }

    /// <summary>
    /// 매개변수로 전달받은 Enum 에 따라 소리를 재생합니다.
    /// </summary>
    /// <param name="sound">재생할 소리의 Enum</param>
    public static void PlaySound(SoundEnum sound)
    {
        #region 매개변수 체크
        if (!instance.soundDictionary.ContainsKey(sound))
        {
            Debug.LogError($"BattleSoundManager: Cannot find request sound\r\n{((int)sound < 1 || sound >= SoundEnum.SOUNDEND ? $"Requested key out of range, {(int)sound}" : $"Key: {sound}") }");
            return;
        }
        #endregion

        AudioSource audio = null;
        
        #region 사용할 수 있는 AudioSource 오브젝트가 있는 지 확인
        if (instance.soundDictionary[sound].Peek().isPlaying)
        {
            audio = Instantiate(instance.audios[(int)sound], instance.transform);
        }
        else
        {
            audio = instance.soundDictionary[sound].Dequeue();
        }
        #endregion

        // 플레이
        audio.Play();
        instance.soundDictionary[sound].Enqueue(audio);
    }
}
