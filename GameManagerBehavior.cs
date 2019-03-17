using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Префабы врагов
    public GameObject[] waypoints; // Точки маршрута движения врагов
    public GameObject pauseMenu; // Меню паузы
    public GameObject CanvasGameOver; // Canvas, появляющийся при завершении игры
    public GameObject PanelVictory; // Панель, появляющаяся при победе
    public GameObject PanelLoses; // Панель, появляющаяся при поражении
    public Text textMoney; // Текст для отображения значения монет
    public Text textWave; // Текст для отображения значения волн
    public Text textLives; // Текст для отображения значения жизней у крепости
    public Text textScore; // Текст для отображения счета икрока
    public bool gameOver = false; // Закончена ли игра
    public bool paused = false; // Остановить ли игру на паузе
    private int MaxEnemy; // Максимальное количество врагов
    private float MoveSpeed { get; set; } // Скорость движения врагов
    private float EnemyHealth { get; set; } // Количество жизней у врагов

    // Монеты игрока
    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            textMoney.text = value.ToString();
        }
    }

    // Максимальное количество волн
    private int maxWave;
    public int MaxWave { get { return maxWave; } set { maxWave = value - 1; } }
    
    // Текущая волна
    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            textWave.text = (value + 1).ToString();
            Score += 20;
        }
    }

    // Количество жизней у крепости
    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            textLives.text = value.ToString();
            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                CanvasGameOver.SetActive(true);
                PanelLoses.SetActive(true);
                Text textGameOver = GameObject.Find("textGameOver").GetComponent<Text>();
                textGameOver.text = "Поражение!";
                textGameOver.color = Color.red;
                GetComponent<AudioSource>().pitch = 0.5f;
            }
        }
    }
    
    // Счет игрока
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            textScore.text = value.ToString();
        }
    }    
  
    /// <summary>
    /// Создание новой волны
    /// </summary>
    /// <returns>Новая волна</returns>
    public Wave CreateWave()
    {
        Wave wave = new Wave();
        wave.maxEnemies = ++MaxEnemy;
        wave.enemyPrefab = enemyPrefabs[0]; //Выбор префаба врагов текущей волны
        wave.enemyHealth = EnemyHealth;
        EnemyHealth += 2f;
        var pathmove = wave.enemyPrefab.GetComponent<PathMove>();
        pathmove.myWaypoints = waypoints;
        pathmove.moveSpeed = MoveSpeed;
        return wave;
    }

    // Use this for initialization
    void Start()
    {
        Gold = 20;
        Wave = 0;
        Health = 20;
        Score = 0;
        MaxWave = 15;
        MoveSpeed = 2f;
        EnemyHealth = 10f;
        MaxEnemy = 4;
        pauseMenu.SetActive(false);
        CanvasGameOver.SetActive(false);
        PanelLoses.SetActive(false);
        PanelVictory.SetActive(false);
        Time.timeScale = 1;
    }

}
