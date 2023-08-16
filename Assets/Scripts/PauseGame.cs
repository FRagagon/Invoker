using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public GameObject pauseMenuUI; // 暂停界面的 Panel

    private bool isPaused = false;

    // Update is called once per frame
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // 暂停游戏时间
            pauseMenuUI.SetActive(true); // 显示暂停界面
        }
        else
        {
            Time.timeScale = 1; // 恢复游戏时间
            pauseMenuUI.SetActive(false); // 隐藏暂停界面
        }
    }
}
