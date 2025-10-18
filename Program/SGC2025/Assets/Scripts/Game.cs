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

        _obstacleManager.MovementCall();
        _timeCounter.Timer();
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
        // テロップなどを出してから遷移
        SetPhase((int)Phase.GameMain);
    }

    /**
     * ゲームメインのフェーズ
     */
    protected void _GameMain()
    {
        // ゲーム終了条件を満たしたらゲーム終了へ
        //SetPhase((int)Phase.GameEnd);
    }

    /**
     * ゲーム終了のフェーズ
     */
    protected void _GameEnd()
    {
        // ゲーム中のスコアを設定
        Work.SetScore(100);
        // テロップなどを出してから遷移
        SetPhase((int)Phase.FadeOut);
    }

    /**
     * フェードアウトのフェーズ
     */
    protected void _FadeOut()
    {
        if (GetPhaseTime() == 0)
        {
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
