using UnityEngine;
using System.Collections.Generic;
public class Tester : MonoBehaviour
{
    //�e�X�g�p�̃X�N���v�g�A�ŏI�I�ɂ͏���
    [SerializeField, Header("�}�l�[�W���[")]
    private ObstacleManager _manager = default;
    [SerializeField, Header("��Q���𐶐�����ꏊ")]
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
