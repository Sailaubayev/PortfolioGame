using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _dumping = 1.5f;
    [SerializeField] private Vector2 _offset = new Vector2(2f, 1f);

    private bool _isLeft;
    private int _lastX;


    private void Start()
    {
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
    }

    private void LateUpdate()
    {
            int currentX = Mathf.RoundToInt(_playerTransform.position.x);
            if (currentX > _lastX) _isLeft = false; 
            else if (currentX < _lastX) _isLeft = true;

            Vector3 target;
            if (_isLeft)
            {
                target = new Vector3(_playerTransform.position.x - _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
            }else
            {
                target = new Vector3(_playerTransform.position.x + _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, _dumping * Time.deltaTime);
            transform.position = currentPosition;

    }

}
