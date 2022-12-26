using System;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    private readonly int _maxValue = 1;

    private int _value;

    public event Action Die;

    private void Start()
    {
        _value = _maxValue;
    }
    public void TakeDamage(int damage)
    {
        _value -= AffectDamage(damage);

        if (_value < 0)
            _value = 0;

        if (_value == 0)
            Die?.Invoke();
    }

    protected virtual int AffectDamage(int damage)
    {
        return damage;
    }
}
