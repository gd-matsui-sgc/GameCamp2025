using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/**
 * シーン用ベースクラス
 */
public class Base : MonoBehaviour
{
    // 終了したか
    private bool m_exited = false;
    // 終了コード
    private int m_exitCode = -1;
    // フェイズ
    private int m_phase = 0;
    // 最後のフェイズ
    private int m_lasePhase = 0;
    // フェイズの時間
    private int m_phaseTime = 0;
    // フェイズの時間が更新可能か
    private bool m_phaseTimeUpdatable = false;


    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected virtual void Awake()
    {
        // SoundManagerのインスタンスを生成
        // SoundManagerがシーン内に既に存在しない場合のみ生成する
        if (SoundManager.Instance == null)
        {
            GameObject soundManagerObj = new GameObject("SoundManager");
            soundManagerObj.AddComponent<SoundManager>();
        }

        if (FloatingTextManager.Instance == null)
        {
            GameObject soundManagerObj = new GameObject("FloatingTextManager");
            soundManagerObj.AddComponent<FloatingTextManager>();
        }
        OnAwake();
    }

    /**
     * Updateの直前に呼ばれる(Unity側)
     */
    protected virtual void Start()
    {
        OnStart();
    }

    /**
     * 毎フレーム呼ばれる(Unity)
     */
    private void Update()
    {
        m_phaseTimeUpdatable = true;
        OnUpdate();
        if (m_phaseTimeUpdatable) { m_phaseTime++; }
    }

    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected virtual void OnAwake()
    {

    }

    /**
     * Updateの直前に呼ばれる
     */
    protected virtual void OnStart()
    {

    }

    /**
     * 毎フレーム呼ばれる
     */
    protected virtual void OnUpdate()
    {
        if(m_exited)
        {
            Destroy(this.gameObject);
        }
    }

    /**
     * 破棄時に呼ばれる
     */
    protected virtual void OnDestroy()
    {

    }

    /**
     * 終了させる
     */
    public void Exit()
    {
        m_exited = true;
    }

    /**
     * 終了したか
     */
    public bool IsExited()
    {
        return m_exited;
    }

    /**
     * 終了コードを設定
     * @param exitCode    終了コード
     */
    public void SetExitCode(int exitCode)
    {
        m_exitCode = exitCode;
    }

    /**
     * 終了コードを取得
     * @returns 終了コード
     */
    public int GetExitCode()
    {
        return m_exitCode;
    }

    /**
     * フェイズを設定
     * @param phase    フェイズ
     * @param forced   強制的に変更するか
     */
    public void SetPhase(int phase, bool forced = false)
    {
        if (m_phase != phase || forced)
        {
            m_lasePhase = m_phase;
            m_phase = phase;
            SetPhaseTime(0);
        }
    }

    /**
     * フェイズを取得
     * @returns フェイズ
     */
    public int GetPhase()
    {
        return m_phase;
    }

    /**
     * フェイズ時間を取得
     * @returns フェイズ時間
     */
    public int GetPhaseTime()
    {
        return m_phaseTime;
    }

    /**
     * フェイズ時間を設定
     * @param phaseTime    フェイズ時間
     */
    public void SetPhaseTime(int phaseTime)
    {
        m_phaseTime = phaseTime;
        m_phaseTimeUpdatable = false;
    }

    /**
     * サウンド
     */
    public SoundManager sound
    {
        get{ return SoundManager.Instance; }
    }
}
