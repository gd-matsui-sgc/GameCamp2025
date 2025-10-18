using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField, Header("��Q���̃}�l�[�W���[")]
    private ObstacleManager _obstacleManager = default;
    [SerializeField,Header("�X�|�[���̃}�l�[�W���[")]
    private SpawnManager _spawnManager = default;
    [SerializeField, Header("���b���ƂɃX�s�[�h�A�b�v�����邩")]
    private float _speedUpEveryTime = 10;
    [SerializeField, Header("���b���ƂɃX�|�[�������邩")]
    private float _spawnTime = 5;
    [SerializeField, Header("��Q���������Ԃ��ǂꂾ���Z�����邩")]
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
            Debug.Log("�X�s�[�h�ύX");
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
            Debug.Log("�X�|�[��");
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
