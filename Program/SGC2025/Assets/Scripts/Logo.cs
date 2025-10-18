using UnityEngine;

public class Logo : BaseScene
{
    public enum Phase
    {
        In,
        Wait,
        Out,
    }

    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected override void OnAwake()
    {
        Debug.Log("Logo");
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
        if (GetPhaseTime() >= 120)
        {
            SetPhase((int)Phase.Out);
        }
    }

    private void Out()
    {
        if (GetPhaseTime() == 0)
        {
            Work.fade.Play(Fade.FadeType.Out, Fade.ColorType.Black);
        }
        else if( !Work.fade.IsPlaying())
        {
            Exit();
        }
    }
}
