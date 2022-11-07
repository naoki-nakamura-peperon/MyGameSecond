//タイトルに戻る
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("title");
    }
}
