using System.Collections;
using System.Collections.Generic;
using System.Text;
using clash;
using clash.Event;
using clash.gameplay;
using clash.Gameplay.UserCtrl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace clash.ui
{
    public class MenuGameplayDebug : ClashBaseMenu
    {
        private TextMeshProUGUI _txtMode = null;
        private TextMeshProUGUI _txtMouseInfo = null;
        private TMP_Dropdown _dropdownTileTypeSelection = null;
        private TMP_Dropdown _dropdownUnitTagSelection = null;

        private TextMeshProUGUI _txtUnitSelectInfo = null;

        private StringBuilder _strMouseInfo = new StringBuilder();

        private StringBuilder _strUnitSelectionInfo = new StringBuilder();
        // private ETileTerrainType? _selectingTieType = null;

        private UserController _userCtrl = null;


        private ClashEventManager _eventManager = null;
        
        public MenuGameplayDebug(MenuManager menuManager,EMenuType menuType,GameObject root):base(menuManager,menuType,root)
        {
            _eventManager = ClashGame.Instance.EventManager;
            _userCtrl = ClashGame.Instance.GP.UserCtrl;
        }

        public override void OnEnter()
        {
            Button btnMode = _gameObject.transform.Find("Button_ToggleMode").GetComponent<Button>();
            btnMode.onClick.AddListener(delegate()
            {
                ClashGame.Instance.GP.SwitchMode(NextMode(ClashGame.Instance.GP.GameMode));
                RefreshModeLabel();
            });
        
            Button btnClose = _gameObject.transform.Find("Button_Close").GetComponent<Button>();
            btnClose.onClick.AddListener(delegate()
            {
                _menuManager.CloseMenu(_menuType);
            });
            
            _txtMode = _gameObject.transform.Find("Text_GameMode").GetComponent<TextMeshProUGUI>();
            _txtMouseInfo = _gameObject.transform.Find("Text_MouseInfo").GetComponent<TextMeshProUGUI>();
            _dropdownTileTypeSelection = _gameObject.transform.Find("Dropdown_TileTypeSelection").GetComponent<TMP_Dropdown>();
            _dropdownUnitTagSelection = _gameObject.transform.Find("Dropdown_UnitTypeSelection").GetComponent<TMP_Dropdown>();
            _txtUnitSelectInfo = _gameObject.transform.Find("Panel_UnitSelection").Find("Text_UnitSelection").GetComponent<TextMeshProUGUI>();
            
            InitUnitSelectionOptions();
            _dropdownUnitTagSelection.onValueChanged.AddListener(OnUnitTypeSelectionValueChanged);
            _dropdownTileTypeSelection.onValueChanged.AddListener(OnTileTypeSelectionValueChanged);
            
            RefreshModeLabel();
        }


        private void InitUnitSelectionOptions()
        {
            var clashWorld = ClashGame.Instance.GP.World;
            if (clashWorld == null)
                return;

            var unitTagList = clashWorld.AllUnitsCfg.ToList();
            _dropdownUnitTagSelection.AddOptions(unitTagList);
        }

        protected override GameObject LoadGameObject()
        {
            var prefab = ClashGame.Instance.ResManager.GetAsset<GameObject>("Assets/Resources_moved/clashgame/ui/Canvas_MenueGameplayDebug.prefab");
            var go = GameObject.Instantiate(prefab);
            return go;
        }
    
        public override void OnUpdate(float deltaTime)
        {
            RefreshMouseStatusLabel();
            RefreshSelectUnitPanel();
        }

        public override void OnClose()
        {
            base.OnClose();
            
            _dropdownUnitTagSelection.onValueChanged.RemoveListener(OnUnitTypeSelectionValueChanged);
            _dropdownTileTypeSelection.onValueChanged.RemoveListener(OnTileTypeSelectionValueChanged);
        }

        public override void Dispose()
        {
            
        }

        private void OnTileTypeSelectionValueChanged(int index)
        {
            TMP_Dropdown.OptionData opt = _dropdownTileTypeSelection.options[index];
            ETileTerrainType? targetTerrainType = null;
            switch (opt.text)
            {
                case "TileType_Ground":
                    targetTerrainType = ETileTerrainType.Ground;
                    break;
                case "TileType_River":
                    targetTerrainType = ETileTerrainType.River;
                    break;
            }
            _eventManager.Invoke_EventChangeTileTerrainType(targetTerrainType);
        }

        private void OnUnitTypeSelectionValueChanged(int index)
        {
            string unitTag = null;
            if (index > 0)
            {
                unitTag = _dropdownUnitTagSelection.options[index].text;
            }

            _eventManager.Invoke_EventChangeDebugMenuSelectUnitTag(unitTag);
        }

        private void RefreshModeLabel()
        {
            switch (ClashGame.Instance.GP.GameMode)
            {
                case EClashGameMode.Test:
                    _txtMode.text = "Test Mode";
                    break;
                case EClashGameMode.MapEdit:
                    _txtMode.text = "Map Edit Mode";
                    break;
                case EClashGameMode.Play:
                    _txtMode.text = "Play Mode";
                    break;
                default:
                    _txtMode.text = "[Mode Error!]";
                    break;
            }
        }

        private void RefreshMouseStatusLabel()
        {
            if (_userCtrl != null)
            {
                _strMouseInfo.Clear();
                _strMouseInfo.Append($"mouse info: tile coordinate ({_userCtrl.SelectTileX},{_userCtrl.SelectTileY})");
                _txtMouseInfo.text = _strMouseInfo.ToString(); 
            }
        }


        private void RefreshSelectUnitPanel()
        {
            if (_userCtrl != null)
            {
                int uuid = _userCtrl.SelectUnitEntityId;
                string tag = _userCtrl.SelectedUnitTag;
            
                _strUnitSelectionInfo.Clear();
                _strUnitSelectionInfo.Append((uuid == 0) ?"no unit be selected" : $"unit uuid:{uuid},tag{tag}");
                _txtUnitSelectInfo.text = _strUnitSelectionInfo.ToString();
            }
        }
        
        private EClashGameMode NextMode(EClashGameMode curMode)
        {
            int nextMode = (int)curMode + 1;
            if (nextMode == (int)EClashGameMode.Max)
            {
                nextMode = 0;
            }
            return (EClashGameMode)nextMode;
        }
    }
    
}
