using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private Transform[] _patrolPoints;


    private int _destPoint = 0;
    private NavMeshAgent _agent;
    private LineRenderer _lineRenderer;
    private Enemy _enemy;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();
        _enemy = GetComponent<Enemy>();

        _agent.speed = _moveSpeed;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.autoBraking = false;

        GotoNextPoint();
    }

    private void FixedUpdate()
    {
        if(_enemy.PlayerDetected)
            AgentMovement();
        else
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                AgentPatrol();
            AgentRotation();
        }
    }

    //  отвечает за движение врага в сторону игрока
    private void AgentMovement()
    {
        _agent.SetDestination(new Vector3(_enemy.Player.position.x, _enemy.Player.position.y, _agent.transform.position.z));
        DrawLine();
        AgentRotation();
    }

     // отвечает за движение врага по маршруту через точки
    private void AgentPatrol()
    {
        GotoNextPoint();
        DrawLine();
        AgentRotation();
    }

    // поворачивает врага в сторону которую он движеться
    private void AgentRotation()
    {
        Vector3 myLocation = transform.position;
        Vector3 lookNavMeshNextCorner;
        if (_agent.path.corners.Length > 0)
            lookNavMeshNextCorner = _agent.path.corners[1];
        else
            lookNavMeshNextCorner = _agent.path.corners[0];
        lookNavMeshNextCorner.z = myLocation.z;
        
        Vector3 vectorToTarget = lookNavMeshNextCorner - myLocation;
        
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed);
    }

    //Патрулирование врага
    private void GotoNextPoint()
    {
        // Если нет точек перемещения то враг стойт на месте
        if (_patrolPoints.Length == 0)
            return;

        // отправляет врага патрулировать на след точку
        _agent.destination = _patrolPoints[_destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        _destPoint = (_destPoint + 1) % _patrolPoints.Length;
    }


    //отрисовывает линию передвижения врага
    private void DrawLine()
    {
        _lineRenderer.positionCount = _agent.path.corners.Length;
        _lineRenderer.SetPositions(_agent.path.corners);
    }
}
