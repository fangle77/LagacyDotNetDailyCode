using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace yltl.Common
{
    /// <summary>
    /// 写日志类，保存为log文件
    /// </summary>
    public class LogWriter
    {
        private string _logPath = string.Empty;//日志路径
        private string _currentFile = string.Empty;//当前日志文件名
        private FileInfo _fi;//当前日志文件的文件信息

        private string _baseDirectoryName;
        private string _baseFileName;

        public LogWriter() { }

        public LogWriter(string baseDirectoryName, string baseFileName)
        {
            this._baseDirectoryName = baseDirectoryName;
            this._baseFileName = baseFileName;

            InitPath();
        }

        public string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(_logPath))
                {
                    _logPath = Application.StartupPath.TrimEnd('\\') + "\\";

                    try
                    {
                        if (Directory.Exists(_logPath) == false)
                        {
                            Directory.CreateDirectory(_logPath);
                        }
                        if (_baseDirectoryName != null && _baseDirectoryName.Trim() != "")
                        {
                            _logPath = _logPath + _baseDirectoryName + "\\";
                            if (Directory.Exists(_logPath) == false)
                            {
                                Directory.CreateDirectory(_logPath);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                return _logPath;
            }
        }

        public string CurrentFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_currentFile)) ResetCurrentFileName();
                return _currentFile;
            }
        }

        public string ResetCurrentFileName()
        {
            _currentFile = string.Format("{0}{1}{2}{3}.log", LogPath, _baseFileName, "_", DateTime.Now.ToString("yyyyMMddHHmmss"));
            _fi = new FileInfo(_currentFile);
            return _currentFile;
        }

        public FileInfo CurrentFileInfo
        {
            get
            {
                if (_fi == null) ResetCurrentFileName();
                _fi.Refresh();
                return _fi;
            }
        }

        private void InitPath()
        {
            var logPath = LogPath;
            ResetCurrentFileName();
        }

        public void WriteLog(string content)
        {
            try
            {
                if (Directory.Exists(_logPath) == false)
                {
                    Directory.CreateDirectory(_logPath);
                }

                _fi.Refresh();
                if (_fi.Exists && _fi.Length > 2097152)//2097152 2M一个文件
                {
                    ResetCurrentFileName();
                }

                //File.AppendAllText(_fi.FullName, content + "\r\n", Encoding.Default);
                using (StreamWriter sw = _fi.AppendText())//new StreamWriter(_fi.FullName, true, Encoding.Default))
                {
                    sw.Write(content + "\r\n");
                }
            }
            catch { }
        }
    }
}
