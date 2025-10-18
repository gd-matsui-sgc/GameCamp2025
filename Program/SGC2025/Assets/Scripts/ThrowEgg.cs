using UnityEngine;

public class AutoEggShooter : MonoBehaviour
{
    [Header("参照設定")]
    [SerializeField] private GameObject eggPrefab;  // 卵プレハブ
    [SerializeField] private Transform mouthPoint;  // 口の位置

    [Header("発射設定")]
    [SerializeField] private float shootInterval = 0.5f; // 発射間隔（秒）
    [SerializeField] private float eggLifeTime = 10f;    // 卵が自動で消えるまでの時間

    private float timer;  // 内部タイマー

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            ShootEgg();
            timer = 0f;
        }
    }

    private void ShootEgg()
    {
        // 口の位置と向きで卵を生成
        GameObject egg = Instantiate(eggPrefab, mouthPoint.position, mouthPoint.rotation);

        // Rigidbodyを取得して重力を無効化
        Rigidbody rb = egg.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }

        // 安全のため一定時間後に削除
        Destroy(egg, eggLifeTime);
    }
}
