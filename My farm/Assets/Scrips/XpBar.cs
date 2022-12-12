using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    [SerializeField] private Image _xpBar;
    [SerializeField] private Text _xpText;
    [SerializeField] private int _multiplyPercent = 15;
    [SerializeField] private float _maxXP = 100;

    private int _xp = 0;

    void Start()
    {
        EventManager.XPEvent += XPChange;
    }

    private void XPChange(int xp)
    {
        _xp += xp;

        if (_xp > _maxXP)
        {
            _xp = 0;
            float percent = _maxXP / 100;
            _maxXP = _maxXP + (percent * _multiplyPercent);
        }

        _xpBar.fillAmount = _xp / _maxXP;
       
        _xpText.text = _xp.ToString() + " / " + ((int)_maxXP);
    }
}
