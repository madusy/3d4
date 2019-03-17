using UnityEngine;

public class Castle : MonoBehaviour
{
    public GameManagerBehavior gameManager;

    // Проверка столкновения с врагом
    void OnTriggerEnter(Collider other)
    {
        // Если игра окончена
        if (gameManager.gameOver)
        {
            // Отключем выполнение скрипта
            enabled = false;
            return;
        }
        if (other.gameObject.tag.Equals("Enemy"))
        {
            gameManager.Health--; // Отнимаем 1 жизнь
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }
}
