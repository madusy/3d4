using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс волны
/// </summary>
[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 0.5f;
    public int maxEnemies = 4;
    public float enemyHealth = 40f;
}

public class SpawnEnemy : MonoBehaviour
{
    public List<Wave> waves; // Волны врагов
    public int timeBetweenWaves = 5; // Время между волнами

    private GameManagerBehavior gameManager;

    private float lastSpawnTime; // Время последнего создания врага
    private int enemiesSpawned = 0; // Количество созданных противников
    private Vector3 enemyPos; // Координаты противника

    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        enemyPos = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        waves.Add(gameManager.CreateWave()); // Создаем новую волну
        enemyPos.y = waves[waves.Count - 1].enemyPrefab.transform.position.y; // Вычисляем координату высоты противника
    }

    // Update is called once per frame
    void Update()
    {
        // Если игра окончена
        if (gameManager.gameOver)
            enabled = false; // Отключем выполнение скрипта
        int currentWave = gameManager.Wave; // Получаем значение текущей волны
        if (currentWave < gameManager.MaxWave) // Если текущая волна не последняя
        {
            float timeInterval = Time.time - lastSpawnTime; // Вычисляем время с последнего создания врага
            float spawnInterval = waves[currentWave].spawnInterval; // Определяем интервал между созданием врагов
            /* Если не создано ни одного врага и интервал между волнами пройден 
             *  или если превышен интервал между созданием врагов,
             *   а также если не превышено максимальное количество врагов */
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                lastSpawnTime = Time.time; // Обновляем время последнего создания врага
                GameObject newEnemy = Instantiate(waves[currentWave].enemyPrefab); // Создаем объект врага
                newEnemy.transform.position = enemyPos; // Задаем ему координаты
                var enemy = newEnemy.GetComponent<Enemy>(); // Обращаемся к скрипту врага
                enemy.MaxHealth = waves[currentWave].enemyHealth; // Задаем в скрипте врага максимальное количество жизней
                enemiesSpawned++; // Увеличиваем счетчик врагов на 1
            }
            // Если достигнто максимальное количество врагов
            if (enemiesSpawned == waves[currentWave].maxEnemies)
            {
                // Если все враги уничтожены
                if (GameObject.FindGameObjectWithTag("Enemy") != null)
                {
                    lastSpawnTime = Time.time; // Обновляем время последнего создания врага
                    return; // Останавливаем функцию
                }
                timeInterval = Time.time - lastSpawnTime + 1; // Вычисляем время прошедшее с последнего создания врага
                if (timeInterval < timeBetweenWaves) // Если это время меньше, чем интервал между волнами
                    return; // Останавливаем функцию
                gameManager.Wave++; // Увеличиваем счетчик волн
                waves.Add(gameManager.CreateWave()); // Создаем новую волну
                gameManager.Gold += 5; // Увеличиваем количество монет на 5
                enemiesSpawned = 0; // Обнуляем счетчик врагов
                lastSpawnTime = Time.time; // Обновляем время последнего создания врага
            }
        }
        // Если волна последняя
        else
        {
            // Останавливаем игру
            gameManager.gameOver = true;
            // Выводим сообщение о победе
            gameManager.CanvasGameOver.SetActive(true);
            gameManager.PanelVictory.SetActive(true);
            Text textGameOver = GameObject.Find("textGameOver").GetComponent<Text>();
            textGameOver.text = "Победа!";
            textGameOver.color = new Color(248, 244, 127);
        }
    }
}
