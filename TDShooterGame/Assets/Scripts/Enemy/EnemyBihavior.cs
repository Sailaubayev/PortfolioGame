using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyDetection))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyBihavior : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionDistance = 1f;

    private EnemyHealth _enemyHealth;
    private EnemyMovement _enemyMovement;
    private EnemyDetection _enemyDetection;
    private EnemyAttack _enemyAttack;
    private Transform _thisTransform;


    private bool _playerDetected = false;
    private bool _reload = false;


    private void OnEnable()
    {
        //_enemyHealth.Die += Died;
    }

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _thisTransform = GetComponent<Transform>();
        _enemyDetection = GetComponent<EnemyDetection>();
        _enemyAttack = GetComponent<EnemyAttack>();
        //Создаем новый список, так как List - 
        //ссылка на динамический массив
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, _thisTransform.position);

        if (!_playerDetected)
        {
            if (distanceToPlayer <= _detectionDistance || _enemyDetection.IsInView(_player))
            {
                _playerDetected = _enemyDetection.IsInView(_player);
                return;
            }

            if (!_enemyMovement.Agent.pathPending && _enemyMovement.Agent.remainingDistance < 0.5f)
                _enemyMovement.AgentPatrol();
        }
        else
        {
            _enemyMovement.AgentMove(_player);

            if (!_reload)
                StartCoroutine(Shoting());
        }
        //_enemyMovement.AgentRotation();
    }

    //private void LateUpdate()
    ///{
   //     _enemyMovement.AgentRotation();
    //}

    private IEnumerator Shoting()
    {
        _reload = true;
        _enemyAttack.Shot();
        yield return new WaitForSeconds(1f);
        _reload = false;
    }

    private void Died()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        _enemyHealth.Die -= Died;
    }
}
