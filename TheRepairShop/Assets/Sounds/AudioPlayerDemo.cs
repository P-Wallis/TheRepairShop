using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerDemo : MonoBehaviour
{
    int id = 0;
    float time1 = 5f;

    float time2 = 6f;

    float time3 = 9f;
    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer.Instance.PlayAudioOnce("GameStart");

        AudioPlayer.Instance.PlayAudioLoop("Paint");
    }

    // Update is called once per frame
    void Update()
    {
        time1 -= Time.deltaTime;

        time2 -= Time.deltaTime;

        time3 -= Time.deltaTime;
        if (time1 < 0)
        {
            AudioPlayer.Instance.StopAudioLoop("Paint");
            time1 = 1000;
        }
        if (time2 < 0)
        {
            id = AudioPlayer.Instance.PlayAudioLoop("AnvilHammering");
            time2 = 1000;
        }
        if (time3 < 0)
        {
            AudioPlayer.Instance.StopAudioLoop(id);
            time3 = 1000;
        }
    }
}
