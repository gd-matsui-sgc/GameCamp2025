using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Work
{
    // 定数
    public static int HIGH_SCORE_COUNT = 3;

    // イベントシステム
    public static EventSystem eventSystem = null;

    // フェード
    public static Fade fade = null;

    // メインのオーディオリスナー
    public static AudioListener mainAudioListener = null;

    // スコア
    private static int m_score =5000;

    // ハイスコア
    private static List<int> m_highScores = new List<int>();

    // ロゴ
    public static Logo logo = null;

    // タイトル
    public static Title title = null;

    // ゲーム
    public static Game game = null;

    // リザルト
    public static Result result = null;

    // 静的コンストラクタ。ゲーム開始時に一度だけ呼ばれる
    static Work()
    {
        LoadHighScores();
    }

    // スコアを設定
    public static void SetScore(int _score)
    {
        m_score = _score;
    }

    // スコアを取得
    public static int GetScore()
    {
        return m_score;
    }

    // ハイスコアを更新
    public static void UpdateHighScore()
    {
        m_highScores.Add(m_score);
        // 降順（スコアが高い順）に並び替え
        m_highScores.Sort();
        m_highScores.Reverse();

        // 上位x件のみ保持する
        if (m_highScores.Count > HIGH_SCORE_COUNT)
        {
            m_highScores.RemoveRange(3, m_highScores.Count - HIGH_SCORE_COUNT);
        }

        // 更新したハイスコアを保存
        SaveHighScores();
    }

    // ハイスコアを取得
    public static int GetHighScore(int _index)
    {
        if (_index < m_highScores.Count)
        {
            return m_highScores[_index];
        }
        return 0;
    }

    /// <summary>
    /// ハイスコアをPlayerPrefsに保存します
    /// </summary>
    public static void ResetHighScores()
    {
        for (int i = 0; i < HIGH_SCORE_COUNT; i++)
        {
            // キーと値を設定
            string key = "HighScore_" + i;
            PlayerPrefs.SetInt(key, 0);
        }
        m_highScores.Clear();
        // 変更をディスクに書き込む
        PlayerPrefs.Save();
    }


    /// <summary>
    /// ハイスコアをPlayerPrefsに保存します
    /// </summary>
    private static void SaveHighScores()
    {
        for (int i = 0; i < HIGH_SCORE_COUNT; i++)
        {
            // キーと値を設定
            string key = "HighScore_" + i;
            int score = (i < m_highScores.Count) ? m_highScores[i] : 0;
            PlayerPrefs.SetInt(key, score);
        }
        // 変更をディスクに書き込む
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ハイスコアをPlayerPrefsから読み込みます
    /// </summary>
    private static void LoadHighScores()
    {
        m_highScores.Clear();
        for (int i = 0; i < HIGH_SCORE_COUNT; i++)
        {
            string key = "HighScore_" + i;
            m_highScores.Add(PlayerPrefs.GetInt(key, 0)); // データがなければ0を読み込む
        }
    }
}
