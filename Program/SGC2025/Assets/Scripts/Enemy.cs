using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("耐久設定")]
    [SerializeField] private int hitPoints = 3;       // HP
    [SerializeField] private GameObject deathEffect;  // 撃破エフェクト（任意）
    [SerializeField] private float hitCooldown = 0.05f;

    private ObstacleManager _obstacleManager = default;
    private ScoreManager _scoreManager = default;

    private bool isHitRecently = false;

    private void Start()
    {
        _obstacleManager =GameObject.FindWithTag("ObstacleManager").GetComponent<ObstacleManager>();
        _scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 🥚 卵に当たった場合のみダメージ
        if (other.CompareTag("Egg"))
        {
            if (isHitRecently) return;
            SoundManager.Instance.PlaySE(SoundDefine.SE.DOOR_KICK);
            isHitRecently = true;
            Invoke(nameof(ResetHitFlag), hitCooldown);

            // 卵を削除
            Destroy(other.gameObject);

            // HPを減らす
            hitPoints--;

            if (hitPoints <= 0)
            {
                Die();
            }
        }
    }

    private void ResetHitFlag() => isHitRecently = false;

    private void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        _obstacleManager.DeActiveObj(this.gameObject);
        _scoreManager.UpdateScoreValue(100);
    }
}
