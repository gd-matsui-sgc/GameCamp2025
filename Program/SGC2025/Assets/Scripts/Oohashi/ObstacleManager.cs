using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObstacleManager : MonoBehaviour
{
    #region Serialize�ϐ�
    [SerializeField, Header("�{����")]
    private GameObject _chikenHut = default;
    [SerializeField, Header("�y�g��")]
    private GameObject _lightTruck = default;
    [SerializeField, Header("�����𐶐����鐔")]
    private int _generateHutCount = 25;
    [SerializeField, Header("�y�g���𐶐����鐔")]
    private int _generateTruckCount = 25;

    #endregion

    #region �ϐ�
    private List<GameObject> _hutList = new List<GameObject>();
    private List<GameObject> _truckList = new List<GameObject>();
    private List<ObstacleMovement> _moveScriptList = new List<ObstacleMovement>();

    private int _hutIndex = 0;
    private int _truckIndex = 0;
    #endregion

    /// <summary>
    /// �������y�ьy�g���̃v�[�����쐬
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
        _hutIndex++;
    }
    /// <summary>
    /// �y�g�����A�N�e�B�u�ɂ��č��W�Z�b�g
    /// </summary>
    /// <param name="pos">�A�N�e�B�u�ɂ�����W</param>
    public void ActiveTruck(Vector3 pos)
    {
        if(_truckIndex >= _generateTruckCount)
        {
            _truckIndex--;
        }
        _truckList[_truckIndex].SetActive(true);
        _truckList[_truckIndex].transform.position=pos;
        _truckIndex++;
    }
    /// <summary>
    /// �I�u�W�F�N�g���\���ɂ��đҋ@������
    /// </summary>
    /// <param name="obj">��\���ɂ���I�u�W�F�N�g</param>
    public void DeActiveObj(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void MovementCall()
    {
        foreach(ObstacleMovement obj in _moveScriptList)
        {
            if (!obj.isActiveAndEnabled)
            {
                return;
            }
            obj.MoveProtocol();
        }
    }
}
