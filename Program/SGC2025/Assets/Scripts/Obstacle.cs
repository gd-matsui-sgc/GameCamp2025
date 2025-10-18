using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("耐久設定")]
    [SerializeField] private int hitPoints = 3;
    [SerializeField] private float hitCooldown = 0.05f;

    [Header("仲間生成設定")]
    [SerializeField] private GameObject friendPrefab;   // 仲間プレハブ
    [SerializeField] private Transform player;          // プレイヤー参照
    [SerializeField] private int spawnFriendCount = 1;  // 一度に増える仲間数

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

            if (hitPoints <= 0)
            {
                SpawnFollowers();
                Destroy(gameObject);
            }
        }
    }

    private void ResetHitFlag() => isHitRecently = false;

    private void SpawnFollowers()
    {
        // プレイヤーのフォロワーマネージャーを取得
        FollowerManager manager = player.GetComponent<FollowerManager>();
        if (manager == null)
        {
            Debug.LogWarning("プレイヤーに FollowerManager がアタッチされていません！");
            return;
        }

        for (int i = 0; i < spawnFriendCount; i++)
        {
            // プレイヤーの近くにランダムでスポーン
            Vector3 spawnPos = player.position + Random.insideUnitSphere * 1f;
            spawnPos.y = player.position.y + 0.5f;

            // 仲間を生成（向きはプレイヤー基準）
            GameObject friend = Instantiate(friendPrefab, spawnPos, player.rotation);

            // FollowerManager に登録（＝円形整列に加える）
            manager.AddFollower(friend.transform);
        }
    }
    private void Start()
{
    // Inspectorで設定されていなければ、自動でPlayerを探す
    if (player == null)
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogWarning("Playerが見つかりません。Tagを 'Player' に設定してください。");
        }
    }
}
}
