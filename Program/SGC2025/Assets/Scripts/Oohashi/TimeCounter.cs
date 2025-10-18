using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField, Header("障害物のマネージャー")]
    private ObstacleManager _obstacleManager = default;
    private float _initTime = 0;
    private bool _canChangeSpeed = true;
    private bool _isFirst = true;
    public void Timer()
    {
        _initTime += Time.deltaTime;
        bool canChangeSpeed = _initTime % 10 <= 0.1f && _canChangeSpeed;
        if (canChangeSpeed)
        {
            if (_isFirst)
            {
                _isFirst = false;
                _canChangeSpeed = false;
                return;
            }
            Debug.Log("スピード変更");
            _obstacleManager.ChangeMoveSpeed();
            _canChangeSpeed = false;
        }

        if (_initTime % 10 > 0.1f)
        {
            _canChangeSpeed = true;
        }
    }
}
