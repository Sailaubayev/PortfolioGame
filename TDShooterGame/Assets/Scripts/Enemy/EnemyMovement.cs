using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _turnSpeed = 10f;

    private int _destPoint = 0;
    private LineRenderer _lineRenderer;
    private Transform _thisTransform;

    public NavMeshAgent Agent { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();
        _thisTransform = GetComponent<Transform>();
       // _enemy = GetComponent<Enemy>();

        Agent.speed = _moveSpeed;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Agent.autoBraking = false;

        GotoNextPoint();
    }

    //  отвечает за движение врага в сторону игрока
    public void AgentMove(Transform player)
    {
        Agent.SetDestination(new Vector3(player.position.x, player.position.y, Agent.transform.position.z));
        DrawLine();
    }

    // отвечает за движение врага по маршруту через точки
    public void AgentPatrol()
    {
        GotoNextPoint();
        DrawLine();
        Debug.Log("GoNextPoint");
    }

    // поворачивает врага в сторону которую он движеться
    public void AgentRotation()
    {
        Vector3 myLocation = _thisTransform.position;
        Vector3 lookNavMeshNextCorner;
        if (Agent.path.corners.Length > 0)
            lookNavMeshNextCorner = Agent.path.corners[1];
        else
            lookNavMeshNextCorner = Agent.path.corners[0];
        lookNavMeshNextCorner.z = myLocation.z;
        
        Vector3 vectorToTarget = lookNavMeshNextCorner - myLocation;
        
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        _thisTransform.rotation = Quaternion.RotateTowards(_thisTransform.rotation, targetRotation, _turnSpeed);
    }

    //Патрулирование врага
    private void GotoNextPoint()
    {
        // Если нет точек перемещения то враг стойт на месте
        if (_patrolPoints.Length == 0)
            return;

        // отправляет врага патрулировать на след точку
        Agent.destination = _patrolPoints[_destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        _destPoint = (_destPoint + 1) % _patrolPoints.Length;
    }


    //отрисовывает линию передвижения врага
    private void DrawLine()
    {
        _lineRenderer.positionCount = Agent.path.corners.Length;
        _lineRenderer.SetPositions(Agent.path.corners);
    }
}
