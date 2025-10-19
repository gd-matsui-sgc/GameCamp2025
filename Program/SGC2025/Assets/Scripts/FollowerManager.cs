using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    [Header("フォーメーション設定")]
    [SerializeField] private float baseRadius = 1.2f;     // 最初の円の半径
    [SerializeField] private float ringSpacing = 0.9f;    // 各円の間隔
    [SerializeField] private int followersPerRing = 8;    // 1つの円に配置する仲間の数
    [SerializeField] private float heightOffset = 0.5f;   // 高さ
    [SerializeField] private float moveSpeed = 5f;        // 移動スピード
    [SerializeField] private float rotationOffset = 0f;   // 角度のずらし

    [Header("仲間リスト")]
    [SerializeField] private List<Transform> followers = new List<Transform>();

    // 仲間を追加
    public void AddFollower(Transform newFollower)
    {
        if (!followers.Contains(newFollower))
        {
            followers.Add(newFollower);
        }
        UpdateFormation();
    }

    // 仲間を削除
    public void RemoveFollower(Transform follower)
    {
        if (followers.Contains(follower))
        {
            followers.Remove(follower);
        }
    }

    private void UpdateFormation()
    {
        // Destroyされた仲間を削除
        followers.RemoveAll(f => f == null);

        int total = followers.Count;
        if (total == 0) return;

        // 🔹 円（リング）の数を計算
        int ringCount = Mathf.CeilToInt((float)total / followersPerRing);
        int index = 0; // 通し番号

        // 各リングを順に配置
        for (int ring = 0; ring < ringCount; ring++)
        {
            float radius = baseRadius + ring * ringSpacing;
            int countInRing = Mathf.Min(followersPerRing, total - index);
            float angleStep = 360f / countInRing;

            for (int i = 0; i < countInRing; i++)
            {
                if (index >= total) break;

                Transform friend = followers[index];
                if (friend == null) { index++; continue; }

                float angle = angleStep * i + rotationOffset;
                float rad = angle * Mathf.Deg2Rad;

                // 円周上の位置を計算
                Vector3 offset = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * radius;
                Vector3 targetPos = transform.position + offset + Vector3.up * heightOffset;

                // スムーズに移動
                friend.position = Vector3.Lerp(friend.position, targetPos, Time.deltaTime * moveSpeed);

                // プレイヤーと同じ向き（180°補正）
                friend.rotation = transform.rotation * Quaternion.Euler(0, 180f, 0);

                index++;
            }
        }
    }

    private void Update()
    {
        UpdateFormation();
    }
}
