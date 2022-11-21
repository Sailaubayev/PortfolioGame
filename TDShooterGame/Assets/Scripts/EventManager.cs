using System;

public class EventManager
{
    public static event Action PlayerHealthEvent;

    public static void PlayerHealth()
    {
        PlayerHealthEvent?.Invoke();
    }
}
