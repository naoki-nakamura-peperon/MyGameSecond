//スコア関連
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    public Text ScoreText;
    int score;
    public Text highScoreText;
    private int highScore; //ハイスコア用変数
    private string key = "HIGH SCORE"; //ハイスコアの保存先キー
    // Start is called before the first frame update
    void Start()
    {
        score = GameSystem.Getscore();
        //スコアを表示
        ScoreText.text = string.Format("Score:" + score);
        //保存しておいたハイスコアをキーで呼び出し取得し保存されていなければ0になる
        highScore = PlayerPrefs.GetInt(key, 0);
        //ハイスコアを表示
        highScoreText.text = "HighScore: " + highScore.ToString();
        //ハイスコアをリセット
        //PlayerPrefs.DeleteAll();
    }
    private void Update()
    {
        //ハイスコアより現在スコアが高い時
        if (score > highScore)
        {
            //ハイスコア更新
            highScore = score;
            //ハイスコアを保存
            PlayerPrefs.SetInt(key, highScore);
            //ハイスコアを表示
            highScoreText.text = "HighScore: " + highScore.ToString();            
        }
    }
}
