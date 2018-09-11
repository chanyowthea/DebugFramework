using UnityEngine;
using UnityEngine.Internal;

namespace DebugFramework
{
    public enum LogColor
    {
        None,
        Green,
        Yellow,
        Red
    }

    public class Debugger
    {
        public static bool EnableLog = true;

        public static bool LogToFile = true;

        public static void Log(object message)
        {
            Log(message, null);
        }

        public static void Flush()
        {
            if (LogToFile && DebuggerFileOutput.instance != null)
            {
                DebuggerFileOutput.instance.FlushToFile();
            }
        }
        public static void OnApplicationQuit()
        {
            if (LogToFile && DebuggerFileOutput.instance != null)
            {
                DebuggerFileOutput.instance.FlushToFile();
                DebuggerFileOutput.instance.Close();
            }
        }

        public static void Log(object message, LogColor type)
        {
            if (EnableLog)
            {
                string fmtMessage = FormatMessage(message);
                WriteToFile(fmtMessage);
                string format;
                switch (type)
                {
                    case LogColor.Green:
                        format = "<color=green>{0}</color>";
                        break;
                    case LogColor.Yellow:
                        format = "<color=yellow>{0}</color>";
                        break;
                    case LogColor.Red:
                        format = "<color=red>{0}</color>";
                        break;
                    default:
                        format = "{0}";
                        break;
                }
                Debug.Log(string.Format(format, fmtMessage));
            }
        }

        public static void Log(object message, UnityEngine.Object context)
        {
            if (EnableLog)
            {
                string fmtMessage = FormatMessage(message);
                WriteToFile(fmtMessage);
                Debug.Log(fmtMessage, context);
            }
        }
        private static string FormatMessage(object message)
        {
            return string.Format("[{0}] {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message);
        }

        public static void LogError(object message)
        {
            LogError(message, null);
        }

        public static void LogError(object message, Object context)
        {
            if (EnableLog)
            {
                string fmtMessage = FormatMessage(message);
                WriteToFile(fmtMessage);
                Debug.LogError(fmtMessage, context);
            }
        }

        public static void LogWarning(object message)
        {
            LogWarning(message, null);
        }

        public static void LogWarning(object message, Object context)
        {
            if (EnableLog)
            {
                string fmtMessage = FormatMessage(message);
                WriteToFile(fmtMessage);
                Debug.LogWarning(fmtMessage, context);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            if (EnableLog)
            {
                Debug.DrawLine(start, end, color);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            if (EnableLog)
            {
                Debug.DrawLine(start, end, color, duration);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end)
        {
            if (EnableLog)
            {
                Debug.DrawLine(start, end);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
        {
            if (EnableLog)
            {
                Debug.DrawLine(start, end, color, duration, depthTest);
            }
        }
        public static void WriteToFile(string message)
        {
            if (LogToFile && DebuggerFileOutput.instance != null)
            {
                DebuggerFileOutput.instance.Log(message);
            }
        }
    }
}
