using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace DebugFramework
{
    public class DebuggerFileOutput : MonoBehaviour
    {
        static public DebuggerFileOutput instance = null;

        private List<string> m_OutputText = new List<string>();

        private string m_OutputFilePath;
        private StreamWriter m_Writer;
        void Start()
        {
            m_OutputFilePath =
#if UNITY_EDITOR || UNITY_STANDALONE
            Application.dataPath
#else
            Application.persistentDataPath
#endif
            + "/debugger.log";
            m_Writer = new StreamWriter(m_OutputFilePath, false, Encoding.UTF8);
            m_Writer.AutoFlush = true;
            //ready
            instance = this;
        }
        void Update()
        {
            if (m_Writer == null)
            {
                return;
            }
            Flush();
        }
        void OnDestroy()
        {
            if (m_Writer != null)
            {
                m_Writer.Flush();
                Close();
            }
        }
        public void Log(string msg)
        {
            lock (m_OutputText)
            {
                m_OutputText.Add(msg);
            }
        }
        public void Flush()
        {
            if (m_Writer == null)
            {
                return;
            }
            lock (m_OutputText)
            {
                if (m_OutputText.Count > 0)
                {
                    foreach (string t in m_OutputText)
                    {
                        m_Writer.WriteLine(t);
                    }
                    m_OutputText.Clear();
                }
            }
        }

        public void FlushToFile()
        {
            Flush();
            if (m_Writer != null)
            {
                m_Writer.Flush();
            }
        }
        public void Close()
        {
            if (m_Writer != null)
            {
                m_Writer.Close();
                m_Writer = null;
            }
        }
    }
}