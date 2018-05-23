using UnityEngine;
using System.Collections;

public class LogOutputHandler : Singleton<LogOutputHandler>
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);    
    }

    //Register the HandleLog function on scene start to fire on debug.log events
    public void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    //Remove callback when object goes out of scope
    public void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    //Create a string to store log level in
    string level = "";

    //Capture debug.log output, send logs to Loggly
    public void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Warning) return;

        level = type.ToString();
        var loggingForm = new WWWForm();

        loggingForm.AddField("LogType", level);
        loggingForm.AddField("Message", logString);
        loggingForm.AddField("StackTrace", stackTrace);
        loggingForm.AddField("DeviceModel", SystemInfo.deviceModel);
        StartCoroutine(SendData(loggingForm));
    }

    public IEnumerator SendData(WWWForm form)
    {
        //Send WWW Form to Loggly, replace TOKEN with your unique ID from Loggly
        WWW sendLog = new WWW("http://logs-01.loggly.com/inputs/7f2aa845-70e6-4b44-bf4d-4c8890b87521/tag/Unity3D", form);
        yield return sendLog;
    }
}