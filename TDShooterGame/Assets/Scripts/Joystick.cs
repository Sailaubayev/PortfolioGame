using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IEndDragHandler, IDragHandler
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Transform _circle;
    [SerializeField] private float _outerRadius = 1f;

    private bool _touchStart = false;
    private Vector2 _pointA;
    private Vector2 _pointB;

    private void FixedUpdate()
    {
        if (_touchStart)
        {
            Vector2 offset = _pointA - _pointB;
            Vector3 direction = Vector2.ClampMagnitude(offset, _outerRadius);

            _circle.transform.position = gameObject.transform.position + direction;

            _playerMove.MoveCharacter(direction);
            _playerMove.RotateCharacter(direction);
        }
        else
        {
            _circle.transform.localPosition = Vector3.zero;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        _pointA = Camera.main.ScreenToWorldPoint(eventData.position);
        _pointB = gameObject.transform.position;
        _touchStart = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _touchStart = false;
        _circle.transform.localPosition = Vector3.zero;
    }

}
