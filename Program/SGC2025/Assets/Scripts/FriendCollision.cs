using UnityEngine;

public class FriendCollision : MonoBehaviour
{
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private string enemyTag = "Enemy"; // ★ 敵のタグを追加
    [SerializeField] private GameObject deathEffect;

    private void OnTriggerEnter(Collider other)
    {
        // 障害物 or 敵に当たった場合
        if (other.CompareTag(obstacleTag) || other.CompareTag(enemyTag))
        {
            // FollowerManager から除外
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var manager = player.GetComponent<FollowerManager>();
                if (manager != null)
                {
                    manager.RemoveFollower(this.transform);
                }
            }

            // エフェクト再生（任意）
            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, Quaternion.identity);

            Destroy(gameObject); // Friend消滅
        }
    }
}
