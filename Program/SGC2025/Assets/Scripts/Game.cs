using UnityEngine;

public class Game : BaseScene
{
    // フェイズ
    public enum Phase
    {
        Startup,    // 事前準備
        FadeIn,     // フェードイン
        Help,       // ヘルプ表示
        GameStart,  // ゲームスタート
        GameMain,   // ゲームメイン
        GameEnd,    // ゲーム終了
        FadeOut,    // フェードアウト
    }


    [SerializeField, Header("障害物のマネージャー")]
    private ObstacleManager _obstacleManager = default;
    [SerializeField, Header("タイマー")]
    private TimeCounter _timeCounter = default;

    // テロップ
    private Telop m_telop = null;

    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected override void OnAwake()
    {
        _obstacleManager.CreatePool();
    }

    /**
     * Updateの直前に呼ばれる
     */
    protected override void OnStart()
    {
    }

    /**
     * 毎フレーム呼ばれる
     */
    protected override void OnUpdate()
    {
        switch ((Phase)GetPhase())
        {
            case Phase.Startup:     _Startup();     break;
            case Phase.FadeIn:      _FadeIn();      break;
            case Phase.Help:        _Help();        break;
            case Phase.GameStart:   _GameStart();   break;
            case Phase.GameMain:    _GameMain();    break;
            case Phase.GameEnd:     _GameEnd();     break;
            case Phase.FadeOut:     _FadeOut();     break;
        }

    }

    /**
     * 破棄時に呼ばれる
     */
    protected override void OnDestroy()
    {

    }

    /**
     * 初期設定などのフェーズ
     */
    protected void _Startup()
    {
        // 暗転中にやっておきたいことを書きます
        SetPhase((int)Phase.FadeIn);
    }

    /**
     * フェードインフェーズ
     */
    protected void _FadeIn()
    {
        if (GetPhaseTime() == 0)
        {
            if( Work.fade != null )
            {
                Work.fade.Play(Fade.FadeType.In, Fade.ColorType.Black);
            }
        }
        else
        {
            if (Work.fade != null &&
                Work.fade.IsPlaying())
            {
                return;
            }
            sound.PlayBGM(SoundDefine.BGM.GAME);
            SetPhase((int)Phase.Help);
        }
    }

    /**
     * ヘルプ表示のフェーズ
     */
    protected void _Help()
    {
        SetPhase((int)Phase.GameStart);
    }

    /**
     * ゲームスタートのフェーズ
     */
    protected void _GameStart()
    {
        // 最初のフレームでテロップを再生
        if (GetPhaseTime() == 0)
        {
            // Telopプレハブを読み込んで生成し、コンポーネントを取得
            GameObject telopPrefab = Resources.Load<GameObject>("Prefabs/UIs/Telop");
            if (telopPrefab != null)
            {
                GameObject instance = Instantiate(telopPrefab);
                m_telop = instance.GetComponent<Telop>();
            }
            m_telop?.Play("Game Start");
        }
        else if(m_telop == null || m_telop.IsExited())
        {
            m_telop = null;
            SetPhase((int)Phase.GameMain);
        }
    }

    /**
     * ゲームメインのフェーズ
     */
    protected void _GameMain()
    {
        // ゲーム終了条件を満たしたらゲーム終了へ
        //SetPhase((int)Phase.GameEnd);
        _obstacleManager.MovementCall();
        _timeCounter.Timer();

    }

    /**
     * ゲーム終了のフェーズ
     */
    protected void _GameEnd()
    {
        // 最初のフレームでテロップを再生
        if (GetPhaseTime() == 0)
        {
            // ゲーム中のスコアを設定
            Work.SetScore(100);


            // Telopプレハブを読み込んで生成し、コンポーネントを取得
            GameObject telopPrefab = Resources.Load<GameObject>("Prefabs/UIs/Telop");
            if (telopPrefab != null)
            {
                GameObject instance = Instantiate(telopPrefab);
                m_telop = instance.GetComponent<Telop>();
            }
            m_telop?.Play("Game End");
        }
        // テロップの再生が終わったら次のフェーズへ
        else if(m_telop == null || m_telop.IsExited())
        {
            m_telop = null;
            SetPhase((int)Phase.FadeOut);
        }
    }

    /**
     * フェードアウトのフェーズ
     */
    protected void _FadeOut()
    {
        if (GetPhaseTime() == 0)
        {
            sound.StopBGM();
            if( Work.fade != null )
            {
                Work.fade.Play(Fade.FadeType.Out, Fade.ColorType.Black);
            }
        }
        else
        {
            if (Work.fade != null &&
                Work.fade.IsPlaying())
            {
                return;
            }
            Exit();
        }
    }
}
