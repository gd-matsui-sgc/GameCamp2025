using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Tweenクラス
 */
public class Tween : MonoBehaviour
{
    // モード
    public enum Mode
    {
        // 等速
        Linear,

        // 加速
        Add,

        // 減速
        Sub,
    }

    // 開始位置
    private Vector3 m_start = Vector3.zero;

    // 終了位置
    private Vector3 m_end   = Vector3.zero;

    // 現在
    private Vector3 m_progress = Vector3.zero;

    // 移動時間
    private float   m_moveTime = 0;

    // 経過時間
    private float   m_elapsedTime = 0;

    // 実行中か
    private bool    m_running = false;

    // モード
    private Mode    m_mode    = Mode.Linear;

    /**
     * 毎フレーム呼ばれます
     */
    void Update()
    {
        if( !m_running ) return;

        m_elapsedTime += Time.deltaTime;

        float v = m_elapsedTime / m_moveTime;
        if( v > 1.0f ){ v = 1.0f; }

        m_progress = Calc( m_start, m_end, v, m_mode);

        if( m_elapsedTime >= m_moveTime )
        {
            m_running = false;
        }
    }

    /**
     * Tweenを再生します
     * @param start         開始値
     * @param end           終了値
     * @param moveTime      移動時間
     * @param mode          モード
     */
    public void Play( Vector3 start, Vector3 end, float moveTime, Mode mode )
    {
        m_mode     = mode;
        m_start    = start;
        m_end      = end;
        m_moveTime = moveTime;
        if( moveTime == 0.0f )
        {
            m_progress    = end;
            m_elapsedTime = m_moveTime;
            m_running     = false;
        }
        else
        {
            m_progress    = start;
            m_elapsedTime = 0;
            m_running     = true;
        }
    }

    /**
     * Tweenを再生中か
     * @return 判定結果
     */
    public bool IsPlaying()
    {
        return m_running;
    }

    /**
     * 進捗値を取得
     * @return 進捗値
     */
    public Vector3 GetProgress()
    {
        return m_progress;
    }

    /**
     * Tween用計算
     * @param start         開始値
     * @param end           終了値
     * @param v             2つの補間値
     * @param mode          モード
     * @return 計算結果
     */
    private Vector3 Calc( Vector3 start, Vector3 end, float v, Mode mode = Mode.Linear )
    {
        return new Vector3( Calc( start.x, end.x, v, mode ),
                            Calc( start.y, end.y, v, mode ),
                            Calc( start.z, end.z, v, mode ) );
    }

    /**
     * Tween用計算
     * @param start         開始値
     * @param end           終了値
     * @param v             2つの補間値
     * @param mode          モード
     * @return 計算結果
     */
    private float Calc( float start, float end, float v, Mode mode = Mode.Linear )
    {
        switch(mode)
        {
        case Mode.Linear: return CalcLinear( start, end, v );
        case Mode.Add:    return CalcAdd( start, end, v );
        case Mode.Sub:    return CalcSub( start, end, v );
        }
        return 0;
    }

    /**
     * Tween用計算（等速用）
     * @param start         開始値
     * @param end           終了値
     * @param v             2つの補間値
     * @return 計算結果
     */
    private float CalcLinear(float start, float end, float v )
    {
        return Mathf.Lerp(start, end, v);
    }

    /**
     * Tween用計算（加速用）
     * @param start         開始値
     * @param end           終了値
     * @param v             2つの補間値
     * @return 計算結果
     */
    private float CalcAdd(float start, float end, float v )
    {
        end -= start;
        return end * v * v + start;
    }

    /**
     * Tween用計算（減速用）
     * @param start         開始値
     * @param end           終了値
     * @param v             2つの補間値
     * @return 計算結果
     */
    private float CalcSub(float start, float end, float v )
    {
        end -= start;
        return -end * v * ( v - 2.0f ) + start;
    }


}
