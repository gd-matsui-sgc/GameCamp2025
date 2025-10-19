using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// BGMとSEの再生を管理するシングルトンクラス
/// </summary>
public class SoundManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static SoundManager Instance { get; private set; }

    // BGMとSEのAudioSource
    private AudioSource[] bgmSources = new AudioSource[2];
    private int activeBgmSourceIndex = 0;
    private readonly List<AudioSource> seSources = new List<AudioSource>();

    // フェード処理中のコルーチン
    private Coroutine bgmFadeCoroutine;

    [Header("Default Settings")]
    [SerializeField, Tooltip("SE再生用のAudioSourceのプール数")]
    private int seSourcePoolCount = 10;
    [SerializeField, Tooltip("BGMのフェード時間")]
    private float bgmFadeDuration = 1.0f;

    [Header("Volume Control")]
    [Range(0f, 1f)]
    public float masterVolume = 1.0f;
    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;
    [Range(0f, 1f)]
    public float seVolume = 0.8f;

    // AudioClipをキャッシュしてパフォーマンスを向上
    private readonly Dictionary<SoundDefine.BGM, AudioClip> bgmCache = new Dictionary<SoundDefine.BGM, AudioClip>();
    private readonly Dictionary<SoundDefine.SE, AudioClip> seCache = new Dictionary<SoundDefine.SE, AudioClip>();

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいで存在させる
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // BGM用のAudioSourceを2つ作成（クロスフェード用）
        for (int i = 0; i < bgmSources.Length; i++)
        {
            bgmSources[i] = gameObject.AddComponent<AudioSource>();
            bgmSources[i].loop = true;
            bgmSources[i].playOnAwake = false;
        }

        // SE用のAudioSourceをプールとして複数作成
        for (int i = 0; i < seSourcePoolCount; i++)
        {
            var seSource = gameObject.AddComponent<AudioSource>();
            seSource.playOnAwake = false;
            seSources.Add(seSource);
        }
    }

    /// <summary>
    /// BGMを再生します。
    /// </summary>
    /// <param name="bgmType">再生するBGMの種類</param>
    public void PlayBGM(SoundDefine.BGM bgmType)
    {
        // BGMを停止する場合
        if (bgmType == SoundDefine.BGM.None)
        {
            StopBGM();
            return;
        }

        // 再生したいBGMのクリップを取得
        if (!SoundDefine.BgmPath.TryGetValue(bgmType, out string bgmPath))
        {
            Debug.LogWarning($"BGM path for {bgmType} not found in SoundDefine.");
            return;
        }

        // キャッシュを確認
        if (!bgmCache.TryGetValue(bgmType, out AudioClip clip))
        {
            // キャッシュになければResourcesから読み込む
            clip = Resources.Load<AudioClip>("Sounds/BGM/" + bgmPath);
            if (clip == null)
            {
                Debug.LogWarning($"BGM not found: Sounds/BGM/{bgmPath}");
                return;
            }
            bgmCache[bgmType] = clip;
        }

        // 現在再生中のBGMと同じなら何もしない
        if (bgmSources[activeBgmSourceIndex].clip == clip && bgmSources[activeBgmSourceIndex].isPlaying)
        {
            return;
        }

        // フェード処理
        if (bgmFadeCoroutine != null)
        {
            StopCoroutine(bgmFadeCoroutine);
        }
        bgmFadeCoroutine = StartCoroutine(CrossFadeBGM(clip));
    }

    /// <summary>
    /// BGMを停止します。
    /// </summary>
    public void StopBGM()
    {
        if (bgmFadeCoroutine != null)
        {
            StopCoroutine(bgmFadeCoroutine);
        }
        bgmFadeCoroutine = StartCoroutine(FadeOutBGM());
    }

    private IEnumerator FadeOutBGM()
    {
        AudioSource sourceToFadeOut = bgmSources[activeBgmSourceIndex];
        float startVolume = sourceToFadeOut.volume;

        float timer = 0f;
        while (timer < bgmFadeDuration)
        {
            timer += Time.deltaTime;
            sourceToFadeOut.volume = Mathf.Lerp(startVolume, 0f, timer / bgmFadeDuration);
            yield return null;
        }

        sourceToFadeOut.Stop();
        sourceToFadeOut.clip = null;
    }

    private IEnumerator CrossFadeBGM(AudioClip newClip)
    {
        // 新しいBGMを再生するソースを準備
        int newBgmSourceIndex = 1 - activeBgmSourceIndex;
        bgmSources[newBgmSourceIndex].clip = newClip;
        bgmSources[newBgmSourceIndex].Play();

        // 現在のBGMソースと新しいBGMソース
        AudioSource sourceToFadeOut = bgmSources[activeBgmSourceIndex];
        AudioSource sourceToFadeIn = bgmSources[newBgmSourceIndex];

        float targetVolume = masterVolume * bgmVolume;
        float timer = 0f;

        while (timer < bgmFadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / bgmFadeDuration;
            sourceToFadeOut.volume = Mathf.Lerp(targetVolume, 0f, progress);
            sourceToFadeIn.volume = Mathf.Lerp(0f, targetVolume, progress);
            yield return null;
        }

        sourceToFadeOut.Stop();
        sourceToFadeOut.clip = null;
        activeBgmSourceIndex = newBgmSourceIndex;
    }

    /// <summary>
    /// SEを再生します。
    /// </summary>
    /// <param name="seType">再生するSEの種類</param>
    public void PlaySE(SoundDefine.SE seType)
    {
        if (seType == SoundDefine.SE.None) return;

        if (!SoundDefine.SePath.TryGetValue(seType, out string sePath))
        {
            Debug.LogWarning($"SE path for {seType} not found in SoundDefine.");
            return;
        }

        // キャッシュを確認
        if (!seCache.TryGetValue(seType, out AudioClip clip))
        {
            // キャッシュになければResourcesから読み込む
            clip = Resources.Load<AudioClip>("Sounds/SE/" + sePath);
            if (clip == null)
            {
                Debug.LogWarning($"SE not found: Sounds/SE/{sePath}");
                return;
            }
            seCache[seType] = clip;
        }

        // 再生中でないAudioSourceを探して再生
        foreach (var source in seSources)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = masterVolume * seVolume;
                source.Play();
                return;
            }
        }

        // すべて使用中の場合は警告（必要ならプールを増やす）
        Debug.LogWarning("All SE sources are busy. Consider increasing the pool count.");
    }

    /// <summary>
    /// 指定されたSEのAudioClipを取得します。キャッシュにない場合は読み込みます。
    /// </summary>
    /// <param name="seType">取得するSEの種類</param>
    /// <returns>AudioClip。見つからない場合はnull。</returns>
    public AudioClip GetSEClip(SoundDefine.SE seType)
    {
        if (seType == SoundDefine.SE.None) return null;

        // まずキャッシュを確認
        if (seCache.TryGetValue(seType, out AudioClip clip))
        {
            return clip;
        }

        // キャッシュになければパスを取得して読み込む
        if (!SoundDefine.SePath.TryGetValue(seType, out string sePath))
        {
            Debug.LogWarning($"SE path for {seType} not found in SoundDefine.");
            return null;
        }

        clip = Resources.Load<AudioClip>("Sounds/SE/" + sePath);
        if (clip == null)
        {
            Debug.LogWarning($"SE not found: Sounds/SE/{sePath}");
            return null;
        }

        // 次回のためにキャッシュに保存
        seCache[seType] = clip;
        return clip;
    }

    /// <summary>
    /// BGMの音量を一時的に下げます（ダッキング）。
    /// </summary>
    /// <param name="duckRatio">音量を下げる割合 (0.0 - 1.0)。0.3なら30%の音量になる。</param>
    /// <param name="duration">音量を下げておく時間（秒）。</param>
    public void DuckBGM(float duckRatio, float duration)
    {
        StartCoroutine(DuckBGMRoutine(duckRatio, duration));
    }

    private IEnumerator DuckBGMRoutine(float duckRatio, float duration)
    {
        float originalVolume = masterVolume * bgmVolume;
        float duckedVolume = originalVolume * duckRatio;

        // 現在再生中のBGMソースの音量を下げる
        bgmSources[activeBgmSourceIndex].volume = duckedVolume;

        // 指定された時間待機
        yield return new WaitForSeconds(duration);

        // 音量を元に戻す
        bgmSources[activeBgmSourceIndex].volume = originalVolume;
    }
}
