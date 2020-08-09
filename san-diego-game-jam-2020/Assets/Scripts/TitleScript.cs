using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{

    public Button startButton;

    // Use this for initialization
    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        SceneManager.LoadScene("Intro_Level", LoadSceneMode.Single);
     }
}
