using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class PlotManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private FarmManager _fm; 
    [SerializeField] private MeshFilter _plantMesh;
    [SerializeField] private float _growthRate = 1f; // множитель роста 1 нормальное значение (можно использовать для удобрений)

    [Header("Cell Color")] // цвет клетки
    [SerializeField] private Color _availableColor = Color.green;
    [SerializeField] private Color _unavailableColor = Color.red;
    [SerializeField] private Color _standartColor = Color.white;

    private bool _isPlanted = false; // есть посаженное растение ?
    private int _plantStage = 0; // стадия роста растения 
    private bool _lastStage; // последняя стадия ?
    private float _timer; // таймер времени
    private SpriteRenderer _plot; // спрайт клетки 
    private PlantData _selectedPlant; // растения

    public bool LastStage => _lastStage;
    public PlantData SelectedPlant => _selectedPlant;
    public bool IsPlanted => _isPlanted;
    public Color StandartColor => _standartColor;

    [Header("Plant UI")] // UI растения
    [SerializeField] private RectTransform _plantUITransform;
    [SerializeField] private Text _stageText;
    [SerializeField] private Text _timeDescription;
    [SerializeField] private Text _timeBtwStagesText;

    private void Start()
    {
        _plot = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isPlanted) // если в клетку посаженно растения
        {
            _timer -= _growthRate  * Time.deltaTime;

            if (_timer <= 0 && _plantStage< SelectedPlant.PlantStages.Length-1)// если время меньше нуля и стадия роста не последняя
            {
                _timer = SelectedPlant.TimeBtwStages;
                _plantStage++;
                UpdatePlant(); //
            }
            else
            {
                CheckStage();
            }
            PlantUI(_timer, _plantStage); // UI Растения
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_fm.IsSelection)
        {
            _fm.SetSelectedCell(this);

            if (_isPlanted)
            {
                SetCellColor(StandartColor);
                _fm.MenuActive(false);
                _fm.HarvestMenuActive(true);
                CheckStage();
            }
            else
            {
                _fm.MenuActive(true);
                _fm.HarvestMenuActive(false);
                _fm.SetIsSelection(false);
            }
        } 

    }

    private void OnMouseOver()
    {
        if (_fm.IsSelection)
        {
            if(_isPlanted)
            {
                SetCellColor(_unavailableColor);
            }
            else
            {
                SetCellColor(_availableColor);
            }
        }
    }

    private void OnMouseExit()
    {
        if (_fm.IsSelection)
            SetCellColor(_standartColor);
    }

    public void SetCellColor(Color color)
    {
        _plot.color = color;
    }

    public void Pull() // собрать растения
    {
        _isPlanted = false;
        _plantMesh.gameObject.SetActive(false);
        _fm.Transaction(SelectedPlant.SellPrice);

        //int xp = Random.Range(currentPlant.xpMin, currentPlant.xpMax);
        int xp =  (int)SelectedPlant.TimeBtwStages * SelectedPlant.PlantStages.Length;//
        EventManager.XPEventStart(xp);

        if (SelectedPlant.PlantName == "ТЫКВА")//
            _fm.CarrotTake();
        _growthRate  = 1f;

        _fm.SetIsSelection(true);
        _fm.SetIsPlanting(false);
    }

    public void Pick() // собрать фрукты
    {
        _fm.Transaction(SelectedPlant.SellPrice);

        //int xp = Random.Range(currentPlant.xpMin, currentPlant.xpMax);
        int xp = (int)SelectedPlant.TimeBtwStages * SelectedPlant.PlantStages.Length;//
        EventManager.XPEventStart(xp);

        _growthRate  = 1f;

        _timer = SelectedPlant.TimeBtwStages;
        _plantStage = 1;
        UpdatePlant();

        _fm.SetIsSelection(true);
        _fm.SetIsPlanting(false);
    }

    public void Remove() // убрать растения
    {
        _isPlanted = false;
        _plantMesh.gameObject.SetActive(false);

        _growthRate  = 1f;

        _fm.SetIsSelection(true);
        _fm.SetIsPlanting(false);
    }


    public void Plant() // посадить растения
    {
        _selectedPlant = _fm.SelectedPlant.Plant;//
        _isPlanted = true;

        _plot.color = _standartColor;
        _plantStage = 0;
        UpdatePlant();
        _timer = SelectedPlant.TimeBtwStages;
        _plantMesh.gameObject.SetActive(true);

        _fm.SetIsSelection(true);
        _fm.SetIsPlanting(false);
    }

    private void UpdatePlant() // обновяет растения увеличивает стадию
    {
        _plantMesh.mesh = SelectedPlant.PlantStages[_plantStage];
        CheckStage();

        if (GetPlantRemoveAfter())
            _plantUITransform.localPosition = new Vector3(0, 5,0);
        else
            _plantUITransform.localPosition = new Vector3(0, 10, 0);
        //plantCollider.size = plant.mesh.bounds.size;
        //plantCollider.offset = new Vector2(0,plant.mesh.size.y/2);
    }

    private void PlantUI(float timer, int plantStage) // UI растения
    {
        if(plantStage < SelectedPlant.PlantStages.Length - 1)
        {
            float intTimer = (int)timer;
            _stageText.text = "Стадия " + plantStage.ToString();
            _timeDescription.text = "Время до след стадий";
            _timeBtwStagesText.text = intTimer.ToString();
        }
        else
        {
            _stageText.text = "Стадия " + plantStage.ToString();
            _timeDescription.text = "Созрело можно собирать";
            _timeBtwStagesText.text = "";
        }
    }

    private void CheckStage() // проверяет на какой стадие растения
    {
        if (_plantStage < SelectedPlant.PlantStages.Length - 1)
        {
            _lastStage = true;
        }
        else
        {
            _lastStage = false;
        }
    }

    public bool GetPlantRemoveAfter() 
    {
        if (SelectedPlant.RemoveAfter)
            return true;
        else
            return false;
    }
}
