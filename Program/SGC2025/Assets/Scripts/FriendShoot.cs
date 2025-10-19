/*using UnityEngine;

public class FriendShoot : MonoBehaviour
{
    [Header("射撃設定")]
    [SerializeField] private GameObject eggPrefab;   // 卵プレハブ（Playerと同じ）
    [SerializeField] private Transform shootPoint;   // 発射位置
    [SerializeField] private float shootInterval = 1.5f;  // 発射間隔
    [SerializeField] private float eggSpeed = 10f;         // 弾のスピード
    [Header("発射方向補正（角度）")]
    [SerializeField] private float yRotationOffset = 90f; // ★ モデルの向きズレ補正

    private float timer;

    private void Start()
    {
        // shootPoint が未指定なら自分の位置を使う
        if (shootPoint == null)
            shootPoint = this.transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        if (eggPrefab == null) return;

        Quaternion shootRot = transform.rotation * Quaternion.Euler(0, yRotationOffset, 0);
        Vector3 shootDir = shootRot * Vector3.forward;

        // 弾を生成
        GameObject egg = Instantiate(eggPrefab, shootPoint.position + transform.forward * 0.5f, transform.rotation);

        // Rigidbodyを取得して前方向に飛ばす
        Rigidbody rb = egg.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * eggSpeed;
        }

        // 一定時間後に自動で消す
        Destroy(egg, 5f);
    }
}*/
using UnityEngine;

public class FriendShoot : MonoBehaviour
{
    [Header("射撃設定")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform shootPoint;   // 弾の出る位置（口の前とか）
    [SerializeField] private float shootInterval = 1.5f;
    [SerializeField] private float eggSpeed = 10f;

    private float timer;

    private void Start()
    {
        // shootPoint が未設定なら自分の位置を使う
        if (shootPoint == null)
            shootPoint = this.transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        if (eggPrefab == null || shootPoint == null) return;

        // 🎯 shootPoint の forward（青矢印の方向）に撃つ！
        Vector3 shootDir = shootPoint.forward;

        GameObject egg = Instantiate(
            eggPrefab,
            shootPoint.position + shootDir * 0.5f,
            Quaternion.LookRotation(shootDir)
        );

        Rigidbody rb = egg.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDir * eggSpeed;
        }

        Destroy(egg, 5f);
    }
}
