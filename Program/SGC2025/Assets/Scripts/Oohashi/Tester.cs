using UnityEngine;
using System.Collections.Generic;
public class Tester : MonoBehaviour
{
    //テスト用のスクリプト、最終的には消す
    [SerializeField, Header("マネージャー")]
    private ObstacleManager _manager = default;
    [SerializeField, Header("障害物を生成する場所")]
    private List<Vector3> _obstacleSpawnPoint = new List<Vector3>();

    private int _spawnIndex = 0;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(_spawnIndex >= _obstacleSpawnPoint.Count)
            {
                _spawnIndex = 0;
            }
            Vector3 test = _obstacleSpawnPoint[_spawnIndex];
            _manager.ActiveHut(test);
            _spawnIndex++;
        }
    }
}
