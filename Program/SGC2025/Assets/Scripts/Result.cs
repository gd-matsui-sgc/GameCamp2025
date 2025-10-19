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
        // --- 1. スコア演出開始 (2秒待ってから) ---
        if (GetPhaseTime() == 120 && resultMenu != null && !resultMenu.IsScoreMoving() && !m_highScoreUpdated)
        {
            resultMenu.SetScore(Work.GetScore());
        }

        // --- 2. スコア演出が完了するのを待つ ---
        if (resultMenu != null && resultMenu.IsScoreMoving())
        {
            return;
        }

        // --- 3. スコア演出完了後、ハイスコア更新処理を一度だけ実行 ---
        //    GetPhaseTime() > 120 は、スコア演出が開始された後であることを保証します。
        if (!m_highScoreUpdated && GetPhaseTime() > 120)
        {
            sound.PlaySE(SoundDefine.SE.SHOW_AMOUNT);

            // ハイスコア更新処理を実行
            UpdateAndDisplayHighScores();

            // 次のフレームからキー入力待ちに移行できるように、フェーズ時間をリセット
            SetPhaseTime(0);
        }

        // --- 4. ハイスコア更新後、1秒間の余韻(Pause)をおいてからキー入力待ちへ ---
        if (m_highScoreUpdated && GetPhaseTime() > 60) // 60フレーム = 1秒
        {
            if (Input.anyKeyDown)
            {
                sound.PlaySE(SoundDefine.SE.CONFIRM_16);
                SetPhase((int)Phase.Out);
            }
        }
    }

    /// <summary>
    /// ハイスコアを更新し、結果を画面に表示します。
    /// </summary>
    private void UpdateAndDisplayHighScores()
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
                resultMenu.HighlightHighScore(i);

                // SEクリップを取得して、その長さだけBGMの音量を下げる
                AudioClip seClip = sound.GetSEClip(SoundDefine.SE.CHICKEN_CRY_1);
                float seDuration = seClip != null ? seClip.length : 0.5f; // クリップが見つからない場合は0.5秒

                sound.DuckBGM(0.3f, seDuration);

                // SEを再生してアニメーション開始
                sound.PlaySE(SoundDefine.SE.CHICKEN_CRY_1);
                resultMenu.PlayStampAnimation();
                isRankIn = true;
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
