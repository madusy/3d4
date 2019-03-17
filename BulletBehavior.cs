using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10; // Скорость полета пули
    public int damage; // Наносимый урон
    public GameObject target; // Цель пули
    public Vector3 startPosition; // Начальная позиция пули
    public Vector3 targetPosition; // Конечная позиция пули

    private float distance; // Дистанция до цели
    private float startTime; // Время в момент выстрела

    private GameManagerBehavior gameManager;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Определяем интервал времени с момента выстрела
        float timeInterval = Time.time - startTime;
        // Движемся к цели
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // Если пуля достигла координат цели
        if (gameObject.transform.position.Equals(targetPosition))
        {
            // Если цель существует
            if (target != null)
            {
                var enemy = target.GetComponent<Enemy>(); // Получаем объект цели
                enemy.Health -= Mathf.Max(damage, 0); // Вычитаем у цели жизни
                // Если у цели закончились жизни
                if (enemy.Health <= 0)
                {
                    Destroy(target); // Уничтожаем цель
                    gameManager.Gold += 5; // Увеличиваем монеты игрока на 5
                    gameManager.Score += 10; // Увеличиваем счет игрока на 10
                }
            }
            Destroy(gameObject); // Уничтожаем объект цели
        }
    }
}
