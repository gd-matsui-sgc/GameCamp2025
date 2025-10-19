using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP設定")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private Image hpFillImage; // ← Sliderの代わりにImage
    private int currentHP;

    [Header("ダメージ設定")]
    [SerializeField] private int damageFromEnemy = 30;
    [SerializeField] private int damageFromFriend = 10;

    [Header("無敵設定")]
    [SerializeField] private float invincibleTime = 2f;
    [SerializeField] private float flashInterval = 0.1f;
    private bool isInvincible = false;

    private Renderer[] renderers;

    private void Start()
    {
        currentHP = maxHP;
        renderers = GetComponentsInChildren<Renderer>();
        UpdateHPUI();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();

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
            float fillAmount = (float)currentHP / maxHP;
            hpFillImage.fillAmount = fillAmount;
        }
    }

    private IEnumerator InvincibleRoutine()
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
        Debug.Log("Playerが倒れた！");
        // TODO: GameOver演出など
    }

    public void TakeSmallDamageFromFriend()
    {
        TakeDamage(damageFromFriend);
    }
}
