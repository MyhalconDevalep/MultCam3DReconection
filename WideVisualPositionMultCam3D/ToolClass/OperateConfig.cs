using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class OperateConfig
    {
        private Dictionary<string, Dictionary<string, string>> _sectionData;
        private string _filePath;
        private string _delimiter;

        public OperateConfig()
        {
            _sectionData = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 初始化配置文件读取器
        /// </summary>
        public void KeyValueFileReader(string filePath, string delimiter = ":=")
        {
            if (string.IsNullOrWhiteSpace(filePath))
                LoggerHelper._.Error(string.Format("文件路径不能为空", nameof(filePath)));

            _filePath = filePath;
            _delimiter = delimiter;

            _sectionData.Clear();
            LoadFile();
        }

        /// <summary>
        /// 加载并解析配置文件
        /// </summary>
        private void LoadFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    File.Create(_filePath).Close();
                    return;
                }

                using (var reader = new StreamReader(_filePath))
                {
                    string line;
                    string currentSection = null;

                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();

                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;

                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            currentSection = line.Substring(1, line.Length - 2).Trim();
                            if (!_sectionData.ContainsKey(currentSection))
                                _sectionData[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }
                        else if (currentSection != null)
                        {
                            var parts = line.Split(new[] { _delimiter }, 2, StringSplitOptions.None);
                            if (parts.Length == 2)
                            {
                                var key = parts[0].Trim();
                                var value = parts[1].Trim();

                                if (!string.IsNullOrEmpty(key))
                                    _sectionData[currentSection][key] = value;
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                LoggerHelper._.Error($"读取文件时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取值：通过节名和键名
        /// </summary>
        public string GetValue(string section, string key)
        {
            if (string.IsNullOrWhiteSpace(section) || string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("节名或键名不能为空");

            if (_sectionData.TryGetValue(section, out var kvPairs))
            {
              string returnValue=   kvPairs.TryGetValue(key, out var value) ? value : null;

                return returnValue;
            }

            return null;
        }

        /// <summary>
        /// 设置值：通过节名和键名
        /// </summary>
        public void SetValue(string section, string key, string value, bool autoSave = true)
        {
            if (string.IsNullOrWhiteSpace(section) || string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("节名或键名不能为空");

            if (!_sectionData.ContainsKey(section))
            {
                _sectionData[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            var sectionDict = _sectionData[section];

            // 如果值相同就不更新
            if (sectionDict.TryGetValue(key, out var existingValue) && existingValue == value)
            {
                return;
            }

            // 更新值
            sectionDict[key] = value;

            if (autoSave)
            {
                SaveToFile();
            }
        }


        /// <summary>
        /// 保存到文件
        /// </summary>
        public void SaveToFile()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath, false))
                {
                    foreach (var section in _sectionData)
                    {
                        writer.WriteLine($"[{section.Key}]");

                        foreach (var kvp in section.Value)
                        {
                            writer.WriteLine($"{kvp.Key}{_delimiter}{kvp.Value}");
                        }

                        writer.WriteLine(); // 空行隔开
                    }
                }
            }
            catch (IOException ex)
            {
                LoggerHelper._.Error($"保存文件时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 是否包含某个键
        /// </summary>
        public bool ContainsKey(string section, string key)
        {
            return _sectionData.ContainsKey(section) && _sectionData[section].ContainsKey(key);
        }

        /// <summary>
        /// 获取所有节名
        /// </summary>
        public IEnumerable<string> GetAllSections()
        {
            return _sectionData.Keys.ToList();
        }

        /// <summary>
        /// 获取某个节下的所有键
        /// </summary>
        public IEnumerable<string> GetAllKeys(string section)
        {
            if (_sectionData.ContainsKey(section))
                return _sectionData[section].Keys.ToList();

            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// 获取所有键值对（按节组织）
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> GetAllKeyValuePairs()
        {
            return _sectionData.ToDictionary(k => k.Key,
                v => new Dictionary<string, string>(v.Value, StringComparer.OrdinalIgnoreCase),
                StringComparer.OrdinalIgnoreCase);
        }
    }
}
