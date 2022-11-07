using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private AudioSource seAudio;
    public AudioClip sound01;

    void Start()
    {
        seAudio = gameObject.AddComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            seAudio.PlayOneShot(sound01);
        }
    }
}
