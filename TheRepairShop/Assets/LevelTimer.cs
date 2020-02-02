using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] Image timer;
    bool isTicking = false;
    float timeLeftRatio = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftRatio = GameManager.instance.levelTimer / GameManager.instance.levelLength;
        timer.fillAmount = 1- timeLeftRatio;
        if (timeLeftRatio < 0.2 && isTicking == false)
        {
            AudioPlayer.Instance.PlayAudioLoop("TimerTick");
            isTicking = true;
        }

        if (timeLeftRatio <= 0)
        {
            AudioPlayer.Instance.StopAudioLoop("TimerTick");
            AudioPlayer.Instance.PlayAudioOnce("TimerAlarm", 0.4f);
        }
    }
}
