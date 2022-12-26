using UnityEngine;

//[RequireComponent(typeof(Enemy))]
public class EnemyDetection : MonoBehaviour
{
    [SerializeField] [Range(0, 360)] private float _viewAngle = 90f;
    [SerializeField] private float _viewDistance = 8f;

    private Transform _thisTransform;

    //private Enemy _enemy;

    private void Awake()
    {
       // _enemy = GetComponent<Enemy>();
        _thisTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        DrawViewState();
        //float distanceToPlayer = Vector3.Distance(_enemy.Player.transform.position, _thisTransform.position);
        //if (!_enemy.PlayerDetected)
        //{
        //    if (distanceToPlayer <= _detectionDistance || IsInView())
        //     {
        //         _enemy.Detected(IsInView());
        //     }
        //     DrawViewState();
        // }
    }

    public bool IsInView(Transform player) // true если цель видна
    {
        Vector2 direction = _thisTransform.TransformDirection(new Vector2(_viewDistance, Mathf.Tan((0) * .5f * Mathf.Deg2Rad) * _viewDistance));

        float realAngle = Vector2.Angle(direction, player.position - _thisTransform.position);

        RaycastHit2D hit;
        hit = Physics2D.Raycast(_thisTransform.position, player.position - _thisTransform.position, _viewDistance);
        if (hit.collider != null)
        {
            if (realAngle < _viewAngle / 2f && Vector2.Distance(_thisTransform.position, player.position) <= _viewDistance && hit.transform == player.transform)
            {
                return true;
            }
        }
        return false;
    }


    private void DrawViewState()
    {
        Vector2 direction = _thisTransform.TransformDirection(new Vector2(_viewDistance, Mathf.Tan((0) * .5f * Mathf.Deg2Rad) * _viewDistance));

        Vector3 left = _thisTransform.position + Quaternion.Euler(new Vector3(0, 0, _viewAngle / 2f)) * (direction);
        Vector3 right = _thisTransform.position + Quaternion.Euler(-new Vector3(0, 0, _viewAngle / 2f)) * (direction);
        Debug.DrawLine(_thisTransform.position, left, Color.yellow);
        Debug.DrawLine(_thisTransform.position, right, Color.yellow);
    }


}
