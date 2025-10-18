using UnityEngine;

public class DetectiveObject : MonoBehaviour
{
    [SerializeField, Header("��Q���̃}�l�[�W���[")]
    private ObstacleManager _obstacleManager = default;
    private void OnTriggerEnter(Collider collision)
    {
        _obstacleManager.DeActiveObj(collision.gameObject);
    }
}
