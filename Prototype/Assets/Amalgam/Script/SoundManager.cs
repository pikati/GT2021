using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    //ボリューム一覧
    [SerializeField, Range(0, 1), Tooltip("マスター音量")]
    private float MasterVolume = 1;
    [SerializeField, Range(0, 1), Tooltip("BGM音量")]
    private float BgmVolume = 1;
    [SerializeField, Range(0, 1), Tooltip("SE音量")]
    private float SeVolume = 1;

    [SerializeField]
    private AudioClip[] BgmClips;
    [SerializeField]
    private AudioClip[] SeClips;

    private Dictionary<string, int> BgmIndex = new Dictionary<string, int>();
    private Dictionary<string, int> SeIndex = new Dictionary<string, int>();

    private AudioSource BgmAudioSource;
    private AudioSource SeAudioSource;

    public float MasterVolumeProperty
    {
        set
        {
            MasterVolume = Mathf.Clamp01(value);
            BgmAudioSource.volume = BgmVolume * MasterVolume;
            SeAudioSource.volume = SeVolume * MasterVolume;
        }
        get
        {
            return MasterVolume;
        }
    }

    public float BgmVolumeProperty
    {
        set
        {
            BgmVolume = Mathf.Clamp01(value);
            BgmAudioSource.volume = BgmVolume * MasterVolume;
        }
        get
        {
            return BgmVolume;
        }
    }

    public float SeVolumeProperty
    {
        set
        {
            SeVolume = Mathf.Clamp01(value);
            SeAudioSource.volume = SeVolume * MasterVolume;
        }
        get
        {
            return SeVolume;
        }
    }

    //起動時にSEとBGMをロード
    new public void Awake()
    {
        if(this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        BgmAudioSource = gameObject.AddComponent<AudioSource>();
        SeAudioSource = gameObject.AddComponent<AudioSource>();

        BgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        SeClips = Resources.LoadAll<AudioClip>("Sound/SE");

        for(int i = 0; i < BgmClips.Length; i++)
        {
            BgmIndex.Add(BgmClips[i].name, i);
        }

        for(int i = 0; i < SeClips.Length; i++)
        {
            SeIndex.Add(SeClips[i].name, i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //名前から番号を検索
    private int GetBgmIndex(string name)
    {
        if (BgmIndex.ContainsKey(name))
        {
            return BgmIndex[name];
        }
        else
        {
            Debug.LogError("指定された名前のBGMが見つかりません：" + name);
            return 0;
        }
    }

    private int GetSeIndex(string name)
    {
        if(SeIndex.ContainsKey(name))
        {
            return SeIndex[name];
        }
        else
        {
            Debug.LogError("指定された名前のSEが見つかりません：" + name);
            return 0;
        }
    }

    //BGM ================================================================
    //再生
    public void PlayBgm(int index)
    {
        if(BgmClips.Length == 0)
        {
            Debug.LogError("BGMが1個もないよ");
            return;
        }

        index = Mathf.Clamp(index, 0, BgmClips.Length);

        BgmAudioSource.clip = BgmClips[index];
        BgmAudioSource.loop = true;
        BgmAudioSource.volume = BgmVolume * MasterVolume;
        BgmAudioSource.Play();
    }

    //名前から再生
    public void PlayBgmByName(string name)
    {
        PlayBgm(GetBgmIndex(name));
    }

    //停止
    public void StopBgm()
    {
        BgmAudioSource.Stop();
        BgmAudioSource.clip = null;
    }

    //一時停止
    public void PauseBgm()
    {
        BgmAudioSource.Pause();
    }

    //再開
    public void UnPauseBgm()
    {
        BgmAudioSource.UnPause();
    }

    //SE ===============================================================
    public void PlaySe(int index)
    {
        if (SeClips.Length == 0)
        {
            Debug.LogError("SEが1個もないよ");
            return;
        }

        index = Mathf.Clamp(index, 0, SeClips.Length);

        SeAudioSource.PlayOneShot(SeClips[index], SeVolume * MasterVolume);
    }

    public void PlaySeByName(string name)
    {
        PlaySe(GetSeIndex(name));
    }

    public void StopSe()
    {
        SeAudioSource.Stop();
        SeAudioSource.clip = null;
    }

    //インスペクタからの変更 ===========================================
    private void OnValidate()
    {
        MasterVolume = Mathf.Clamp01(MasterVolume);
        BgmVolume = Mathf.Clamp01(BgmVolume);
        SeVolume = Mathf.Clamp01(SeVolume);

        if(BgmAudioSource != null)
        {
            BgmAudioSource.volume = BgmVolume * MasterVolume;
        }

        if (SeAudioSource != null)
        {
            SeAudioSource.volume = SeVolume * MasterVolume;
        }
    }
}
