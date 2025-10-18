using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * フェードクラス
 */
public class Fade : MonoBehaviour
{
    // フェードの種類
    public enum FadeType
    {
        // なし
        None,

        // 塗りつぶし
        Blank,

        // フェードイン
        In,

        // フェードアウト
        Out,

        // ワイプイン
        WipeIn,

        // ワイプアウト
        WipeOut,
    }

    // カラーの種類
    public enum ColorType
    {
        // 黒
        Black,

        // 白
        White,
    }

    // フェードの速度
    public enum FadeSpeed
    {
        // 早い
        Fast = 0,
        // 通常
        Norma = 1,
        // 遅い
        Slow = 2,
    }

    // フェードの時間（s）
    public float[] FadeTimes =
    {
        0.5f,
        1.0f,
        1.5f,
    };

    // Tweenクラス
    private Tween m_tween = null;

    // カラータイプ
    private ColorType m_colorType = ColorType.Black;

    // フェードの種類
    private FadeType  m_fadeType = FadeType.None;

    // フェード用イメージ
    [SerializeField]
    private Image m_image = null;

    /**
     * 生成時に呼ばれます
     */
    public void Awake()
    {
        m_tween = gameObject.AddComponent<Tween>();
    }

    /**
     * 毎フレーム呼ばれます
     */
    public void Update()
    {
        Vector3 progress = m_tween.GetProgress();
        UpdateOpacity( progress.x );
   }

    /**
     * 再生します
     * @param fadeType   フェードの種類
     * @param colorType  カラーの種類
     * @param fadeSpeed  フェード速度
     */
    public void Play( FadeType fadeType, ColorType colorType = ColorType.Black, FadeSpeed fadeSpeed = FadeSpeed.Norma )
    {
        float fadeTime = FadeTimes[(int)fadeSpeed];
        switch( fadeType )
        {
        case FadeType.In:
            {
                m_tween.Play( Vector3.one, Vector3.zero, fadeTime, Tween.Mode.Linear );
            }
            break;

        case FadeType.Out:
            {
                m_tween.Play( Vector3.zero, Vector3.one, fadeTime, Tween.Mode.Linear );
            }
            break;

        case FadeType.Blank:
            {
                m_tween.Play( Vector3.one, Vector3.one, fadeTime, Tween.Mode.Linear );
            }
            break;

        case FadeType.None:
            {
                m_tween.Play( Vector3.zero, Vector3.zero, fadeTime, Tween.Mode.Linear );
            }
            break;

        // @Todo 余力あれば作る。(画像アニメのワイプ)
        case FadeType.WipeIn:
            {
                m_tween.Play( Vector3.one, Vector3.zero, fadeTime, Tween.Mode.Linear );
            }
            break;

        case FadeType.WipeOut:
            {
                m_tween.Play( Vector3.zero, Vector3.one, fadeTime, Tween.Mode.Linear );
            }
            break;

        }
        m_fadeType  = fadeType;
        m_colorType = colorType;
        Vector3 progress = m_tween.GetProgress();
        UpdateOpacity( progress.x );
    }

    /**
     * 透明度の更新
     * @param alpha  アルファ値
     */
    public void UpdateOpacity( float alpha )
    {
        if( m_image != null )
        {
            switch( m_colorType )
            {
            case ColorType.Black:   m_image.color = new Color( 0.0f, 0.0f, 0.0f, alpha ); break;
            case ColorType.White:   m_image.color = new Color( 1.0f, 1.0f, 1.0f, alpha ); break;
            }
            m_image.raycastTarget = ( alpha != 0.0f );
        }
    }

    /**
     * 再生中かを判定
     * @return 判定結果
     */
    public bool IsPlaying()
    {
        return m_tween.IsPlaying();
    }

    /**
     * フェードの種類を取得
     * @return フェードの種類
     */
    public FadeType GetFadeType()
    {
        return m_fadeType;
    }
}
