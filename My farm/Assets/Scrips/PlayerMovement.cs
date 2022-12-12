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
    [SerializeField] private bool _planting = false; // ���� �������
    [SerializeField] private List<Transform> _wayPoints; // ����� �����������

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
        if (!_planting) // ���� �� ���� �������
        {
            if (_wayPoints.Count > 0) // ���� ���� ��������� ������ � ������� ����� �������������
            {
                float distance = Vector3.Distance(transform.position, _wayPoints[0].position); // ����������� ���������
                if (distance >= 0.8f) // ���� ��� ������ ��� �����
                {
                    Move(); // ������������ � ���������� � ����
                    Rotate(_navMeshAgent.steeringTarget);
                    
                }
                else
                {
                    // ��� ������ ����� ��������������� � �������� ������ �������
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

    private void Plant() // �������
    {
        PlotManager plant = _wayPoints[0].GetComponent<PlotManager>();
        _planting = true;

        if (plant.IsPlanted) // ���� � ������ ���� ��������
        {
            if (!plant.LastStage) // �������� �� ��������� ������
            {
                if (plant.GetPlantRemoveAfter()) // ����� �� ������� �������� ����� �����
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
            else // �������� �� �� ��������� ������
            {
                _animator.SetTrigger("Remove");
                StartCoroutine(Removing());
            }

        }else // ������ ������� �������
        {
            _animator.SetTrigger("Plant");
            StartCoroutine(Planting());
        }
    }

    private IEnumerator Planting() // ������ ������� ��������
    {
        yield return new WaitForSeconds(7.567f); // ����� ���������� ��������
        _planting = false;
        _wayPoints[0].gameObject.GetComponent<PlotManager>().Plant(); // ������ ��������
        _wayPoints.RemoveAt(0);
    }

    private IEnumerator PullingPlant() // ������ ����� ��������
    {
        yield return new WaitForSeconds(4.733f);
        _planting = false;
        _wayPoints[0].gameObject.GetComponent<PlotManager>().Pull();
        _wayPoints.RemoveAt(0);
    }

    private IEnumerator PickingFruit() // ������ ����� �������
    {
        yield return new WaitForSeconds(8.000f);
        _planting = false;
        _wayPoints[0].gameObject.GetComponent<PlotManager>().Pick();
        _wayPoints.RemoveAt(0);
    }

    private IEnumerator Removing() // ������ ������� �������� ���� �� �������
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
