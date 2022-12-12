using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    [SerializeField] private int _money = 100;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _mainCamera;
    [SerializeField] private bool _isSelection = true; // 
    [SerializeField] private bool _isPlanting = false; //

    private PlotManager _selectedCell; // ������
    private PlantItem _selectPlant; // �������� 
    private int _xp = 0; // ����
    private int _carrot = 0; // �������

    public bool IsSelection => _isSelection;
    public bool IsPlanting => _isPlanting;
    public int Money => _money;
    public PlantItem SelectedPlant => _selectPlant;


    [Header ("UI")] // UI �������� �� �����
    [SerializeField] private Text moneyTxt;
    [SerializeField] private Text carrotText;

    [Header("HarvestUI")] // ������ ����� ������
    [SerializeField] private GameObject _harvestMenu;
    [SerializeField] private Image _harvesImg;
    [SerializeField] private Sprite _defaultHarvestImg;
    [SerializeField] private Text _harvestBtnText;


    [Header("StoreUI")] // ���� ��������
    [SerializeField] private GameObject _menuStore;

    // Start is called before the first frame update
    private void Start()
    {
        moneyTxt.text = "$" + _money;
        carrotText.text = _carrot.ToString();
        EventManager.XPEventStart(_xp);
    }

    private void FixedUpdate()
    {
        if (_selectedCell) // ���� ���� ��������� ������
        {
            if (_selectedCell.IsPlanted) // � � ��� ��� ������ �� ��������� 
                ChangeStatus();
        }

        if(!IsSelection) // ���� ������ �������� ���������� ������ � ���� ������
            _mainCamera.MovePosition(_selectedCell.transform);

    }


    private void SelectPlant(PlantItem newPlant) 
    {
        if(SelectedPlant == newPlant)
        {
            CheckSelection();
        }
        else
        {
            _selectPlant = newPlant;
        }
    }

    private void CheckSelection()
    {
        if (IsSelection)
        {
            if (SelectedPlant != null)
            {
                _selectPlant = null;
            }

        }
    }

    private void ChangeStatus() // ������ ������ ������ ����� ������ ��������� �� ����� ������ ����� ��������
    {
        if (!_selectedCell.LastStage)
        {
            _harvestBtnText.text = _selectedCell.SelectedPlant.PlantCollection;
            _harvesImg.sprite = _selectedCell.SelectedPlant.HarvestIcon;
        }
        else
        {
            _harvesImg.sprite = _defaultHarvestImg;
            _harvestBtnText.text = "������";
        }


    }

    private void GiveWayPoint() // ���� ������ ������ � ������� ����� ���������
    {
        _playerMovement.SetWayPoint(_selectedCell.transform);
        _isPlanting = true;
    }

    public void PlantBtn(int buyPrice, PlantItem plantItem, PlantData plantObject) // ��� ������� �� ������ "��������"
    {
        if (!IsPlanting)
        {
            Transaction(-plantObject.BuyPrice);
            SelectPlant(plantItem);
            GiveWayPoint();
        }

    }

    public void HarvestBtnClick() // ������� �� ������ ����� ������
    {
        if (!IsPlanting)
        {
            HarvestMenuActive(false);
            GiveWayPoint();
        }

    }

    public void MenuActive(bool active) // ���� ��������
    {
        _menuStore.SetActive(active);
        if (!active)
        {
            SetIsSelection(true);
            _selectedCell.SetCellColor(_selectedCell.StandartColor);
        }
    }

    public void HarvestMenuActive(bool active) // ���� ������
    {
        _harvestMenu.SetActive(active);
        _harvestBtnText.text = "��������";
    }


    public void Transaction(int value) // �������� �� ������� �����
    {
        _money += value;
        moneyTxt.text = "$" + _money;
    }

    public void CarrotTake() // ��������� ������� 
    {
        _carrot++;
        carrotText.text = _carrot.ToString();
    }

    public void SetIsSelection(bool isSelection)
    {
        _isSelection = isSelection;
    }

    public void SetIsPlanting(bool isPlanting)
    {
        _isPlanting = isPlanting;
    }

    public void SetSelectedCell(PlotManager selectedCell)
    {
        _selectedCell = selectedCell;
    }
}
