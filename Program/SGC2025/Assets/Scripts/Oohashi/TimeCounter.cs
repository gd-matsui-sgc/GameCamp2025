using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField, Header("障害物のマネージャー")]
    private ObstacleManager _obstacleManager = default;
    [SerializeField,Header("スポーンのマネージャー")]
    private SpawnManager _spawnManager = default;
    [SerializeField, Header("何秒ごとにスピードアップさせるか")]
    private float _speedUpEveryTime = 10;
    [SerializeField, Header("何秒ごとにスポーンさせるか")]
    private float _spawnTime = 5;
    [SerializeField, Header("障害物生成時間をどれだけ短くするか")]
    private float _shorteningTime = 0.01f;
    private float _initTime = 0;
    private bool _canChangeSpeed = true;
    private bool _canSpawn = true;
    private bool _isFirst = true;
    public void Timer()
    {
        _initTime += Time.deltaTime;
        bool canChangeSpeed = _initTime % _speedUpEveryTime <= 0.1f && _canChangeSpeed;
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

        if (_initTime % _speedUpEveryTime > 0.1f)
        {
            _canChangeSpeed = true;
        }

        bool canSpawn = _initTime % _spawnTime <= 0.1f && _canSpawn;
        if (canSpawn)
        {
            Debug.Log("スポーン");
            _spawnManager.RandomSpawn();
            _canSpawn = false;
            _spawnTime -= _shorteningTime;
            _spawnTime *= 10;
            _spawnTime = Mathf.Floor(_spawnTime) / 10;
            _spawnTime = Mathf.Clamp(_spawnTime,0.5f,_spawnTime); 
        }
        if(_initTime % _spawnTime > 0.1f)
        {
            _canSpawn = true;
        }
    }
}
