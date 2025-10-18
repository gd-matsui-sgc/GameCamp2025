using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    [Header("円形整列設定")]
    [SerializeField] private float radius = 2.5f;
    [SerializeField] private float heightOffset = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationOffset = 0f;

    [Header("仲間リスト")]
    [SerializeField] private List<Transform> followers = new List<Transform>();

    public void AddFollower(Transform newFollower)
    {
        if (!followers.Contains(newFollower))
        {
            followers.Add(newFollower);
        }
        UpdateFormation();
    }

    private void UpdateFormation()
    {
        int count = followers.Count;
        if (count == 0) return;

        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i + rotationOffset;
            float rad = angle * Mathf.Deg2Rad;

            // 円の位置を計算
            Vector3 offset = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * radius;
            Vector3 targetPos = transform.position + offset + Vector3.up * heightOffset;

            // スムーズに追従
            followers[i].position = Vector3.Lerp(followers[i].position, targetPos, Time.deltaTime * moveSpeed);

            // ★ 常にプレイヤーと同じ向きを強制的に適用
            followers[i].rotation = transform.rotation * Quaternion.Euler(0, 180f, 0);

        }
    }

    private void Update()
    {
        UpdateFormation();
    }
}
