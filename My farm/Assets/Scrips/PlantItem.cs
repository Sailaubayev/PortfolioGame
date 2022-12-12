using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    [SerializeField] private PlantData _plant; // растения
    [SerializeField] private Text _nameTxt; // имя
    [SerializeField] private Text _priceTxt; // цена
    [SerializeField] private Image _icon; // иконка
    [SerializeField] private Image _btnImage;
    [SerializeField] private FarmManager _farmMenager;

    public PlantData Plant => _plant;

    // Start is called before the first frame update
    void Start()
    {
        InitializeUI();
    }

    private void Update()
    {
       if(_plant.BuyPrice > _farmMenager.Money) // меняет цвет кнопки в зависимости от наличия денег
        {
            _btnImage.color = Color.red;
        }
        else
        {
            _btnImage.color = Color.green;
        }
    }

    public void BuyPlant() //кнопка покупки растения
    {
        if (!_farmMenager.IsSelection && _plant.BuyPrice <= _farmMenager.Money)
        {
            _farmMenager.PlantBtn(_plant.BuyPrice, this, _plant);
        }
    }

    private void InitializeUI()
    {
        _nameTxt.text = _plant.PlantName;
        _priceTxt.text = "$" + _plant.BuyPrice;
        _icon.sprite = _plant.PlantIcon;
    }
}
