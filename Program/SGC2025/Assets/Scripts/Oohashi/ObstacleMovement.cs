using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Serialize変数
    #endregion
    #region 変数
    private Rigidbody _rigidBody = default;
    private float _moveSpeed = 0;
    #endregion
    /// <summary>
    /// 生成された時にコンポーネントを取得する
    /// </summary>
    public void GetComponentProtocol()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// 移動
    /// </summary>
    public void MoveProtocol()
    {
        Debug.Log("現在の移動速度は" + _moveSpeed);
        _rigidBody.linearVelocity = new Vector3(_moveSpeed, 0, 0);
    }

    /// <summary>
    /// 速度をセットする
    /// </summary>
    /// <param name="speed">設定する移動速度</param>
    public void SpeedUp(int speed)
    {
        _moveSpeed = speed;
    }
}
