using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class ResultMenu : MonoBehaviour
{
    // ハイスコア用テキスト
    [SerializeField]
    public List<TMP_Text> highScoreTexts = null;

    [SerializeField]
    public List<GameObject> highScoreMarks = null;

    // スタンプオブジェクト
    [SerializeField]
    public GameObject stampObject = null;

    // スコア用テキスト
    [SerializeField]
    public TMP_Text scoreText = null;

    [Header("ハイライト設定")]
    [SerializeField, Tooltip("ハイスコア更新時に使用する色")]
    private Color newHighScoreColor = Color.yellow;


    private Tween m_scoreTween = null;
    private Tween m_stampScaleTween = null;
    private Tween m_stampRotationTween = null;

    public int m_score = 0;

    public void Awake()
    {
        m_scoreTween = gameObject.AddComponent<Tween>();
        m_stampScaleTween = gameObject.AddComponent<Tween>();
        m_stampRotationTween = gameObject.AddComponent<Tween>();

        for (int i = 0; i < highScoreMarks.Count; i++)
        {
            SetHighScoreMarkVisible(i, false);
        }

        // スタンプを初期状態では非表示にしておく
        if (stampObject != null)
        {
            stampObject.SetActive(false);
            stampObject.transform.localScale = Vector3.zero;
        }
    }

    public void Update()
    {
        if (m_scoreTween.IsPlaying())
        {
            int score = Mathf.RoundToInt(m_scoreTween.GetProgress().x);
            scoreText.text = score.ToString();
        }
        else
        {
            scoreText.text = m_score.ToString();
        }

        // スタンプアニメーションの更新
        if (m_stampScaleTween.IsPlaying() && stampObject != null)
        {
            stampObject.transform.localScale = m_stampScaleTween.GetProgress();
            stampObject.transform.localRotation = Quaternion.Euler(m_stampRotationTween.GetProgress());
        }
    }

    public bool IsScoreMoving()
    {
        return m_scoreTween.IsPlaying();
    }

    public void SetScore(int _score)
    {
        m_scoreTween.Play(Vector3.zero, Vector3.one * _score, 1, Tween.Mode.Linear);
        m_score = _score;
    }

    public void SetHighScore(int _index, int _score)
    {
        if( _index < highScoreMarks.Count )
        {
            highScoreTexts[_index].text = _score.ToString();
        }
    }

    public void SetHighScoreMarkVisible(int _index, bool _isVisible)
    {
        if (_index < highScoreMarks.Count)
        {
            highScoreMarks[_index].SetActive(_isVisible);
        }
    }

    /// <summary>
    /// 指定されたインデックスのハイスコアテキストをハイライト表示します。
    /// </summary>
    /// <param name="_index">ハイライトするハイスコアのインデックス</param>
    public void HighlightHighScore(int _index)
    {
        if (_index < highScoreTexts.Count)
        {
            highScoreTexts[_index].color = newHighScoreColor;
        }
    }

    /// <summary>
    /// スタンプを押すアニメーションを再生します。
    /// </summary>
    /// <param name="animationDuration">アニメーションの時間（秒）</param>
    public void PlayStampAnimation()
    {
        if (stampObject == null) return;
        if (m_stampScaleTween.IsPlaying()) return; // 既に再生中なら何もしない

        StartCoroutine(StampAnimationCoroutine());
    }

    private IEnumerator StampAnimationCoroutine()
    {
        stampObject.SetActive(true);

        // --- アニメーションのパラメータ ---
        float overshootScale = 1.2f; // 最終サイズよりどれだけ大きくするか
        float firstDuration = 0.15f; // オーバーシュートまでの時間
        float secondDuration = 0.2f; // 最終サイズに落ち着くまでの時間
        float initialRotation = 15f; // 最初の回転角度

        // 1. ゼロから少し大きいサイズまで素早く拡大（オーバーシュート）
        m_stampScaleTween.Play(Vector3.zero, Vector3.one * overshootScale, firstDuration, Tween.Mode.Sub);
        m_stampRotationTween.Play(new Vector3(0, 0, -initialRotation), Vector3.zero, firstDuration + secondDuration, Tween.Mode.Sub); // 回転は通しで
        yield return new WaitForSeconds(firstDuration);

        // 2. 少し大きいサイズから最終的なサイズに落ち着く（バウンド）
        m_stampScaleTween.Play(Vector3.one * overshootScale, Vector3.one, secondDuration, Tween.Mode.Sub);
    }
}
