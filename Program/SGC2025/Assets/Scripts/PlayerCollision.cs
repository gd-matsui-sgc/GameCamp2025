using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("衝突タグ設定")]
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private string lightTruckTag = "LightTruck";

    [Header("ダメージ設定")]
    [SerializeField] private int damageFromObstacle = 20;
    [SerializeField] private int damageFromEnemy = 30;
    [SerializeField] private int damageFromLightTruck = 50;

    private PlayerHealth health;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        if (health == null)
        {
            Debug.LogWarning("PlayerHealth コンポーネントが見つかりません！");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health == null) return;

        // 敵 or 障害物に触れたらダメージ
        if (other.CompareTag(enemyTag))
        {
            health.TakeDamage(damageFromEnemy);
        }
        else if (other.CompareTag(obstacleTag))
        {
            health.TakeDamage(damageFromObstacle);
        }else if(other.CompareTag(lightTruckTag))
        {
            health.TakeDamage(damageFromLightTruck);
        }
    }
}
