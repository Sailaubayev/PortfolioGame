using UnityEngine;

[CreateAssetMenu(fileName = "New PlantsData", menuName = "Plants Data", order = 51)]
public class PlantData : ScriptableObject
{
    [SerializeField] private string _plantName; // им€ растени€
    [SerializeField] private string _plantCollection; // способ сбора растени€
    [SerializeField] private Sprite _plantIcon; // иконка растени€
    [SerializeField] private Sprite _harvestIcon; // иконка сбора растени€
    [SerializeField] private float _timeBtwStages; // врем€ между стади€ми роста
    [SerializeField] private int _buyPrice; // цена при покупке
    [SerializeField] private int _sellPrice; // цена при продаже
    [SerializeField] private bool _removeAfter; // удалить растени€ после сбора
    [SerializeField] private Mesh[] _plantStages; // стадий роста растени€
    //public int xpMin,xpMax;

    public string PlantName => _plantName;
    public string PlantCollection => _plantCollection;
    public Sprite PlantIcon => _plantIcon;
    public Sprite HarvestIcon => _harvestIcon;
    public float TimeBtwStages => _timeBtwStages;
    public int BuyPrice => _buyPrice;
    public int SellPrice => _sellPrice;
    public bool RemoveAfter => _removeAfter;
    public Mesh[] PlantStages => _plantStages;
}
