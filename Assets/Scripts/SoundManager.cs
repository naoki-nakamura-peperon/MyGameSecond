//BGM関連
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //AudioSource:スピーカー
    [SerializeField] AudioSource audioSourceBGM = default;
    //AudioClip:CD，カセットテープ
    [SerializeField] AudioClip audioClip = default;
    private void Start()
    {
        PlayBGM();
    }
    public void PlayBGM()
    {
        audioSourceBGM.clip = audioClip;
        audioSourceBGM.Play();
    }
}
