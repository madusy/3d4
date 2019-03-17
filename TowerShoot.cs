using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    public List<GameObject> enemiesInRange; // Враги
    private float lastShotTime; // Время с момента последнего выстрела
    private TowerData towerData;
    public float range = 2f; // Радиус поражения башен
    private Transform target; // Цель для пули

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInChildren<TowerData>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Обновление информации о цели
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Находим все объекты врагов
        float shortestDistance = Mathf.Infinity;
        GameObject nearstEnemy = null;
        // Перебираем объекты врагов в цикле
        foreach (GameObject enemy in enemies)
        {
            // Вычисляем расстояние до врага
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // Если враг ближе чем предыдущие
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }
        // Если найденый враг в радиусе поражения
        if (nearstEnemy != null && shortestDistance <= range)
            target = nearstEnemy.transform; // Назначаем его в качестве цели
    }

    // Update is called once per frame
    void Update()
    {
        // Если цели нет
        if (target == null)
            return; // Останавливаем функцию
        // Если время с момента последнего выстрела превышает интервал между выстрелами
        if (Time.time - lastShotTime > towerData.CurrentLevel.fireRate)
        {
            Shoot(target); // Осуществляем выстрел
            lastShotTime = Time.time; // Обновляем время последнего выстрела
        }
    }

    // Событие при уничтожении противника
    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Выстрел по цели
    private void Shoot(Transform target)
    {
        GameObject bulletPrefab = towerData.CurrentLevel.bullet; // Выбираем префаб пули
        
        Vector3 startPosition = gameObject.transform.position; // Задаем начальные координаты пули
        Vector3 targetPosition = target.position; // Задаем координаты цели

        GameObject newBullet = Instantiate(bulletPrefab); // Создаем объект пули
        newBullet.transform.position = startPosition; // Задаем начальные координаты пули
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>(); // Получаем скрипт пули
        bulletComp.target = target.gameObject; // Задаем цель пули
        bulletComp.startPosition = startPosition; // Задаем начальные координаты пули в скрипте
        bulletComp.targetPosition = targetPosition; // Задаем координаты цели
    }
}
