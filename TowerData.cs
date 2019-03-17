using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс уровней башен
/// </summary>
[System.Serializable]
public class TowerLevel
{
    public int cost; // Стоимость башни
    public GameObject visualization; // Префаб башни
    public GameObject bullet; // Префаб пули
    public float fireRate; // Интервал между выстрелами
}

public class TowerData : MonoBehaviour
{
    public List<TowerLevel> levels; // Список уровней башен
    private TowerLevel currentLevel; // Текущий уровень башни

    public TowerLevel CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel); // Получаем индекс текущего уровня

            GameObject levelVisualization = levels[currentLevelIndex].visualization; // Получаем префаб текущего уровня башни
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true); // Выводим префаб текущего уровня башни
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false); // Скрываем префабы остальных уровней башни
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0]; // Получаем первый уровень башни
    }

    /// <summary>
    /// Получение следующего уровня башни
    /// </summary>
    /// <returns>Следующий уровень башни</returns>
    public TowerLevel getNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Повышение уровня башни
    /// </summary>
    public void increaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }

}
