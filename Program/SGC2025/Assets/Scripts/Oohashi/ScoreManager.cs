using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField, Header("テキスト")]
    private TextMeshProUGUI _scoreText = default;
    private int _initScore = 0;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            UpdateScoreValue(100);
        }
    }

    public void UpdateScoreValue(int score)
    {
        _initScore += score;
        _scoreText.text = _initScore.ToString();
    }
}
