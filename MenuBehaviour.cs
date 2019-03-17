using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    private GameManagerBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Возобновление игры
    public void Continue()
    {
        gameManager.paused = false;
        gameManager.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Выход в главное меню
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    // Перезапуск уровня
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}
