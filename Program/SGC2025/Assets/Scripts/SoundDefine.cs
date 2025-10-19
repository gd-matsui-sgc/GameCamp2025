using System.Collections.Generic;

/// <summary>
/// サウンド関連の定義を管理する静的クラス
/// </summary>
public static class SoundDefine
{
    /// <summary>
    /// BGMの種類
    /// </summary>
    public enum BGM
    {
        None,
        TITLE,
        GAME,
        RESULT,
    }

    /// <summary>
    /// SEの種類
    /// </summary>
    public enum SE
    {
        None,
        DOOR_KICK,			// ドアを蹴破る
        CHICKEN,			// ニワトリ
        CHICKEN_CRY_1,		// ニワトリの鳴き声1
        CHICKEN_CRY_2,		// ニワトリの鳴き声2
        CHICKEN_CRY_3,		// ニワトリの鳴き声3
        TSUKKOMI_HIT_1,		// ビシッとツッコミ1
        BOING,				// ボヨン
        SHOW_AMOUNT,		// 金額表示
        CONFIRM_16,			// 決定16
        UNLOCK_1,			// 鍵を開ける1
        MEN_START_SHOUT,	// 男衆「始めいッ！」
        EXPLOSION_1,		// 爆発1
        JAIL_DOOR_CLOSE,	// 牢屋の扉を閉める
        STADIUM_CROWD,		// スタジアムのざわめき
        DUMP_TRUCK_IDLE,    // ダンプカーのアイドリング
    }

    // BGMのファイルパス辞書
    public static readonly Dictionary<BGM, string> BgmPath = new Dictionary<BGM, string>
    {
        { BGM.TITLE,    "title"     },
        { BGM.GAME,     "game"      },
        { BGM.RESULT,   "result"    },
    };

    // SEのファイルパス辞書
    public static readonly Dictionary<SE, string> SePath = new Dictionary<SE, string>
    {
        { SE.DOOR_KICK ,        "se_01" }, // ドアを蹴破る
        { SE.CHICKEN ,          "se_02" }, // ニワトリ
        { SE.CHICKEN_CRY_1 ,    "se_03" }, // ニワトリの鳴き声1
        { SE.CHICKEN_CRY_2 ,    "se_04" }, // ニワトリの鳴き声2
        { SE.CHICKEN_CRY_3 ,    "se_05" }, // ニワトリの鳴き声3
        { SE.TSUKKOMI_HIT_1 ,   "se_06" }, // ビシッとツッコミ1
        { SE.BOING ,            "se_07" }, // ボヨン
        { SE.SHOW_AMOUNT ,      "se_08" }, // 金額表示
        { SE.CONFIRM_16 ,       "se_09" }, // 決定16
        { SE.UNLOCK_1 ,         "se_10" }, // 鍵を開ける1
        { SE.MEN_START_SHOUT ,  "se_11" }, // 男衆「始めいッ！」
        { SE.EXPLOSION_1 ,      "se_12" }, // 爆発1
        { SE.JAIL_DOOR_CLOSE ,  "se_13" }, // 牢屋の扉を閉める
        { SE.STADIUM_CROWD ,    "se_14" }, // スタジアムのざわめき
        { SE.DUMP_TRUCK_IDLE ,  "se_15" }, // ダンプカーのアイドリング
    };
}
