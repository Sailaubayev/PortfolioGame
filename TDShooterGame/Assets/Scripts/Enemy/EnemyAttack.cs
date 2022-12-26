using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bulletsPool;

    private Transform _thisTransform;

    private void Start()
    {
        _thisTransform = GetComponent<Transform>();
        _bulletsPool = new List<GameObject>();
    }

    public void Shot()
    {
        //diff - будет смещением нашего нажатия от объекта
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _thisTransform.position;
        //номализация приводит каждое значение в промежуток
        //от -1 до 1
        diff.Normalize();
        //по нормализованному виду мы находим угол, так как в diff
        //находится вектор, который можно перенести на тригонометрическую окружность
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //и приваиваем наш угол персонажу
        _thisTransform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        //Показывает, нашли ли мы выключенный объект в нашем массиве
        bool freeBullet = false;
        //Теперь необходимо проверить, есть ли выключенный объект в нашем пуле
        for (int i = 0; i < _bulletsPool.Count; i++)
        {
            //Смотрим, активен ли объект в игровом пространстве
            if (!_bulletsPool[i].activeInHierarchy)
            {
                //Если объект не активен
                //То мы задаем ему все нужные параметры
                //Позицию
                _bulletsPool[i].transform.position = _thisTransform.position;
                //Поворот
                _bulletsPool[i].transform.rotation = _thisTransform.rotation;
                //И опять его включаем
                _bulletsPool[i].SetActive(true);
                //Ставим объект найденным, чтоб опять не создавать лишний
                freeBullet = true;
                break;
            }
        }
        //если свободный объект не был найден, то нужно создать еще один
        if (!freeBullet)
        {
            //Создаем объект с нужными значениями и заносим его в пул
            _bulletsPool.Add(Instantiate(Resources.Load("Prefub/Bullet"), _thisTransform.position, _thisTransform.rotation) as GameObject);
        }
    }
}
