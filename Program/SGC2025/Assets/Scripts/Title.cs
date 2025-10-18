using UnityEngine;

public class Title : BaseScene
{
    public enum Phase
    {
        In,
        Wait,
        Out,
    }

    [SerializeField, Tooltip("タイトルメニューの参照")]
    private TitleMenu titleMenu = null;

    // 決定ボタンが押されたかどうかを管理するフラグ
    private bool _isConfirmationStarted = false;


    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected override void OnAwake()
    {
        Debug.Log("Title");
    }

    /**
     * Updateの直前に呼ばれる
     */
    protected override void OnStart()
    {
        sound.PlayBGM(SoundDefine.BGM.TITLE);
    }

    /**
     * 毎フレーム呼ばれる
     */
    protected override void OnUpdate()
    {
        switch ((Phase)GetPhase())
        {
            case Phase.In: In(); break;
            case Phase.Wait: Wait(); break;
            case Phase.Out: Out(); break;
        }
    }

    /**
     * 破棄時に呼ばれる
     */
    protected override void OnDestroy()
    {

    }

    private void In()
    {
        if (GetPhaseTime() == 0)
        {
            Work.fade.Play(Fade.FadeType.In, Fade.ColorType.Black);
        }
        else if( !Work.fade.IsPlaying())
        {
            SetPhase((int)Phase.Wait);
        }
    }

    private void Wait()
    {
        // まだ決定ボタンが押されておらず、何かしらの入力があった場合
        if (!_isConfirmationStarted && Input.anyKeyDown)
        {
            if (titleMenu != null)
            {
                sound.PlaySE(SoundDefine.SE.CONFIRM_16);
                // TitleMenuの決定演出を開始
                titleMenu.OnConfirm();
            }
            _isConfirmationStarted = true;
        }

        // 決定演出が開始された後、その完了を待つ
        if (_isConfirmationStarted && titleMenu != null && titleMenu.IsConfirmFlashFinished())
        {
            SetPhase((int)Phase.Out); // 演出が完了したら次のフェーズへ
        }
    }

    private void Out()
    {
        if (GetPhaseTime() == 0)
        {
            sound.StopBGM();
            Work.fade.Play(Fade.FadeType.Out, Fade.ColorType.Black);
        }
        else if( !Work.fade.IsPlaying())
        {
            Exit();
        }
    }
}
