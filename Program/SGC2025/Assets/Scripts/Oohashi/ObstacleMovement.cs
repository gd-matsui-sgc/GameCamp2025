using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Serialize�ϐ�
    [SerializeField, Header("�ǂꂭ�炢�ړ������邩")]
    private float _moveDistance = 10f;
    #endregion
    #region �ϐ�
    private Rigidbody _rigidBody = default;
    private float _originDistance = default;
    #endregion

    public void GetComponentProtocol()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void MoveProtocol()
    {
        _rigidBody.linearVelocity = new Vector3(_moveDistance, 0, 0);
    }
}
