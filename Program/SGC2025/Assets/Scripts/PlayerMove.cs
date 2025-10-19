using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveZ_Rigidbody : MonoBehaviour
{
    public float moveSpeed = 5f;       // 移動速度
    private Rigidbody rb;              // Rigidbody参照
    private bool _canMove = true;
    public bool CanMove
    {
        set { _canMove = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // プレイヤーが落ちないように設定（Y固定）
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | 
                         RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        Vector3 pos = rb.position;
        pos.z = Mathf.Clamp(pos.z, -10f, 10f);
        rb.position = pos;

        if (!_canMove)
        {
            return;
        }
        // ←→キーまたはA/Dキー入力
        float move = Input.GetAxis("Horizontal");

        // 現在速度を基準にしてZ方向に力を加える
        Vector3 velocity = rb.linearVelocity;
        velocity.z = move * moveSpeed * -1;
        rb.linearVelocity = velocity;


    }
}
