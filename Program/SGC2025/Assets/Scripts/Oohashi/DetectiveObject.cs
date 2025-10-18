using UnityEngine;

public class DetectiveObject : MonoBehaviour
{
    [SerializeField, Header("障害物のマネージャー")]
    private ObstacleManager _obstacleManager = default;
    private void OnTriggerEnter(Collider collision)
    {
        _obstacleManager.DeActiveObj(collision.gameObject);
    }
}
