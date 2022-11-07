//Ball生成関連
using System.Collections;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{    
    [SerializeField] GameObject ballPrefab = default;
    //Ballの画像
    [SerializeField] Sprite[] ballSprites = default;
    //ボムの画像
    [SerializeField] Sprite bombSprite = default;
    public IEnumerator Spawns(int count)
   {
        for (int i = 0; i < count; i++)
        {
            //Ballを左右にランダムに生成
            Vector2 pos = new Vector2(Random.Range(-0.2f, 0.2f), 8f);
            //Instantiate = オブジェクトを生成する関数(BallのPrefabを上記のランダムなポジションに回転させずに生成する)
            //Quaternion.Identityは回転における初期の値
            GameObject ball =　Instantiate(ballPrefab,pos,Quaternion.identity);
            //画像の設定
            //Ball Spritesの0,1,2,3,4の内からランダムで生成:-1はボム
            int ballID = Random.Range(0, ballSprites.Length);
            //もしボムならば ballID = -1
            if (Random.Range(0, 100) < 7) //7％の確立でtrue(ボム)
            {
                ballID = -1;
                ball.GetComponent<SpriteRenderer>().sprite = bombSprite;
            }
            //Ball ならば ballID = 0∼4
            else
            {
                ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballID];
            }            
            ball.GetComponent<Ball>().id = ballID;
            //0.04秒 Ball の再生を待機させる
            yield return new WaitForSeconds(0.04f);
        }        
   }
}
