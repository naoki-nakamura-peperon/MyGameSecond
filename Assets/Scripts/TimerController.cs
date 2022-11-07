//タイマー関連
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public Text timerText;
    public float totalTime;
    int seconds;

    // Update is called once per frame
    void Update()
    {
        //直前のフレームと今のフレーム間で経過した時間[秒] = Time.deltaTime
        //1フレームは0.何秒の世界であるため、floatを用いる
        totalTime -= Time.deltaTime;
        //制限時間は小数単位以下不要
        seconds = (int)totalTime;
        timerText.text = seconds.ToString();
        if(seconds == 0)
        {
            SceneManager.LoadScene("result");
        }
    }
}
