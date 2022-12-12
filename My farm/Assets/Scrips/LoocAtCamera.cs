using UnityEngine;

[RequireComponent(typeof(Transform))]
public class LoocAtCamera : MonoBehaviour
{
    // Скрипт чтоб UI смотрел на камеру
    [SerializeField] private Transform _mainCamera;

    private Transform _localTransform;

    private void Start()
    {
        _localTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_mainCamera)
            _localTransform.LookAt(2 * _localTransform.position - _mainCamera.position);
    }
}
