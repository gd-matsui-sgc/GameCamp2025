using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("耐久設定")]
    [SerializeField] private int hitPoints = 3;            // 耐久値（壊れるまでのヒット数）
    [SerializeField] private float hitCooldown = 0.05f;    // 当たり判定のクールタイム

    [Header("仲間生成設定")]
    [SerializeField] private GameObject friendPrefab;      // 仲間プレハブ
    [SerializeField] private Transform player;             // プレイヤー参照
    [SerializeField] private int spawnFriendCount = 1;     // 生成する仲間数

    private bool isHitRecently = false;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (isHitRecently) return;

        // 🥚 卵に当たった時だけ破壊処理
        if (collision.gameObject.CompareTag("Egg"))
        {
            isHitRecently = true;
            Invoke(nameof(ResetHitFlag), hitCooldown);

            Destroy(collision.gameObject);
            hitPoints--;

            if (hitPoints <= 0)
            {
                SpawnFollowers();
                Destroy(gameObject); // 障害物を削除
            }
        }

        // 🐥 Friendがぶつかっても何もしない（障害物は壊れない）
        else if (collision.gameObject.CompareTag("Friend"))
        {
            // 何も処理しない
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

            // 仲間を生成（プレイヤーの向きに合わせる）
            GameObject friend = Instantiate(friendPrefab, spawnPos, player.rotation);

            // FollowerManager に登録（円形フォーメーションに追加）
            manager.AddFollower(friend.transform);
        }
    }
}
