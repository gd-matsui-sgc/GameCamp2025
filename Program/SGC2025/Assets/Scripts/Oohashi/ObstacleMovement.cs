using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Serialize�ϐ�
    [SerializeField, Header("�ǂꂭ�炢�ړ������邩")]
    private float _moveDistance = 10f;
    #endregion
    #region �ϐ�
    private Rigidbody _rigidBody = default;
    #endregion

    public void GetComponentProtocol()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
}
