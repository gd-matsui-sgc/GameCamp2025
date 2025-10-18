using UnityEngine;

public class Game : BaseScene
{
    [SerializeField, Header("障害物のマネージャー")]
    private ObstacleManager _obstacleManager = default;
    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected override void OnAwake()
    {
        _obstacleManager.CreatePool();
    }

    /**
     * Updateの直前に呼ばれる
     */
    protected override void OnStart()
    {
    }

    /**
     * 毎フレーム呼ばれる
     */
    protected override void OnUpdate()
    {
        _obstacleManager.MovementCall();
    }

    /**
     * 破棄時に呼ばれる
     */
    protected override void OnDestroy()
    {

    }

}
