using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Serialize�ϐ�
    #endregion
    #region �ϐ�
    private Rigidbody _rigidBody = default;
    private float _moveSpeed = 0;
    #endregion
    /// <summary>
    /// �������ꂽ���ɃR���|�[�l���g���擾����
    /// </summary>
    public void GetComponentProtocol()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// �ړ�
    /// </summary>
    public void MoveProtocol()
    {
        Debug.Log("���݂̈ړ����x��" + _moveSpeed);
        _rigidBody.linearVelocity = new Vector3(_moveSpeed, 0, 0);
    }

    /// <summary>
    /// ���x���Z�b�g����
    /// </summary>
    /// <param name="speed">�ݒ肷��ړ����x</param>
    public void SpeedUp(int speed)
    {
        _moveSpeed = speed;
    }
}
