using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("耐久設定")]
    [SerializeField] private int hitPoints = 3;       // HP
    [SerializeField] private GameObject deathEffect;  // 撃破エフェクト（任意）
    [SerializeField] private float hitCooldown = 0.05f;

    private bool isHitRecently = false;

    private void OnTriggerEnter(Collider other)
    {
        // 🥚 卵に当たった場合のみダメージ
        if (other.CompareTag("Egg"))
        {
            if (isHitRecently) return;
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

        Destroy(gameObject);
    }
}
