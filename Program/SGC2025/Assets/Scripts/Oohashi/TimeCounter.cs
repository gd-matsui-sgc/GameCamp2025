using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField, Header("��Q���̃}�l�[�W���[")]
    private ObstacleManager _obstacleManager = default;
    private float _initTime = 0;
    public void Timer()
    {
        _initTime += Time.deltaTime;
        if(_initTime % 10 <= 0)
        {
            Debug.Log("�X�s�[�h�ύX");
            _obstacleManager.ChangeMoveSpeed();
        }
    }
}
