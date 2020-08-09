using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TitleScript : MonoBehaviour
{
    public void GoToGameplay()
    {
        SceneManager.LoadScene("SceneName");
     }
}
