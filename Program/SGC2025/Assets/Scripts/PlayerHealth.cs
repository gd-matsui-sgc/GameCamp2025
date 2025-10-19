using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // ← シーン切り替えに必要

public class PlayerHealth : MonoBehaviour
{
    [Header("HP設定")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private Image hpFillImage; // HPバー（Fill用Image）
    private int currentHP;

    [Header("ダメージ設定")]
    [SerializeField] private int damageFromEnemy = 30;
    [SerializeField] private int damageFromFriend = 10;

    [Header("無敵設定")]
    [SerializeField] private float invincibleTime = 2f;
    [SerializeField] private float flashInterval = 0.1f;
    private bool isInvincible = false;
    private bool m_isDie = false;

    private Renderer[] renderers;

    private void Start()
    {
        currentHP = maxHP;

        // HPバーの初期化
        if (hpFillImage != null)
            hpFillImage.fillAmount = 1f;

        renderers = GetComponentsInChildren<Renderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();

        Debug.Log($"[PlayerHealth] ダメージ {amount} → 残りHP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibleRoutine());
        }
    }

    private void UpdateHPUI()
    {
        if (hpFillImage != null)
        {
            float ratio = (float)currentHP / maxHP;
            hpFillImage.fillAmount = ratio;
        }
    }

    private System.Collections.IEnumerator InvincibleRoutine()
    {
        isInvincible = true;
        float timer = 0f;

        while (timer < invincibleTime)
        {
            SetVisible(false);
            yield return new WaitForSeconds(flashInterval);
            SetVisible(true);
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval * 2;
        }

        isInvincible = false;
        SetVisible(true);
    }

    private void SetVisible(bool visible)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }
    }

    private void Die()
    {
        Debug.Log("💀 Player死亡 → リザルト画面へ遷移！");

        // 無敵解除＆非表示解除（念のため）
        isInvincible = false;
        SetVisible(true);

        m_isDie = true;
    }

    public bool IsDie()
    {
        return m_isDie;
    }

    // Friendが消えた時にHPを減らす
    public void TakeSmallDamageFromFriend()
    {
        TakeDamage(damageFromFriend);
    }
}
