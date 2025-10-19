using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    #region Serialize変数
    [SerializeField, Header("鶏小屋")]
    private GameObject _chikenHut = default;
    [SerializeField, Header("軽トラ")]
    private GameObject _lightTruck = default;
    [SerializeField, Header("敵")]
    private GameObject _enemy = default;
    [SerializeField, Header("小屋を生成する数")]
    private int _generateHutCount = 25;
    [SerializeField, Header("軽トラを生成する数")]
    private int _generateTruckCount = 25;
    [SerializeField,Header("敵を生成する数")]
    private int _generateEnemyCount = 25;

    [SerializeField, Header("鳥小屋を移動させる初期速度(マイナス方向)")]
    private int _hutMoveSpeed = -10;
    [SerializeField, Header("軽トラを移動させる初期速度")]
    private int _lightTruckMoveSpeed = -20;
    [SerializeField, Header("敵を移動させる初期速度")]
    private int _enemyMoveSpeed = -15;

    [SerializeField, Header("一回の速度変更でどれくらい速度を変えるか")]
    private int _changeMoveSpeedValue = -10;

    #endregion

    #region 変数
    private List<GameObject> _hutList = new List<GameObject>();
    private List<GameObject> _truckList = new List<GameObject>();
    private List<GameObject> _enemyList = new List<GameObject>();
    private List<ObstacleMovement> _moveScriptList = new List<ObstacleMovement>();

    private int _hutIndex = 0;
    private int _truckIndex = 0;
    private int _enemyIndex = 0;
    #endregion

    /// <summary>
    /// 鳥小屋、軽トラ、敵のプールを作成
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
    /// 鳥小屋をアクティブにして座標をセットする
    /// </summary>
    /// <param name="pos">アクティブにする座標</param>
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
    /// 軽トラをアクティブにして座標セット
    /// </summary>
    /// <param name="pos">アクティブにする座標</param>
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
    /// 敵をアクティブにして座標セット
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
    /// オブジェクトを非表示にして待機させる
    /// </summary>
    /// <param name="obj">非表示にするオブジェクト</param>
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
    /// オブジェクトの移動速度を変更する
    /// </summary>
    public void ChangeMoveSpeed()
    {
        _hutMoveSpeed += _changeMoveSpeedValue;
        _lightTruckMoveSpeed += _changeMoveSpeedValue;
        _enemyMoveSpeed += _changeMoveSpeedValue;
    }
    /// <summary>
    /// 障害物の移動メソッドを呼び出し
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
