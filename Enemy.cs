using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Максимальное значение жизней врага
    private float maxHealth { get; set; }
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            Health = value;
        }
    }
    // Текущее количество жизней игрока
    public float Health { get; set; }
}
