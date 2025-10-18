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

    // イベントシステム
    [SerializeField]
    protected Fade fade = null;

    /**
     * Updateの直前に呼ばれる(Unity側)
     */
    protected override void Awake()
    {
        Application.targetFrameRate = 60;
        base.Awake();
    }

    /**
     * Updateの直前に呼ばれる(Unity側)
     */
    protected override void Start()
    {
        // このコンポーネントが属するシーンを取得
        Scene currentScene = this.gameObject.scene;
        // シーン内のルートオブジェクトをすべて取得
        GameObject[] rootObjects = currentScene.GetRootGameObjects();

        // EventSystemがインスペクターで設定されていなければ、このシーン内から探す
        if (eventSystem == null)
        {

            // ルートオブジェクトとその子を再帰的に検索して EventSystem を探す
            foreach (GameObject root in rootObjects)
            {
                // GetComponentInChildren<T>(true) で非アクティブなオブジェクトも検索
                eventSystem = root.GetComponentInChildren<EventSystem>(true);
                if (eventSystem != null) break; // 見つかったらループを抜ける
            }
        }

        // イベントシステムは複数起動できないので１つにする
        if (eventSystem != null)
        {
            if (Work.eventSystem == null)
            {
                Work.eventSystem = eventSystem;
            }
            else
            {
                eventSystem.gameObject.SetActive(false);
            }
        }

        if (fade != null)
        {
            if (Work.fade == null)
            {
                Work.fade = fade;
            }
            else
            {
                fade.gameObject.SetActive(false);
            }
        }

        // このシーン内のAudioListenerを探す
        AudioListener sceneListener = null;
        foreach (GameObject root in rootObjects)
        {
            sceneListener = root.GetComponentInChildren<AudioListener>(true);
            if (sceneListener != null) break;
        }

        // オーディオリスナーは複数起動できないので１つにする
        if (sceneListener != null)
        {
            if (Work.mainAudioListener == null)
            {
                Work.mainAudioListener = sceneListener;
            }
            else if (Work.mainAudioListener != sceneListener)
            {
                // 既にアクティブなリスナーがあるので、このシーンのリスナーは無効化
                sceneListener.enabled = false;
            }
        }
        base.Start();
    }
}
