using Handler = System.Action<System.Object, System.Object>;

public class NotificationObserver
{
    public Handler Handler { get; set; }
    public string NotificationName { get; set; }

    public NotificationObserver(Handler handler, string notificationName)
    {
        Handler = handler;
        NotificationName = notificationName;
    }
}

