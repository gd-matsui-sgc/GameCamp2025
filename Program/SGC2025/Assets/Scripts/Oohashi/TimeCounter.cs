using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField, Header("障害物のマネージャー")]
    private ObstacleManager _obstacleManager = default;
    private float _initTime = 0;
    public void Timer()
    {
        _initTime += Time.deltaTime;
        if(_initTime % 10 <= 0)
        {
            Debug.Log("スピード変更");
            _obstacleManager.ChangeMoveSpeed();
        }
    }
}
