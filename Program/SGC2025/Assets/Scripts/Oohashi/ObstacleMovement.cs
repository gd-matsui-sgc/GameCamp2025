using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Serialize•Ï”
    [SerializeField, Header("‚Ç‚ê‚­‚ç‚¢‚Ì‘¬“x‚ÅˆÚ“®‚³‚¹‚é‚©")]
    private float _moveSpeed = -10f;
    #endregion
    #region •Ï”
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
