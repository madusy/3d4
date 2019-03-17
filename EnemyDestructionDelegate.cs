using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionDelegate : MonoBehaviour
{
    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;

    /// <summary>
    /// Уничтожение противника
    /// </summary>
    void OnDestroy()
    {
        enemyDelegate?.Invoke(gameObject);
    }
}
