using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Boot : BaseScene
{
    public enum Phase
    {
        Logo,
        Title,
        Game,
        Result,
    }

    // 変数
    [SerializeField]
    public Phase initPhase = Phase.Title;

    // ロゴ
    private Logo m_logo = null;

    // タイトル
    private Title m_title = null;

    // ゲーム
    private Game m_game = null;

    // リザルト
    private Result m_result = null;

    protected override void OnStart()
    {
        base.OnStart();
        SetPhase((int)initPhase);
    }


    protected override void OnUpdate()
    {
        switch ((Phase)GetPhase())
        {
            case Phase.Logo: _Logo(); break;
            case Phase.Title: _Title(); break;
            case Phase.Game: _Game(); break;
            case Phase.Result: _Result(); break;
        }
    }

    /**
     * ロゴ画面
     */
    protected void _Logo()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateLogo");
        }
        else if( m_logo != null)
        {
            if( m_logo.IsExited())
            {
                m_logo = null;
                SceneManager.UnloadSceneAsync("Logo");
                SetPhase(  ( int )Phase.Title );
            }
        }
    }

    /**
     * タイトル画面
     */
    protected void _Title()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateTitle");
        }
        else if( m_title != null)
        {
            if( m_title.IsExited())
            {
                m_title = null;
                SceneManager.UnloadSceneAsync("Title");
                SetPhase(  ( int )Phase.Game );
            }
        }
    }

    /**
     * ゲーム画面
     */
    protected void _Game()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateGame");
        }
        else if( m_game != null)
        {
            if( m_game.IsExited())
            {
                m_game = null;
                SceneManager.UnloadSceneAsync("Honpen");
                SetPhase(  ( int )Phase.Result );
            }
        }
    }

    /**
     * リザルト画面
     */
    protected void _Result()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateResult");
        }
        else if( m_result != null)
        {
            if(m_result.IsExited())
            {
                if(m_result.GetExitCode() == Result.EXIT_CODE_TITLE)
                {
                    SetPhase((int)Phase.Title);
                }
                else
                {
                    SetPhase((int)Phase.Game);
                }
                SceneManager.UnloadSceneAsync("Result");
                m_result = null;
            }
        }
    }

    /* -------------------------------------------------------------------------------------- */
    /* |コルーチン |
    /* -------------------------------------------------------------------------------------- */

    /**
     * ロゴ生成のコルーチン
     */
    IEnumerator CreateLogo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "logo", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject logo = GameObject.Find("Logo");
        m_logo = logo?.GetComponent<Logo>();
    }

    /**
     * タイトル生成のコルーチン
     */
    IEnumerator CreateTitle()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "title", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject title = GameObject.Find("Title");
        m_title = title?.GetComponent<Title>();
    }

    /**
     * ゲーム生成のコルーチン
     */
    IEnumerator CreateGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Honpen", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
             yield return null;
        }

        GameObject game = GameObject.Find("Game");
        m_game = game?.GetComponent<Game>();
    }

    /**
     * リザルト生成のコルーチン
     */
    IEnumerator CreateResult()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "result", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject result = GameObject.Find("Result");
        m_result = result?.GetComponent<Result>();
    }
}
