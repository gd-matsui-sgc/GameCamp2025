using UnityEngine;

public class Result : BaseScene
{
    public static int EXIT_CODE_TITLE = 1;

    public enum Phase
    {
        In,
        Wait,
        Out,
    }

    public ResultMenu resultMenu = null;
    private bool m_highScoreUpdated = false;

    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected override void OnAwake()
    {
        Debug.Log("Result");
        sound.PlayBGM(SoundDefine.BGM.RESULT);
    }

    /**
     * Updateの直前に呼ばれる
     */
    protected override void OnStart()
    {
        if (resultMenu != null)
        {
            for (int i = 0; i < Work.HIGH_SCORE_COUNT; i++)
            {
                int score = Work.GetHighScore(i);
                resultMenu.SetHighScore(i, score);
            }
        }
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
        // スコア演出開始 (2秒後)
        if (GetPhaseTime() == 60 * 2)
        {
            if (resultMenu != null)
            {
                resultMenu.SetScore(Work.GetScore());
            }
            return; // このフレームでは他の処理をしない
        }

        // スコア演出中は待機
        if (resultMenu != null && resultMenu.IsScoreMoving())
        {
            return;
        }

        // ハイスコア更新処理 (スコア演出完了後、一度だけ実行)
        if (!m_highScoreUpdated && GetPhaseTime() > 60 * 2)
        {
            m_highScoreUpdated = true; // フラグを立てて再実行を防ぐ
            Work.UpdateHighScore();
            bool isRankIn = false;
            for (int i = 0; i < Work.HIGH_SCORE_COUNT; i++)
            {
                int score = Work.GetHighScore(i);
                resultMenu.SetHighScore(i, score);
                // 今回のスコアがハイスコアにランクインした場合
                if (score != 0 && score == Work.GetScore() && !isRankIn)
                {
                    resultMenu.SetHighScoreMarkVisible(i, true);
                    isRankIn = true;
                }
            }
        }

        // ハイスコア表示後、一定時間待ってからキー入力待ちへ
        if (m_highScoreUpdated && GetPhaseTime() > 60 * 4)
        {
            if (Input.anyKeyDown)
            {
                SetPhase((int)Phase.Out);
            }
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
            SetExitCode(EXIT_CODE_TITLE);
            Exit();
        }
    }
}
