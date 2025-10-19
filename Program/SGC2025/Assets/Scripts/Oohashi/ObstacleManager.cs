using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    #region Serialize�ϐ�
    [SerializeField, Header("�{����")]
    private GameObject _chikenHut = default;
    [SerializeField, Header("�y�g��")]
    private GameObject _lightTruck = default;
    [SerializeField, Header("�G")]
    private GameObject _enemy = default;
    [SerializeField, Header("�����𐶐����鐔")]
    private int _generateHutCount = 25;
    [SerializeField, Header("�y�g���𐶐����鐔")]
    private int _generateTruckCount = 25;
    [SerializeField,Header("�G�𐶐����鐔")]
    private int _generateEnemyCount = 25;

    [SerializeField, Header("���������ړ������鏉�����x(�}�C�i�X����)")]
    private int _hutMoveSpeed = -10;
    [SerializeField, Header("�y�g�����ړ������鏉�����x")]
    private int _lightTruckMoveSpeed = -20;
    [SerializeField, Header("�G���ړ������鏉�����x")]
    private int _enemyMoveSpeed = -15;

    [SerializeField, Header("���̑��x�ύX�łǂꂭ�炢���x��ς��邩")]
    private int _changeMoveSpeedValue = -10;

    #endregion

    #region �ϐ�
    private List<GameObject> _hutList = new List<GameObject>();
    private List<GameObject> _truckList = new List<GameObject>();
    private List<GameObject> _enemyList = new List<GameObject>();
    private List<ObstacleMovement> _moveScriptList = new List<ObstacleMovement>();

    private int _hutIndex = 0;
    private int _truckIndex = 0;
    private int _enemyIndex = 0;
    #endregion

    /// <summary>
    /// �������A�y�g���A�G�̃v�[�����쐬
    /// </summary>
    public void CreatePool()
    {
        GameObject obj = null;
        ObstacleMovement move = null;
        for(int i = 0; i < _generateHutCount; i++)
        {
            obj = Instantiate(_chikenHut);
            move = obj.GetComponent<ObstacleMovement>();
            move.GetComponentProtocol();
            _hutList.Add(obj);
            _moveScriptList.Add(move);
            obj.SetActive(false);
        }
        for(int i = 0;i< _generateTruckCount; i++)
        {
            obj = Instantiate(_lightTruck);
            move = obj.GetComponent<ObstacleMovement>();
            move.GetComponentProtocol();
            _moveScriptList.Add(move);
            _truckList.Add(obj);
            obj.SetActive(false);
        }
        for(int i = 0;i < _generateEnemyCount;i++)
        {
            obj = Instantiate(_enemy);
            move = obj.GetComponent<ObstacleMovement>();
            move.GetComponentProtocol();
            _moveScriptList.Add(move);
            _enemyList.Add(obj);
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// ���������A�N�e�B�u�ɂ��č��W���Z�b�g����
    /// </summary>
    /// <param name="pos">�A�N�e�B�u�ɂ�����W</param>
    public void ActiveHut(Vector3 pos)
    {
        if(_hutIndex >= _generateHutCount)
        {
            _hutIndex = 0;
        }
        _hutList[_hutIndex].SetActive(true);
        _hutList[_hutIndex].transform.position = pos;
        _hutList[_hutIndex].GetComponent<ObstacleMovement>().SpeedUp(_hutMoveSpeed);
        _hutIndex++;
        SoundManager.Instance.PlaySE(SoundDefine.SE.DOOR_KICK);
    }
    /// <summary>
    /// �y�g�����A�N�e�B�u�ɂ��č��W�Z�b�g
    /// </summary>
    /// <param name="pos">�A�N�e�B�u�ɂ�����W</param>
    public void ActiveTruck(Vector3 pos)
    {
        if(_truckIndex >= _generateTruckCount)
        {
            _truckIndex=0;
        }
        _truckList[_truckIndex].SetActive(true);
        _truckList[_truckIndex].transform.position=pos;
        _truckList[_truckIndex].GetComponent<ObstacleMovement>().SpeedUp(_lightTruckMoveSpeed);
        _truckIndex++;
        SoundManager.Instance.PlaySE(SoundDefine.SE.DUMP_TRUCK_IDLE);
    }
    /// <summary>
    /// �G���A�N�e�B�u�ɂ��č��W�Z�b�g
    /// </summary>
    /// <param name="pos"></param>
    public void ActiveEnemy(Vector3 pos)
    {
        if(_enemyIndex >= _generateEnemyCount)
        {
            _enemyIndex = 0;
        }
        _enemyList[_enemyIndex].SetActive(true);
        _enemyList[_enemyIndex].transform.position=pos;
        _enemyList[_enemyIndex].GetComponent<ObstacleMovement>().SpeedUp(_enemyMoveSpeed);
        _enemyIndex++;
        SoundManager.Instance.PlaySE(SoundDefine.SE.CHICKEN);
    }
    /// <summary>
    /// �I�u�W�F�N�g���\���ɂ��đҋ@������
    /// </summary>
    /// <param name="obj">��\���ɂ���I�u�W�F�N�g</param>
    public void DeActiveObj(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Obstacle":
                SoundManager.Instance.PlaySE(SoundDefine.SE.CHICKEN_CRY_1);
                break;

            case "Enemy":
                SoundManager.Instance.PlaySE(SoundDefine.SE.CHICKEN_CRY_3);
                break;
            default:
                SoundManager.Instance.PlaySE(SoundDefine.SE.EXPLOSION_1);
                break;
        }
        obj.SetActive(false);
    }

    /// <summary>
    /// �I�u�W�F�N�g�̈ړ����x��ύX����
    /// </summary>
    public void ChangeMoveSpeed()
    {
        _hutMoveSpeed += _changeMoveSpeedValue;
        _lightTruckMoveSpeed += _changeMoveSpeedValue;
        _enemyMoveSpeed += _changeMoveSpeedValue;
    }
    /// <summary>
    /// ��Q���̈ړ����\�b�h���Ăяo��
    /// </summary>
    public void MovementCall()
    {
        foreach(ObstacleMovement obj in _moveScriptList)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                continue;
            }
            obj.MoveProtocol();
        }
    }
}
