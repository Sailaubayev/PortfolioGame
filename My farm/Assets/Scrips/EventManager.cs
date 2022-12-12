using System;
//События и метод для увеличения опыта
public class EventManager
{
    public static event Action<int> XPEvent;
    public static void XPEventStart(int xp)
    {
        XPEvent?.Invoke(xp);
    }
}
