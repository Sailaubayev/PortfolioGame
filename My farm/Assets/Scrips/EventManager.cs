using System;
//������� � ����� ��� ���������� �����
public class EventManager
{
    public static event Action<int> XPEvent;
    public static void XPEventStart(int xp)
    {
        XPEvent?.Invoke(xp);
    }
}
