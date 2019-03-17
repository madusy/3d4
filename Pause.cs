using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameManagerBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Если нажата клавиша Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.paused = !gameManager.paused; // Меняем значение паузы
            bool paused = gameManager.paused; // Получаем новое значение паузы
            // Если игра ставится на паузу
            if (paused)
                Time.timeScale = 0; // Останавливаем время
            else
                Time.timeScale = 1; // Запускаем время
            gameManager.pauseMenu.SetActive(paused); // Показываем/скрываем меню паузы в зависимости от значения паузы
        }
    }
}
