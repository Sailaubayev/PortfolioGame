using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 1;
    [SerializeField] private Transform _player;

    private bool _playerDetected;

    public bool PlayerDetected => _playerDetected;
    public Transform Player => _player;



    public void Detected(bool detect)
    {
        _playerDetected = detect;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }
}
