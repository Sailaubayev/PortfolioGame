using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]int _rayDistance = 5;
    [SerializeField] private Transform[] _patrolPoints; 

    private Transform _currentPosition;


    public GameObject[] path; // точки подряд

    private int _totalItems = 0;

    private int _angleCount = 3;

    void Start()
    {


        _totalItems = gameObject.transform.childCount;

        path = new GameObject[_totalItems];

        for (int i = 0; i < _totalItems; i++)
        {
            path[i] = gameObject.transform.GetChild(i).gameObject;
        }
        Debug.Log(_totalItems);
    }

    private void FixedUpdate()
    {
        PLayerTransform();
    }

    public void PLayerTransform()
    {
            Vector2 direction = transform.TransformDirection(new Vector2(_rayDistance, Mathf.Tan((0) * .5f * Mathf.Deg2Rad) * _rayDistance));

        //if (transform.localScale.x < 0f)
        //{
        //    direction.x = -direction.x;
        //}

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10);

            Debug.DrawRay(transform.position, direction, Color.red);
        if(hit.collider != null)
        if (hit.collider.tag == "Player")
        {
                Debug.Log(hit.collider.tag);
        
        }else
            Debug.Log(hit.collider.tag);
    }

    private float AngleChange(int multiplay, float angle)
    {
        return angle * multiplay;
    }
}
