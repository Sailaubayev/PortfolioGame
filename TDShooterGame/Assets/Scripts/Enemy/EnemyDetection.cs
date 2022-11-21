using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDetection : MonoBehaviour
{
    [SerializeField] [Range(0, 360)] private float _viewAngle = 90f;
    [SerializeField] private float _viewDistance = 8f;
    [SerializeField] private float _detectionDistance = 1f;

    private float distanceToPlayer;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(_enemy.Player.transform.position, transform.position);
        if (!_enemy.PlayerDetected)
        {
            if (distanceToPlayer <= _detectionDistance || IsInView())
            {
                _enemy.Detected(IsInView());
            }
            DrawViewState();
        }
    }

    public bool IsInView() // true если цель видна
    {
        Vector2 direction = transform.TransformDirection(new Vector2(_viewDistance, Mathf.Tan((0) * .5f * Mathf.Deg2Rad) * _viewDistance));

        float realAngle = Vector2.Angle(direction, _enemy.Player.position - transform.position);

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, _enemy.Player.position - transform.position, _viewDistance);
        if (hit.collider != null)
        {
            if (realAngle < _viewAngle / 2f && Vector2.Distance(transform.position, _enemy.Player.position) <= _viewDistance && hit.transform == _enemy.Player.transform)
            {
                return true;
            }
        }
        return false;
    }


    private void DrawViewState()
    {
        Vector2 direction = transform.TransformDirection(new Vector2(_viewDistance, Mathf.Tan((0) * .5f * Mathf.Deg2Rad) * _viewDistance));

        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, 0, _viewAngle / 2f)) * (direction);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, 0, _viewAngle / 2f)) * (direction);
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }


}
