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

    // スコア
    private static int m_score =5000;

    // ハイスコア
    private static List<int> m_highScores = new List<int>();

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

}
