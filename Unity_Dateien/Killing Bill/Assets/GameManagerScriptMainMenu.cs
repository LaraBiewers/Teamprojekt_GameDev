using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScriptMainMenu : MonoBehaviour
{
    public void developmentScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Development");
    }
}
