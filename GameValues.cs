using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс содержит глобальные значения игры
/// </summary>
public class GameValues : MonoBehaviour
{
    /// <summary>
    /// Жизни
    /// </summary>
    public static int Lives { get; set; }
    /// <summary>
    /// Монеты
    /// </summary>
    public static int Money { get; set; }
    /// <summary>
    /// Счет
    /// </summary>
    public static int Score { get; set; }
    /// <summary>
    /// Волна
    /// </summary>
    public static int Wave { get; set; }

    /// <summary>
    /// Задание значений в начале игры
    /// </summary>
    public void Start()
    {
        Lives = 20;
        Money = 20;
        Score = 0;
        Wave = 1;
    }

}
