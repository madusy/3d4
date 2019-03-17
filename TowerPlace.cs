using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    public GameObject towerPrefab; // Префаб башни
    private GameObject tower; // Объект башни
    private GameManagerBehavior gameManager;
    private Renderer rend;
    public Material SelectedMaterial;
    private Material BaseMaterial;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        rend = GetComponent<Renderer>();
        BaseMaterial = rend.materials[0];
    }

    /// <summary>
    /// Проверка возможности установки башни
    /// </summary>
    private bool CanPlaceTower()
    {
        int cost = towerPrefab.GetComponent<TowerData>().levels[0].cost;
        return tower == null && gameManager.Gold >= cost;
    }

    /// <summary>
    /// Проверка возможности улучшения башни
    /// </summary>
    private bool CanUpgradeTower()
    {
        if (tower != null)
        {
            TowerData towerData = tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.getNextLevel(); // Получаем следующий уровень башни
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

    // При нажатии мыши
    void OnMouseDown()
    {
        // Если игра на паузе
        if (gameManager.paused)
            return; // Останавливаем функцию
        // Если игра окончена
        if (gameManager.gameOver)
        {
            enabled = false; // Останавливаем выполнение скрипта
            return; // Останавливаем функцию
        }
        // Если на данной платформе возможно поставить башню
        if (CanPlaceTower())
        {
            Vector3 Pos = transform.position;
            Pos.y = towerPrefab.transform.position.y;
            tower = Instantiate(towerPrefab, Pos, Quaternion.identity); // Создаем башню
            gameManager.Gold -= tower.GetComponent<TowerData>().CurrentLevel.cost; // Вычитаем у игрока стоимость башни
        }
        // Если башню поставить нельзя, но можно ее улучшить
        else if (CanUpgradeTower())
        {
            tower.GetComponent<TowerData>().increaseLevel(); // Повышаем уровень башни           
            gameManager.Gold -= tower.GetComponent<TowerData>().CurrentLevel.cost; // Вычитаем у игрока стоимость улучшения
        }
    }

    // При наведении мыши
    void OnMouseEnter()
    {
        // Если игра НЕ на паузе
        if (!gameManager.paused)
            rend.material = SelectedMaterial; // Меняем цвет платформы
    }

    // При отводе мыши
    void OnMouseExit()
    {
        // Если игра НЕ на паузе
        if (!gameManager.paused)
            rend.material = BaseMaterial; // Вызвращаем исходный цвет материала
    }
}
