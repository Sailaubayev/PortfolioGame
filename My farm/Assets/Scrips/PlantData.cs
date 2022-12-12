using UnityEngine;

[CreateAssetMenu(fileName = "New PlantsData", menuName = "Plants Data", order = 51)]
public class PlantData : ScriptableObject
{
    [SerializeField] private string _plantName; // ��� ��������
    [SerializeField] private string _plantCollection; // ������ ����� ��������
    [SerializeField] private Sprite _plantIcon; // ������ ��������
    [SerializeField] private Sprite _harvestIcon; // ������ ����� ��������
    [SerializeField] private float _timeBtwStages; // ����� ����� �������� �����
    [SerializeField] private int _buyPrice; // ���� ��� �������
    [SerializeField] private int _sellPrice; // ���� ��� �������
    [SerializeField] private bool _removeAfter; // ������� �������� ����� �����
    [SerializeField] private Mesh[] _plantStages; // ������ ����� ��������
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
