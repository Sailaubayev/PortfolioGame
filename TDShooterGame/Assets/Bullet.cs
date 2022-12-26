using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�������� ����� ����
    public float Speed;
    //�����, ����� ������� ���� ����
    //����� � ��� ��������
    public float TimeToDisable;
    //������ ���, ����� ���� ���� ������������
    void OnEnable()
    {
        //�� ����� ��������� ������ ��� ����
        //���� ��������� �
        StartCoroutine(SetDisabled(TimeToDisable));

    }
    IEnumerator SetDisabled(float TimeToDisable)
    {
        //������ ����� ������������ ���� ���������� �� 
        //TimeToDisable ������, � ����� ��������� ������
        yield return new WaitForSeconds(TimeToDisable);
        //��������� ������, �� � ��� ��������� � ���� ��������
        //�� ���������� �������������
        gameObject.SetActive(false);
    }
    void Update()
    {
        //������ ���� ����� ������ ������ �� ����, ���� ������ �� ����� ��������
        //��� ��� ���� ���� ��� ��������� � ������ �������, � � 2D ������������
        //������ ��� �������, �� ����� ���� ���� ������ ������ ������� 
        //������������� ������ �������� ������������
        transform.position = transform.position + transform.right * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBihavior _enemyBihaviour = collision.gameObject.GetComponent<EnemyBihavior>();

        if (_enemyBihaviour)
        {
            //��������� �������� ���������� ���� � ������ ���� �� ���������
            //��������� ����� ��������
            StopCoroutine("SetDisabled");
            //��������� ������
            gameObject.SetActive(false);
            //������ ���� ��� ���, ������� ������ ��� ����� ����

        }
    }
}
