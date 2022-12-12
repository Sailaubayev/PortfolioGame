using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private float _moveSpeed = 2.5f; 
    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] private bool _planting = false; // идет посадка
    [SerializeField] private List<Transform> _wayPoints; // точки перемещения

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _navMeshAgent.speed = _moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!_planting) // если не идет посадка
        {
            if (_wayPoints.Count > 0) // если есть выбранная клетка к которой можно переместиться
            {
                float distance = Vector3.Distance(transform.position, _wayPoints[0].position); // высчитывает дистанцию
                if (distance >= 0.8f) // если она больше или равно
                {
                    Move(); // поворачивает и двигаеться к цели
                    Rotate(_navMeshAgent.steeringTarget);
                    
                }
                else
                {
                    // как только дошел останавливается и начинает сажать растеия
                    Rotate(_wayPoints[0].position);
                    _navMeshAgent.isStopped = true;
                    Plant();
                    _animator.SetBool("Run", false);
                }
            }
    }
    }


    private void Move()
    {
        _navMeshAgent.isStopped = false;
        _animator.SetBool("Run", true);
        _navMeshAgent.SetDestination(_wayPoints[0].position);
    }

    private void Rotate(Vector3 target)
    {
        Vector3 lookrotation = target - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), _rotationSpeed);
    }

    private void Plant() // посадка
    {
        PlotManager plant = _wayPoints[0].GetComponent<PlotManager>();
        _planting = true;

        if (plant.IsPlanted) // если в клетке есть растения
        {
            if (!plant.LastStage) // растение на последней стадий
            {
                if (plant.GetPlantRemoveAfter()) // нужно ли удалять растения после сбора
                {
                    _animator.SetTrigger("Pull");
                    StartCoroutine(PullingPlant());
                }
                else
                {
                    _animator.SetTrigger("PickFruit");
                    StartCoroutine(PickingFruit());
                }

            }
            else // растение не на последней стадий
            {
                _animator.SetTrigger("Remove");
                StartCoroutine(Removing());
            }

        }else // начало посадки фруктов
        {
            _animator.SetTrigger("Plant");
            StartCoroutine(Planting());
        }
    }

    private IEnumerator Planting() // начало посадки растения
    {
        yield return new WaitForSeconds(7.567f); // время выполнения анимаций
        _planting = false;
        _wayPoints[0].gameObject.GetComponent<PlotManager>().Plant(); // сажает растения
        _wayPoints.RemoveAt(0);
    }

    private IEnumerator PullingPlant() // начало сбора растения
    {
        yield return new WaitForSeconds(4.733f);
        _planting = false;
        _wayPoints[0].gameObject.GetComponent<PlotManager>().Pull();
        _wayPoints.RemoveAt(0);
    }

    private IEnumerator PickingFruit() // начало сбора фруктов
    {
        yield return new WaitForSeconds(8.000f);
        _planting = false;
        _wayPoints[0].gameObject.GetComponent<PlotManager>().Pick();
        _wayPoints.RemoveAt(0);
    }

    private IEnumerator Removing() // просто удалить растение если не созрело
    {
        yield return new WaitForSeconds(4.667f);
        _planting = false;
        _wayPoints[0].gameObject.GetComponent<PlotManager>().Remove();
        _wayPoints.RemoveAt(0);
    }

    public void SetWayPoint(Transform transform)
    {
        _wayPoints.Add(transform);
    }
}
