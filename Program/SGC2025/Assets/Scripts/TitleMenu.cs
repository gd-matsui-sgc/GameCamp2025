using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    // PressStart
    [SerializeField]
    private TMP_Text pressStartText = null;

    [Header("点滅設定")]
    [SerializeField, Tooltip("通常時の点滅速度")]
    private float blinkSpeed = 1.0f;

    // 決定済みかどうかを管理するフラグ
    private bool _isConfirmed = false;

    // 決定演出が完了したかどうかを管理するフラグ
    private bool _isFlashFinished = false;

    public void Awake()
    {
        // 安全のため、最初はテキストを非表示にしておく
        if (pressStartText != null)
        {
            SetTextAlpha(0);
        }
    }

    public void Update()
    {
        // 決定されていなければ、通常点滅を続ける
        if (!_isConfirmed && pressStartText != null)
        {
            // Mathf.PingPongを使って0と1の間を往復する値を作成し、アルファ値に設定
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
            SetTextAlpha(alpha);
        }
    }

    /// <summary>
    /// 決定時の演出を再生します。
    /// </summary>
    public void OnConfirm()
    {
        if (_isConfirmed) return; // 既に決定済みなら何もしない
        _isConfirmed = true;
        _isFlashFinished = false; // 演出開始時にフラグをリセット

        if (pressStartText != null)
        {
            StartCoroutine(ConfirmFlash());
        }
    }

    // 決定時の点滅を処理するコルーチン
    private IEnumerator ConfirmFlash()
    {
        // 3回素早く点滅させてから消える
        SetTextAlpha(0); yield return new WaitForSeconds(0.05f);
        SetTextAlpha(1); yield return new WaitForSeconds(0.05f);
        SetTextAlpha(0); yield return new WaitForSeconds(0.05f);
        SetTextAlpha(1); yield return new WaitForSeconds(0.05f);
        SetTextAlpha(0); yield return new WaitForSeconds(0.05f);
        SetTextAlpha(1);
        _isFlashFinished = true; // 演出完了
    }

    /// <summary>
    /// 決定演出（点滅して消える）が完了したかどうかを返します。
    /// </summary>
    /// <returns>完了していればtrue</returns>
    public bool IsConfirmFlashFinished()
    {
        return _isFlashFinished;
    }

    // テキストのアルファ値を設定するヘルパー関数
    private void SetTextAlpha(float alpha)
    {
        Color color = pressStartText.color;
        color.a = alpha;
        pressStartText.color = color;
    }
}
