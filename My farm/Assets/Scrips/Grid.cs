using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int XAxis = 10;
    [SerializeField] private int ZAxis = 10;
    [SerializeField] private float _cellSize = 1.25f;
    [SerializeField] private GameObject cells;
    
    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < XAxis; x++)
        {
            for (int z = 0; z < ZAxis; z++)
            {
                GameObject _newCell = Instantiate(cells, new Vector3(_cellSize * x, transform.position.y, _cellSize * z), Quaternion.Euler(90,0,0));
                _newCell.transform.SetParent(gameObject.transform);
            }
        }
    }


}
