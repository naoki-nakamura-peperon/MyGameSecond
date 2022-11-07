//Ball消去時のスコア表示
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointEffect : MonoBehaviour
{
    //同時にBallを消したことで得るScoreに応じて表示を変更
    
    [SerializeField] Text text = default;
    public void Show(int score)
    {
        text.text = score.ToString();
        //コルーチンを使う際に必要
        StartCoroutine(MoveUp());
    }
    //ScoreTextを上に上げる
    IEnumerator MoveUp()
    {
        // transform は一瞬で位置を変える
        //コルーチンを用いるとパラパラ漫画のようなコンマ単位の移動が可能
        //
        for(int i = 0; i < 20; i++)
        {
            //0.02秒経つと0.3上げる
            yield return new WaitForSeconds(0.02f);
            //yield return null;
            transform.Translate(0, 0.3f, 0);
        }
        //0.2秒後消える
        Destroy(gameObject, 0.2f);
    }
}
