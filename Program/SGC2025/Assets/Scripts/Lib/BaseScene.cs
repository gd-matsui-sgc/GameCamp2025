using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/**
 * シーン用ベースクラス
 */
public class BaseScene : Base
{
    // イベントシステム
    [SerializeField]
    protected EventSystem eventSystem = null;

    /**
     * Updateの直前に呼ばれる(Unity側)
     */
    protected override void Start()
    {
        // イベントシステムは複数起動できないので１つにする
        if( eventSystem != null )
        {
            if( Work.eventSystem == null )
            {
                Work.eventSystem = eventSystem;
            }
            else
            {
                eventSystem.gameObject.SetActive( false );
            }
        }

        base.Start();
    }
}
