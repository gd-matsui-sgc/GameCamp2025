using UnityEngine;

public class EggProjectile : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 10f;

    [Header("物理設定")]
    [SerializeField] private bool useGravity = false;

    [Header("コライダー設定")]
    [SerializeField] private bool autoAdjustCollider = true; // コライダーを自動調整するか
    [SerializeField] private Vector3 colliderScaleOffset = Vector3.one; // 調整倍率（微調整用）

    private Rigidbody rb;
    private BoxCollider boxCollider;

    private void Awake()
    {
        // Rigidbodyをキャッシュ or 自動追加
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        // BoxColliderをキャッシュ or 自動追加
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
            boxCollider = gameObject.AddComponent<BoxCollider>();
    }

    private void Start()
    {
        // 重力設定
        rb.useGravity = useGravity;
        rb.isKinematic = false;

        // コライダーを卵の大きさに合わせる
        if (autoAdjustCollider)
        {
            Vector3 localScale = transform.localScale;
            boxCollider.size = Vector3.Scale(localScale, colliderScaleOffset);
            boxCollider.center = Vector3.zero;
        }

        // 前に飛ばす
        rb.linearVelocity = transform.forward * speed;

        // 一定時間で削除
        Destroy(gameObject, lifeTime);
    }

    
}
