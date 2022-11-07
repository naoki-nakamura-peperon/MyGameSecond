//パラメータテーブル

//・unity内の同じインスペクターから数値を設定できる
//・unityを扱いなれていない人、チーム制作でパラメータの細かい調整をしたい場合には有効
//・unityで制作することに慣れている人、1人で制作していてどこに何のパラメータがあるか分かっている人には不要
//・スクリプト内のコードに保存場所が直で書かれているため、保存場所を変えたり紛失すると機能が不可能になる
using UnityEngine;

[CreateAssetMenu]
//パラメータを管理するスクリプタブルオブジェクト
//変数をパブリックで用意する
public class ParamsSO : ScriptableObject
{
    [Header("初期のBallの数")]
    public int initBallCount;
    [Header("Ballを消した時の得点")]
    public int scorePoint;
    [Header("Ballを消せる判定距離")]
    public float ballDistance;
    [Header("bombの範囲")]
    //メモリを作成することも可能
    [Range(0,10)]
    public float bombRange;

    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ParamsSO";

    //MyScriptableObjectの実体
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                //Resouurcesフォルダからデータを持ってくる
                _entity = Resources.Load<ParamsSO>(PATH);
                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }
            return _entity;
        }
    }
}
