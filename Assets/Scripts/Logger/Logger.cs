using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Logger
{
    public static class Logger
    {
        public static void SendMsg(object msg) => UnityEngine.Debug.Log($"{FromFrame(2)} {msg}");
        public static void SendMsg(object msg, int frame) => UnityEngine.Debug.Log($"{FromFrame(frame)} {msg}");
        public static void SendAlert(object msg) => UnityEngine.Debug.LogError($"{FromFrame(2)} {msg}");
        public static void SendAlert(object msg, int frame) => UnityEngine.Debug.LogError($"{FromFrame(frame)} {msg}");
        public static void SendWarning(object msg) => UnityEngine.Debug.LogWarning($"{FromFrame(2)} {msg}");
        public static void SendMWarning(object msg, int frame) => UnityEngine.Debug.LogWarning($"{FromFrame(frame)} {msg}");
        
        public static string FromFrame(int frame) {
            return GetString(new StackTrace().GetFrame(frame));
        }
        private static string GetString(StackFrame stackFrame) {
            return new StringBuilder()
                    .Append("From class: ")
                    .Append(stackFrame.GetMethod().ReflectedType.Name)
                    .Append(" method: ")
                    .Append(stackFrame.GetMethod())
                    .Append(" message:")
                    .ToString();
        }
    }
}
