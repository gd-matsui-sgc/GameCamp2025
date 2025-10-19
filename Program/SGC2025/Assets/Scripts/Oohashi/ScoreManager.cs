using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField, Header("テキスト")]
    private TextMeshProUGUI _scoreText = default;
    private int _initScore = 0;
    public int Score
    {
        get { return _initScore; }
    }

    private FollowerManager _follow = default;

    private int _scoreMultiplier = 1;

    private void Start()
    {
        _follow = GameObject.FindWithTag("Player").GetComponent<FollowerManager>();
    }


    public void UpdateScoreValue(int score)
    {
        if(_follow.Followers.Count == 0 || _follow.Followers.Count == 1)
        {
            _scoreMultiplier = 1;
        }
        else
        {
            _scoreMultiplier = _follow.Followers.Count;
        }
        _initScore += score * _scoreMultiplier;
        _scoreText.text = _initScore.ToString();
    }
}
