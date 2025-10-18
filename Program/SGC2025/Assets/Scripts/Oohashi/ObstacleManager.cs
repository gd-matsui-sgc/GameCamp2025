using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObstacleManager : MonoBehaviour
{
    #region Serialize変数
    [SerializeField, Header("鶏小屋")]
    private GameObject _chikenHut = default;
    [SerializeField, Header("軽トラ")]
    private GameObject _lightTruck = default;
    [SerializeField, Header("小屋を生成する数")]
    private int _generateHutCount = 25;
    [SerializeField, Header("軽トラを生成する数")]
    private int _generateTruckCount = 25;

    #endregion

    #region 変数
    private List<GameObject> _hutList = new List<GameObject>();
    private List<GameObject> _truckList = new List<GameObject>();

    private int _hutIndex = 0;
    private int _truckIndex = 0;
    #endregion

    /// <summary>
    /// 鳥小屋及び軽トラのプールを作成
    /// </summary>
    public void CreatePool()
    {
        GameObject obj = null;
        for(int i = 0; i < _generateHutCount; i++)
        {
            obj = Instantiate(_chikenHut);
            _hutList.Add(obj);
            obj.GetComponent<ObstacleMovement>().GetComponentProtocol();
            obj.SetActive(false);
        }
        for(int i = 0;i< _generateTruckCount; i++)
        {
            obj = Instantiate(_lightTruck);
            _truckList.Add(obj);
            obj.GetComponent<ObstacleMovement>().GetComponentProtocol();
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
        _hutIndex++;
    }
    /// <summary>
    /// 軽トラをアクティブにして座標セット
    /// </summary>
    /// <param name="pos">アクティブにする座標</param>
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
    /// オブジェクトを非表示にして待機させる
    /// </summary>
    /// <param name="obj">非表示にするオブジェクト</param>
    public void DeActiveObj(GameObject obj)
    {
        obj.SetActive(false);
    }
}
