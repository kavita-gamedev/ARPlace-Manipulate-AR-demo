using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource Tutorial_AudioSource;
    public AudioSource audioBtnSource;
   
    public int musicState;
    public int soundState;

    public float setsfxvolume;
    public float setMusicvolume;
     [Header("Audio Source Pooling")]
    public AudioSource audioSkeletonPrefab;
    public Transform audioPoolParent;
    public int audioSourcePoolCount;
    private List<AudioSource> inactiveAudioPool;
    private List<AudioSource> activeAudioPool;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update_volume()
    {
        if (PlayerPrefs.HasKey(EventVariables.Musicvol))
        {
            setMusicvolume = PlayerPrefs.GetFloat(EventVariables.Musicvol);
            Debug.Log("Musicvol" + PlayerPrefs.GetFloat(EventVariables.Musicvol));
        }
        else
        {
            setMusicvolume = 1;
            PlayerPrefs.SetFloat(EventVariables.Musicvol, setMusicvolume);
        }

        if (PlayerPrefs.HasKey(EventVariables.SFXvol))
        {
            setsfxvolume = PlayerPrefs.GetFloat(EventVariables.SFXvol);
            Debug.Log("SFXvol" + PlayerPrefs.GetFloat(EventVariables.SFXvol));
        }
        else
        {
            setsfxvolume = 1;
            PlayerPrefs.SetFloat(EventVariables.SFXvol, setsfxvolume);
        }
        Debug.Log("setMusicvolume"+ PlayerPrefs.HasKey(EventVariables.Musicvol));
        Debug.Log("setsfxvolume" + PlayerPrefs.HasKey(EventVariables.SFXvol));
        updateBGmusic();
    }

    public void updateBGmusic()
    {

        if (musicState == (int)Game.ToggleState.On)
        {
            bgMusic.volume = setMusicvolume;
        }
    }

    void Start()
    {
        InitAudioSourcePool();
    }

    public AudioSource bgMusic;

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
         Setting_Screen.OnMusicStateChange += SettingsScreen_OnMusicStateChange;
        Setting_Screen.OnSoundStateChange += SettingsScreen_OnSoundStateChange;

        Debug.Log("musicState" + musicState);
        Debug.Log("musicState" + musicState);
        musicState = PlayerPrefs.GetInt(EventVariables.musicOn);
        soundState = PlayerPrefs.GetInt(EventVariables.soundOn);
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        Setting_Screen.OnMusicStateChange -= SettingsScreen_OnMusicStateChange;
        Setting_Screen.OnSoundStateChange -= SettingsScreen_OnSoundStateChange;

    }

    private void SettingsScreen_OnMusicStateChange()
    {
        UpdateState();
        Debug.Log("SettingsScreen_OnMusicStateChange"+musicState);
        if (musicState == (int)Game.ToggleState.Off)
        {
            StopMusic(bgMusic);
        }
        else
        {
            PlayMusic(bgMusic);
        }
    }

    private void SettingsScreen_OnSoundStateChange()
    {
        UpdateState();
    }


    private void SceneManager_sceneLoaded(Scene mScene, LoadSceneMode arg1)
    {
        if (mScene.buildIndex == (int)Game.Scenes.Mainmenu)
        {
            //StopMusic(battleMusic);
             PlayMusic(bgMusic);
        }
        else if (mScene.buildIndex == (int)Game.Scenes.GamePlay)
        {
            StopMusic(bgMusic);
            // StopMusic(eventBGMusic);
            // PlayMusic(battleMusic);
        }
        //else if (mScene.name.Equals("storyplayer"))
        //{
        //    StopMusic(battleMusic);
        //}
    }
   
    public void UpdateState()
    {
        musicState = PlayerPrefs.GetInt(EventVariables.musicOn);
        soundState = PlayerPrefs.GetInt(EventVariables.soundOn);
    }

    public void StopMusic(AudioSource _source)
    {
       // _source.Stop();
        _source.Pause();
    }

    public void PlayMusic(AudioSource _source)
    {
        Debug.Log("PlayMusic" + musicState);
        if (musicState == (int)Game.ToggleState.On)
        {

            _source.volume = setMusicvolume;
            _source.loop = true;
            _source.Play();
        }

    }

     public void PlayBtnSound()
    {

        if (soundState == (int)Game.ToggleState.On)
        {
            if (audioBtnSource)
            {
                audioBtnSource.volume = setsfxvolume;
                audioBtnSource.Play();
            }
        }
    }




    public void PlaySound(AudioClip _clip, float volume = 1f)
    {
        if (_clip == null)
            return;

        if (soundState == (int)Game.ToggleState.On)
        {
            volume = setsfxvolume;
            AudioSource _source = GetAudioSourceFromPool();
            _source.clip = _clip;
            _source.volume = volume;
            // _source.GetComponent<AutoDestroy>().destroyTime = _clip.length;
            _source.gameObject.SetActive(true);
            this.Execute(() => DisableAudioSourceFromPool(_source), _clip.length);
            _source.Play();
        }
    }

     public void PlayTitorialSound(AudioClip _clip, float volume = 1f)
    {
        if (_clip == null)
            return;

        if (soundState == (int)Game.ToggleState.On)
        {
            volume = setsfxvolume;
            //AudioSource _source = GetAudioSourceFromPool();
            Tutorial_AudioSource.clip = _clip;
            Tutorial_AudioSource.volume = volume;
            // _source.GetComponent<AutoDestroy>().destroyTime = _clip.length;
            Tutorial_AudioSource.gameObject.SetActive(true);
            //this.Execute(() => DisableAudioSourceFromPool(_source), _clip.length);
            Tutorial_AudioSource.Play();
        }
    }
     private void InitAudioSourcePool()
    {
        inactiveAudioPool = new List<AudioSource>();
        activeAudioPool = new List<AudioSource>();
        for (int index = 0; index < audioSourcePoolCount; index++)
        {
            AudioSource source = Instantiate(audioSkeletonPrefab, audioPoolParent);
            source.gameObject.SetActive(false);
            inactiveAudioPool.Add(source);
        }
    }
    private AudioSource GetAudioSourceFromPool()
    {
        AudioSource source;
        Debug.Log("GetAudioSourceFromPool: Inactive Count = " + inactiveAudioPool.Count + ", Active Count = " + activeAudioPool.Count);
        if (inactiveAudioPool.Count > 0)
        {
            source = inactiveAudioPool[0];
            inactiveAudioPool.Remove(source);
            source.gameObject.SetActive(true);
        }
        else
        {
            source = Instantiate(audioSkeletonPrefab, audioPoolParent);
        }
        activeAudioPool.Add(source);

        return source;
    }
    private void DisableAudioSourceFromPool(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(false);
        inactiveAudioPool.Add(audioSource);
        activeAudioPool.Remove(audioSource);
    }
}
