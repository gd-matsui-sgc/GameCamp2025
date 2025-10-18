using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("耐久設定")]
    [SerializeField] private int hitPoints = 3;

    [Header("当たり判定設定")]
    [SerializeField] private float hitCooldown = 0.05f;

    [Header("仲間生成設定")]
    [SerializeField] private GameObject friendPrefab;  // 仲間のプレハブ
    [SerializeField] private Transform player;          // プレイヤー参照
    [SerializeField] private int spawnFriendCount = 1;  // 生成する仲間の数

    private bool isHitRecently = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isHitRecently) return;

        if (collision.gameObject.CompareTag("Egg"))
        {
            isHitRecently = true;
            Invoke(nameof(ResetHitFlag), hitCooldown);

            Destroy(collision.gameObject);
            hitPoints--;

            Debug.Log($"{gameObject.name} に卵命中！ 残りHP: {hitPoints}");

            if (hitPoints <= 0)
            {
                BreakObstacle();
            }
        }
    }

    private void ResetHitFlag()
    {
        isHitRecently = false;
    }

    private void BreakObstacle()
    {
        Debug.Log($"{gameObject.name} が破壊された！ 仲間が増える！");

        // 仲間を生成
        for (int i = 0; i < spawnFriendCount; i++)
        {
           // プレイヤーの向きを基準に、少し後ろ側で出す
            Vector3 backOffset = player.forward * 2f;
            Vector3 spawnPos = player.position + backOffset + new Vector3(
               Random.Range(-0.5f, 0.5f),
               0.5f,
               Random.Range(-0.5f, 0.5f)
);

// ★ プレイヤーの回転そのままで生成！
GameObject friend = Instantiate(friendPrefab, spawnPos, player.rotation * Quaternion.Euler(0, 180f, 0));

            // 追従先（プレイヤー）を設定
            FriendFollow follow = friend.GetComponent<FriendFollow>();
            if (follow != null && player != null)
            {
                follow.SetTarget(player);
            }
        }

        Destroy(gameObject);
    }
}
