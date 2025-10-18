/*using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [Header("ターゲット設定")]
    public Transform target;  // プレイヤー

    [Header("カメラ位置設定")]
    public float distance = 4f;  // プレイヤーからの距離
    public float height = 4f;    // 高さ
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
}*/
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [Header("ターゲット設定")]
    public Transform target;

    [Header("カメラ位置設定")]
    public float distance = 4f;
    public float height = 2f;
    public float lookOffset = 1.5f;
    public float yRotationOffset = 180f; // ★ 向き補正用

    void LateUpdate()
    {
        if (!target) return;

        // 向き補正を加えた「背面方向」
        Vector3 behind = Quaternion.Euler(0, yRotationOffset, 0) * -target.forward * distance;

        Vector3 desiredPosition = target.position + behind + Vector3.up * height;

        transform.position = desiredPosition;

        transform.LookAt(target.position + Vector3.up * lookOffset);
    }
}
