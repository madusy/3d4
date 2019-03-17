using UnityEngine;

public class PathMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;  // скорость движения
    [SerializeField] public GameObject[] myWaypoints;
    private float YCoord;
    private Vector3 myWaypointCoord;

    private int myWaypointId = 0; // текущая точка в массиве куда двигаться

    void Start()
    {
        YCoord = gameObject.transform.position.y;
    }

    void EnemyMovement()
    {
        // если точки есть
        if (myWaypoints.Length != 0)
        {
            myWaypointCoord = myWaypoints[myWaypointId].transform.position;
            myWaypointCoord.y = YCoord;
            // если мы уже достигли назначенной точки, то переходим к следующей
            if (Vector3.Distance(myWaypointCoord, transform.position) <= 0)
            {
                myWaypointId++;           
            }

            //если точки исчерпаны то переходим к началу массива точек
            if (myWaypointId >= myWaypoints.Length)
            {
                Destroy(gameObject);
            }

            //движемся в назначенную точку
            transform.position = Vector3.MoveTowards(transform.position, myWaypointCoord, moveSpeed * Time.deltaTime);
            transform.LookAt(myWaypointCoord);
        }
    }

    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    /// <summary>
    /// Вычисление дистанции до конечной точки
    /// </summary>
    /// <returns>Дистанция до конечной точки</returns>
    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector3.Distance(
            gameObject.transform.position,
            myWaypoints[myWaypointId + 1].transform.position);
        for (int i = myWaypointId + 1; i < myWaypoints.Length - 1; i++)
        {
            Vector3 startPosition = myWaypoints[i].transform.position;
            Vector3 endPosition = myWaypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }
}
