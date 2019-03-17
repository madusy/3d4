using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Запуск новой игры
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    // Выход из игры
    public void Exit()
    {
        Application.Quit();
    }
}
