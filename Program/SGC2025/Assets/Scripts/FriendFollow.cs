using UnityEngine;

public class FriendFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // 追いかける対象（プレイヤー）
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float followDistance = 2f;

    private Vector3 followOffset;

    private void Start()
    {
        // 初期オフセットをランダムにして、自然にバラけさせる
        followOffset = new Vector3(
            Random.Range(-followDistance, followDistance),
            0,
            Random.Range(-followDistance, followDistance)
        );
    }

    private void Update()
    {
        if (!target) return;

        // プレイヤーの後ろ側（forwardの逆方向）＋オフセットに向かう
        Vector3 targetPos = target.position - target.forward * followDistance + followOffset;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    // 外部から追従ターゲットを設定できるようにする
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
