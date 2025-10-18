using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultMenu : MonoBehaviour
{
    // ハイスコア用テキスト
    [SerializeField]
    public List<TMP_Text> highScoreTexts = null;

    [SerializeField]
    public List<GameObject> highScoreMarks = null;

    // スコア用テキスト
    [SerializeField]
    public TMP_Text scoreText = null;


    private Tween m_scoreTween = null;

    public int m_score = 0;

    public void Awake()
    {
        m_scoreTween = gameObject.AddComponent<Tween>();
        for (int i = 0; i < highScoreMarks.Count; i++)
        {
            SetHighScoreMarkVisible(i, false);
        }
    }

    public void Update()
    {
        if (m_scoreTween.IsPlaying())
        {
            int score = Mathf.RoundToInt(m_scoreTween.GetProgress().x);
            scoreText.text = score.ToString();
        }
        else
        {
            scoreText.text = m_score.ToString();
        }
    }

    public bool IsScoreMoving()
    {
        return m_scoreTween.IsPlaying();
    }

    public void SetScore(int _score)
    {
        m_scoreTween.Play(Vector3.zero, Vector3.one * _score, 1, Tween.Mode.Linear);
        m_score = _score;
    }

    public void SetHighScore(int _index, int _score)
    {
        if( _index < highScoreMarks.Count )
        {
            highScoreTexts[_index].text = _score.ToString();
        }
    }

    public void SetHighScoreMarkVisible(int _index, bool _isVisible)
    {
        if( _index < highScoreMarks.Count )
        {
            highScoreMarks[_index].SetActive(_isVisible);
        }
    }
}
