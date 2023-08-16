using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }

    public void ToIntroduction()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void ToSkillList()
    {
        SceneManager.LoadScene("SkillList");
    }


}
