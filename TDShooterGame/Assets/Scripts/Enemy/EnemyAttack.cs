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
        //diff - ����� ��������� ������ ������� �� �������
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _thisTransform.position;
        //����������� �������� ������ �������� � ����������
        //�� -1 �� 1
        diff.Normalize();
        //�� ���������������� ���� �� ������� ����, ��� ��� � diff
        //��������� ������, ������� ����� ��������� �� ������������������ ����������
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //� ���������� ��� ���� ���������
        _thisTransform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        //����������, ����� �� �� ����������� ������ � ����� �������
        bool freeBullet = false;
        //������ ���������� ���������, ���� �� ����������� ������ � ����� ����
        for (int i = 0; i < _bulletsPool.Count; i++)
        {
            //�������, ������� �� ������ � ������� ������������
            if (!_bulletsPool[i].activeInHierarchy)
            {
                //���� ������ �� �������
                //�� �� ������ ��� ��� ������ ���������
                //�������
                _bulletsPool[i].transform.position = _thisTransform.position;
                //�������
                _bulletsPool[i].transform.rotation = _thisTransform.rotation;
                //� ����� ��� ��������
                _bulletsPool[i].SetActive(true);
                //������ ������ ���������, ���� ����� �� ��������� ������
                freeBullet = true;
                break;
            }
        }
        //���� ��������� ������ �� ��� ������, �� ����� ������� ��� ����
        if (!freeBullet)
        {
            //������� ������ � ������� ���������� � ������� ��� � ���
            _bulletsPool.Add(Instantiate(Resources.Load("Prefub/Bullet"), _thisTransform.position, _thisTransform.rotation) as GameObject);
        }
    }
}
