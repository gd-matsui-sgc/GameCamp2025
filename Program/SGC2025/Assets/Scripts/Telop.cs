using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームスタート/エンドなどのテロップを管理するクラス
/// </summary>
public class Telop : Base
{
    public enum Phase
    {
        Idle, // 待機中（非表示）
        Run,  // 実行中（表示）
        Exit, // 終了処理中（フェードアウト）
    }

    [SerializeField, Tooltip("表示するテキストコンポーネント")]
    private TMP_Text telopText = null;

    [SerializeField, Tooltip("背景画像")]
    private Image backgroundImage = null;

    [SerializeField, Tooltip("テロップの表示時間（秒）")]
    private float displayDuration = 1.5f;

    [SerializeField, Tooltip("フェードイン/アウトの時間（秒）")]
    private float fadeTime = 0.25f;

    private Tween m_alphaTween = null;
    private Tween m_scaleTween = null;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_alphaTween = gameObject.AddComponent<Tween>();
        m_scaleTween = gameObject.AddComponent<Tween>();

        // 最初は非表示にしておく
        if (telopText != null)
        {
            SetTextAlpha(0);
            telopText.gameObject.SetActive(false);
        }
        if (backgroundImage != null)
        {
            SetBackgroundScale(0);
            backgroundImage.gameObject.SetActive(false);
        }
        SetPhase((int)Phase.Idle);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        // Tweenによるアルファ値の更新
        if (m_alphaTween.IsPlaying())
        {
            SetTextAlpha(m_alphaTween.GetProgress().x);
        }
        if (m_scaleTween.IsPlaying())
        {
            SetBackgroundScale(m_scaleTween.GetProgress().y);
        }

        switch ((Phase)GetPhase())
        {
            case Phase.Idle:
                // 何もしない
                break;
            case Phase.Run: Run();  break;
            case Phase.Exit:
                // フェードアウト/スケールアウトが完了したら自身を終了させる
                if (!m_alphaTween.IsPlaying() && !m_scaleTween.IsPlaying())
                {
                    Exit(); // BaseクラスのExit()を呼び出す
                }
                break;
        }
    }

    /// <summary>
    /// テロップの再生を開始します。
    /// </summary>
    /// <param name="message">表示する文字列</param>
    public void Play(string message)
    {
        if (telopText == null || GetPhase() == (int)Phase.Run) return;

        telopText.text = message;

        // フェードイン
        telopText.gameObject.SetActive(true);
        m_alphaTween.Play(Vector3.zero, Vector3.one, fadeTime, Tween.Mode.Linear);

        // スケールイン
        if (backgroundImage != null)
        {
            backgroundImage.gameObject.SetActive(true);
            m_scaleTween.Play(new Vector3(1, 0, 1), Vector3.one, fadeTime, Tween.Mode.Sub); // 減速
        }

        SetPhase((int)Phase.Run);
    }

    /// <summary>
    /// テロップが再生中かどうかを返します。
    /// </summary>
    /// <returns>再生中ならtrue</returns>
    public bool IsPlaying()
    {
        // 終了していなければ再生中とみなす
        return !IsExited();
    }

    // テキストのアルファ値を設定するヘルパー関数
    private void SetTextAlpha(float alpha)
    {
        if (telopText == null) return;
        Color color = telopText.color;
        color.a = alpha;
        telopText.color = color;
    }

    // 背景のスケールを設定するヘルパー関数
    private void SetBackgroundScale(float scaleY)
    {
        if (backgroundImage == null) return;
        Vector3 scale = backgroundImage.transform.localScale;
        backgroundImage.transform.localScale = new Vector3(scale.x, scaleY, scale.z);
    }

    private void Run()
    {
        if (GetPhaseTime() == 0)
        {
            if (m_alphaTween.IsPlaying() ||
                m_scaleTween.IsPlaying())
            {
                SetPhaseTime(0);
            }
        }
        else
        {
            // 表示時間が経過したらExitフェーズへ
            if (GetPhaseTime() * Time.fixedDeltaTime >= displayDuration)
            {
                m_alphaTween.Play(Vector3.one, Vector3.zero, fadeTime, Tween.Mode.Linear);
                m_scaleTween.Play(Vector3.one, new Vector3(1, 0, 1), fadeTime, Tween.Mode.Add); // 加速
                SetPhase((int)Phase.Exit);
            }
        }
    }
}
