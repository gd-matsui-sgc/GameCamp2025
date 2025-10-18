using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [SerializeField, Header("マネージャー")]
    private ObstacleManager _manager = default;
    [SerializeField, Header("障害物を生成する場所")]
    private List<Vector3> _obstacleSpawnPoint = new List<Vector3>();

    private int _spawnIndex = 0;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            RandomSpawn();
        }
    }

    public void RandomSpawn()
    {
        int spawnIndex = Random.Range(0, _obstacleSpawnPoint.Count);
        Vector3 pos = _obstacleSpawnPoint[spawnIndex];
        int spawnObjIndex = Random.Range(0, 3);
        Debug.Log("湧かせる障害物の番号"+spawnObjIndex);
        switch (spawnObjIndex)
        {
            case 0:
                _manager.ActiveHut(pos);
                break;  
            case 1:
                _manager.ActiveTruck(pos);
                break;
            default:
                _manager.ActiveEnemy(pos);
                break;
        }
    }
}
