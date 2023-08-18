using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [Header("“Ù¿÷")]
    public AudioSource music;         //“Ù–ß≤•∑≈∆˜
    [SerializeField] float musicVolume = 1;
    // [SerializeField] float musicPitch = 1;
    Coroutine switchMusicCoroutine;
    float currentMusicVolume;
    public AudioClip mainMenuMusic;
    public AudioClip gamePlayMusic;
    public AudioClip gameOverRoomMusic;
    public AudioClip scoreMusic;

    [Header("“Ù–ß")]
    public AudioSource sfx;         //“Ù–ß≤•∑≈∆˜
    [SerializeField] float sfxVolume = 1;
    // [SerializeField] float sfxPitch = 1;
    [SerializeField] float minSFX_Pitch = 0.9f;
    [SerializeField] float maxSFX_Pitch = 1.1f;

    [Header("UI")]
    [SerializeField] AudioSource ui;         //“Ù–ß≤•∑≈∆˜
    [SerializeField] float uiVolume = 1;
    // [SerializeField] float uiPitch = 1;


    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        EventCenter.Instance.AddEventListener<float>("SetMusicVolumeValue", SetMusicVolumeValue);
        EventCenter.Instance.AddEventListener<float>("SetSFXVolumeValue", SetSFXVolumeValue);
        EventCenter.Instance.AddEventListener<float>("SetSFXVolumeValue", SetUIVolumeValue);

        EventCenter.Instance.AddEventListener("ReturnMainMenu", PlayMainMenuMusic);
        EventCenter.Instance.AddEventListener("LoadGamePlayScene", PlayGamePlayMusic);
        EventCenter.Instance.AddEventListener("LoadPlayerDataScene", PlayScoreMusic);
        EventCenter.Instance.AddEventListener("LoadGameOverRoomScene", PlayGameOverRoomMusic);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<float>("SetMusicVolumeValue", SetMusicVolumeValue);
        EventCenter.Instance.RemoveEventListener<float>("SetSFXVolumeValue", SetSFXVolumeValue);
        EventCenter.Instance.RemoveEventListener<float>("SetSFXVolumeValue", SetUIVolumeValue);

        EventCenter.Instance.RemoveEventListener("ReturnMainMenu", PlayMainMenuMusic);
        EventCenter.Instance.RemoveEventListener("LoadGamePlayScene", PlayGamePlayMusic);
        EventCenter.Instance.RemoveEventListener("LoadPlayerDataScene", PlayScoreMusic);
        EventCenter.Instance.RemoveEventListener("LoadGameOverRoomScene", PlayGameOverRoomMusic);
    }

    public void AudioFadeUpdateVolume(AudioSource audioSource, float toValue, float time)
    {
        audioSource.DOFade(toValue, time);
    }

    #region Music
    public void PlayMusic(AudioClip audio)
    {
        music.Play();
    }

    public void SetMusicVolumeValue(float value)
    {
        musicVolume = value;
        music.volume = musicVolume;
    }

    public void PlayMainMenuMusic()
    {
        SwitchMusic(mainMenuMusic);
    }
    public void PlayGamePlayMusic()
    {
        SwitchMusic(gamePlayMusic);
    }
    public void PlayGameOverRoomMusic()
    {
        SwitchMusic(gameOverRoomMusic);
    }
    public void PlayScoreMusic()
    {
        SwitchMusic(scoreMusic);
    }

    public void SwitchMusic(AudioClip musicClip)
    {
        currentMusicVolume = music.volume;
        if(switchMusicCoroutine != null)
            StopCoroutine(switchMusicCoroutine);
        switchMusicCoroutine = StartCoroutine(SwitchMusicCoroutine(musicClip));
    }

    IEnumerator SwitchMusicCoroutine(AudioClip musicClip)
    {
        music.DOFade(0, 0.3f);
        yield return new WaitUntil(() => music.volume == 0);
        music.clip = musicClip;
        music.Play();
        music.DOFade(currentMusicVolume, 0);
        musicVolume = currentMusicVolume;
    }
    #endregion

    #region SFX
    public void PlaySFX(AudioClip audio)
    {
        sfx.PlayOneShot(audio, sfxVolume);
    }

    public void PlaySFX_RandomPitch(AudioClip audio)
    {
        sfx.pitch = Random.Range(minSFX_Pitch, maxSFX_Pitch);
        PlaySFX(audio);
    }

    public void SetSFXVolumeValue(float value)
    {
        sfxVolume = value;
        sfx.volume = sfxVolume;
    }
    #endregion

    #region UI
    public void PlayUI(AudioClip audio)
    {
        ui.PlayOneShot(audio, sfxVolume);
    }

    public void SetUIVolumeValue(float value)
    {
        uiVolume = value;
        ui.volume = uiVolume;
    }
    #endregion

}
