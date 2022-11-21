using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _playerHealth = 100f;
    [SerializeField] private float _playerMoveSpeed = 1f;
    public float PlayerHealth => _playerHealth;
    public float PlayerMoveSpeed => _playerMoveSpeed;

    public enum AttackType : byte
    {
        knife, pistol, shotgun, rifle
    }

    private AttackType _attackType;

}
