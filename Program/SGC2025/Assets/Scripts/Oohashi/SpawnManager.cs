using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [SerializeField, Header("�}�l�[�W���[")]
    private ObstacleManager _manager = default;
    [SerializeField, Header("��Q���𐶐�����ꏊ")]
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
        Debug.Log("�N�������Q���̔ԍ�"+spawnObjIndex);
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
