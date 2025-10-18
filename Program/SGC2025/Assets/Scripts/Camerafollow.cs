using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [Header("ターゲット設定")]
    public Transform target;  // プレイヤー

    [Header("カメラ位置設定")]
    public float distance = 8f;  // プレイヤーからの距離
    public float height = 6f;    // 高さ
    public float angle = 45f;    // 見下ろし角度（度数）

    void LateUpdate()
    {
        if (!target) return;

        // X軸にだけ回転をかけて見下ろす
        Quaternion rotation = Quaternion.Euler(angle, 0f, 0f);

        // プレイヤー後方にオフセットを作成
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        // プレイヤーの位置からオフセットを加えた「固定位置」
        Vector3 desiredPosition = target.position + offset + Vector3.up * height;

        // 位置を直接設定（累積しない！）
        transform.position = desiredPosition;

        // プレイヤーを見る
        transform.LookAt(target);
    }
}
