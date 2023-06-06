using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    AudioSource BgmPlayer;
    public float BgmVolume;
    public AudioClip BgmClip;

    [Header("#SFX")]
    AudioSource[] SfxPlayers;
    int channelIndex = 0;
    public float SfxVolume;
    public int channels;
    public AudioClip[] SfxClips;

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win };

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // Initialize BgmPlayer
        GameObject BgmObject = new GameObject("BgmPlayer");
        BgmObject.transform.parent = gameObject.transform;
        BgmPlayer = BgmObject.AddComponent<AudioSource>();
        BgmPlayer.playOnAwake = false;
        BgmPlayer.loop = true;
        BgmPlayer.volume = BgmVolume;
        BgmPlayer.clip = BgmClip;

        // Initialize SfxPlayer
        GameObject SfxObject = new GameObject("SfxPlayer");
        SfxObject.transform.parent = gameObject.transform;
        SfxPlayers = new AudioSource[channels];

        for (int i = 0; i < channels; i++)
        {
            SfxPlayers[i] = SfxObject.AddComponent<AudioSource>();
            SfxPlayers[i].playOnAwake = false;
            SfxPlayers[i].volume = SfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay) BgmPlayer.Play();
        else BgmPlayer.Stop();
    }

    public void PlayAudioEffect(bool isPlay)
    {
        Camera.main.GetComponent<AudioHighPassFilter>().enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        Sfx[] sfxValues = (Sfx[])Enum.GetValues(sfx.GetType());
        int nextIndex = Array.IndexOf(sfxValues, sfx);
        int clipCount = (nextIndex >= sfxValues.Length ? SfxClips.Length : (int)sfxValues[nextIndex]) - (int)sfx;

        for (int i = 0; i < SfxClips.Length; i++)
        {
            int loopIndex = (channelIndex + i) % SfxClips.Length;
            if (SfxPlayers[loopIndex].isPlaying) continue;

            int clipIndex = UnityEngine.Random.Range(0, clipCount);
            SfxPlayers[loopIndex].clip = SfxClips[(int)sfx + clipIndex];
            SfxPlayers[loopIndex].bypassListenerEffects = true;
            SfxPlayers[loopIndex].Play();
            channelIndex = loopIndex;
            break;
        }
    }
}
