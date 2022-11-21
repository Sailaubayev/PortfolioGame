using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Rigidbody2D _rigidbody2d;
    private void Awake()
    {
       _playerStats = GetComponent<PlayerStats>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void MoveCharacter(Vector2 direction)
    {
        //_rigidbody2d.MovePosition(direction * _playerStats.PlayerMoveSpeed * Time.deltaTime);
        _rigidbody2d.AddForce(direction * _playerStats.PlayerMoveSpeed * Time.fixedDeltaTime ,ForceMode2D.Impulse);

    }

    public void RotateCharacter(Vector3 direction)
    {

        if (direction.sqrMagnitude > 0.1f)
            transform.right = direction;

    }
}
