using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Serialize�ϐ�
    [SerializeField, Header("�ǂꂭ�炢�̑��x�ňړ������邩")]
    private float _moveSpeed = -10f;
    #endregion
    #region �ϐ�
    private Rigidbody _rigidBody = default;
    #endregion

    public void GetComponentProtocol()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void MoveProtocol()
    {
        _rigidBody.linearVelocity = new Vector3(_moveSpeed, 0, 0);
    }
}
