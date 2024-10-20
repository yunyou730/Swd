using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using LitJson;
using swd;

namespace clash.gameplay
{
    /**
     Json config.Example:
     {
      "u101": {
        "tag": "u101",
        "cfg_path": "Assets/Resources_moved/clashgame/config/unit/u101.asset"
      },
      "u102": {
        "tag": "u102",
        "cfg_path":"Assets/Resources_moved/clashgame/config/unit/u102.asset"
      }
    }
    */
    public class ClashAllUnitsConfig : IDisposable
    {
        private ClashWorld _world = null;
        private SortedDictionary<string, string> _configMap = null;
        

        public ClashAllUnitsConfig(ClashWorld world,JsonData jsonData)
        {
            _world = world;
            
            _configMap = new SortedDictionary<string, string>();
            foreach (var unitTag in jsonData.Keys)
            {
                string configPath = jsonData[unitTag]["cfg_path"].ToString();
                _configMap.Add(unitTag, configPath);
            }
        }

        public string GetConfigPath(string unitTag)
        {
            if (_configMap.ContainsKey(unitTag))
            {
                return _configMap[unitTag];
            }
            return null;
        }


        public ClashCfgUnitEntry GetConfig(string unitTag)
        {
            string path = GetConfigPath(unitTag);
            if (!string.IsNullOrEmpty(path))
            {
                var unitCfgEntry = _world.ResManager.GetAsset<ClashCfgUnitEntry>(path);
                return unitCfgEntry;
            }
            return null;
        }
        
        public List<string> ToList()
        {
            List<string> result = new List<string>();
            foreach (var key in _configMap.Keys)
            {
                result.Add(key);
            }
            return result;
        }

        public void Dispose()
        {
            _configMap.Clear();
            _configMap = null;
        }
    }
}