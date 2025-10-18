using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Serialize•Ï”
    [SerializeField, Header("‚Ç‚ê‚­‚ç‚¢ˆÚ“®‚³‚¹‚é‚©")]
    private float _moveDistance = 10f;
    #endregion
    #region •Ï”
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
