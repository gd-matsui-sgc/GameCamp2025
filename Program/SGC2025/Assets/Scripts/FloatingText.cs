using TMPro;
using UnityEngine;

/// <summary>
/// 3D空間に表示されるフローティングテキストを制御します。
/// (例: +100, Damage Text)
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class FloatingText : MonoBehaviour
{
    [Header("アニメーション設定")]
    [SerializeField, Tooltip("移動する高さ")]
    private float moveHeight = 1.5f;

    [SerializeField, Tooltip("アニメーション時間（秒）")]
    private float duration = 1.0f;

    [SerializeField, Tooltip("表示が消えるまでの待機時間（秒）")]
    private float lifeTime = 0.5f;

    private TMP_Text _textMesh;
    private Tween _alphaTween;
    private Tween _positionTween;
    private Camera _mainCamera;

    private float _timer;
    private bool _isPlaying;

    void Awake()
    {
        _textMesh = GetComponent<TMP_Text>();
        _alphaTween = gameObject.AddComponent<Tween>();
        _positionTween = gameObject.AddComponent<Tween>();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (!_isPlaying) return;

        // カメラの方向を常に向くようにする (ビルボード効果)
        if (_mainCamera != null)
        {
            transform.localEulerAngles = new Vector3( 0.0f, 90.0f, 0.0f);
        }

        // Tweenの値を反映
        if (_positionTween.IsPlaying())
        {
            transform.position = _positionTween.GetProgress();
        }
        if (_alphaTween.IsPlaying())
        {
            Color color = _textMesh.color;
            color.a = _alphaTween.GetProgress().x;
            _textMesh.color = color;
        }

        // 表示時間が過ぎたらフェードアウトを開始
        _timer += Time.deltaTime;
        if (_timer > lifeTime && !_alphaTween.IsPlaying())
        {
            _isPlaying = false;
            gameObject.SetActive(false);
        }
    }

    public void Play(string text, Vector3 position, Color color)
    {
        _textMesh.text = text;
        _textMesh.color = color;
        transform.position = position;
        _timer = 0f;
        _isPlaying = true;
        gameObject.SetActive(true);

        // アニメーションを開始
        _positionTween.Play(position, position + Vector3.up * moveHeight, duration, Tween.Mode.Sub);
        _alphaTween.Play(Vector3.one, Vector3.zero, duration, Tween.Mode.Add); // xをアルファ値として使う
    }
}
