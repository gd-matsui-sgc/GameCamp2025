using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フローティングテキストの生成とプールを管理するシングルトンクラス。
/// </summary>
public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    [SerializeField, Tooltip("最初にプールしておく数")]
    private int poolSize = 50;

    private List<FloatingText> _pool = new List<FloatingText>();

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameObject floatingTextPrefab = Resources.Load<GameObject>("Prefabs/UIs/ScorePopup");

        // プレハブからプールを生成
        if (floatingTextPrefab != null)
        {

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(floatingTextPrefab, transform);
                obj.SetActive(false);
                _pool.Add(obj.GetComponent<FloatingText>());
            }
        }
    }

    /// <summary>
    /// フローティングテキストを表示します。
    /// </summary>
    /// <param name="text">表示する文字列</param>
    /// <param name="position">表示する3D座標</param>
    /// <param name="color">テキストの色</param>
    public void Show(string text, Vector3 position, Color color)
    {
        // プールから利用可能なオブジェクトを探す
        foreach (var floatingText in _pool)
        {
            if (!floatingText.gameObject.activeInHierarchy)
            {
                floatingText.Play(text, position, color);
                return;
            }
        }

        // TODO: プールが足りない場合、新しく生成する処理を追加することも可能
        Debug.LogWarning("FloatingText pool is empty. Consider increasing the pool size.");
    }
}
