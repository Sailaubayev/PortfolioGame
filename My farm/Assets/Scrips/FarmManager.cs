using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    [SerializeField] private int _money = 100;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _mainCamera;
    [SerializeField] private bool _isSelection = true; // 
    [SerializeField] private bool _isPlanting = false; //

    private PlotManager _selectedCell; // Клетка
    private PlantItem _selectPlant; // Растения 
    private int _xp = 0; // Опыт
    private int _carrot = 0; // Морковь

    public bool IsSelection => _isSelection;
    public bool IsPlanting => _isPlanting;
    public int Money => _money;
    public PlantItem SelectedPlant => _selectPlant;


    [Header ("UI")] // UI элементы на сцене
    [SerializeField] private Text moneyTxt;
    [SerializeField] private Text carrotText;

    [Header("HarvestUI")] // Кнопка сбора урожая
    [SerializeField] private GameObject _harvestMenu;
    [SerializeField] private Image _harvesImg;
    [SerializeField] private Sprite _defaultHarvestImg;
    [SerializeField] private Text _harvestBtnText;


    [Header("StoreUI")] // Меню Магазина
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
        if (_selectedCell) // если есть выбранная клетка
        {
            if (_selectedCell.IsPlanted) // и в ней еще ничего не посаженно 
                ChangeStatus();
        }

        if(!IsSelection) // если ячейка выбранна перемешает камеру к этой ячейке
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

    private void ChangeStatus() // Меняет статус кнопки сбора урожая определяя на какой стадие роста растение
    {
        if (!_selectedCell.LastStage)
        {
            _harvestBtnText.text = _selectedCell.SelectedPlant.PlantCollection;
            _harvesImg.sprite = _selectedCell.SelectedPlant.HarvestIcon;
        }
        else
        {
            _harvesImg.sprite = _defaultHarvestImg;
            _harvestBtnText.text = "Убрать";
        }


    }

    private void GiveWayPoint() // дает игроку клетку к которой нужно двигаться
    {
        _playerMovement.SetWayPoint(_selectedCell.transform);
        _isPlanting = true;
    }

    public void PlantBtn(int buyPrice, PlantItem plantItem, PlantData plantObject) // при нажатие на кнопку "Посадить"
    {
        if (!IsPlanting)
        {
            Transaction(-plantObject.BuyPrice);
            SelectPlant(plantItem);
            GiveWayPoint();
        }

    }

    public void HarvestBtnClick() // нажатие на кнопку сбора урожая
    {
        if (!IsPlanting)
        {
            HarvestMenuActive(false);
            GiveWayPoint();
        }

    }

    public void MenuActive(bool active) // меню магазина
    {
        _menuStore.SetActive(active);
        if (!active)
        {
            SetIsSelection(true);
            _selectedCell.SetCellColor(_selectedCell.StandartColor);
        }
    }

    public void HarvestMenuActive(bool active) // сбор урожая
    {
        _harvestMenu.SetActive(active);
        _harvestBtnText.text = "Выкопать";
    }


    public void Transaction(int value) // отвечает за перевод денег
    {
        _money += value;
        moneyTxt.text = "$" + _money;
    }

    public void CarrotTake() // добавляет морковь 
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
