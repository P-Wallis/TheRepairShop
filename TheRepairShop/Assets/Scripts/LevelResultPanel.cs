using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelResultPanel : MonoBehaviour
{
    [SerializeField] Text ReceivedNumText;

    [SerializeField] Text SuccessNumText;
    [SerializeField] GameObject ChildPanel;

    public event Action OnNextLevel;

    [SerializeField] Button ReplayButton;
    [SerializeField] Button NextLevelButton;

    public static LevelResultPanel Instance => GameObject.Find("LevelResultPanel").GetComponent<LevelResultPanel>();
    // Start is called before the first frame update
    void Start()
    {
        ChildPanel.SetActive(false);
        NextLevelButton.onClick.AddListener(OnNextLevelButton);

        ReplayButton.onClick.AddListener(OnReplayButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelEnd(int Recieved, int Success) {
        ReceivedNumText.text = Recieved.ToString();
        SuccessNumText.text = Success.ToString();
        ChildPanel.SetActive(true);
    
    }
    public void OnNextLevelButton() 
    
    {
        AudioPlayer.Instance.StopAudioAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //There is no such a thing as the next level
        //OnNextLevel?.Invoke();

    }

    public void OnReplayButton()

    {
        AudioPlayer.Instance.StopAudioAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
