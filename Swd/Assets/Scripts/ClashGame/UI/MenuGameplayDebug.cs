using System.Collections;
using System.Collections.Generic;
using System.Text;
using clash;
using clash.gameplay;
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
        private TMP_Dropdown _dropdownUnitTypeSelection = null;
        
        private StringBuilder _strMouseInfo = new StringBuilder();
        // private ETileTerrainType? _selectingTieType = null;
        
        public MenuGameplayDebug(MenuManager menuManager,EMenuType menuType,GameObject root):base(menuManager,menuType,root)
        {
            
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
            _dropdownUnitTypeSelection = _gameObject.transform.Find("Dropdown_UnitTypeSelection").GetComponent<TMP_Dropdown>();

            
            InitUnitSelectionOptions();
            _dropdownUnitTypeSelection.onValueChanged.AddListener(OnUnitTypeSelectionValueChanged);
            _dropdownTileTypeSelection.onValueChanged.AddListener(OnTileTypeSelectionValueChanged);
            
            
            RefreshModeLabel();
        }


        private void InitUnitSelectionOptions()
        {
            var clashWorld = ClashGame.Instance.GP.World;
            if (clashWorld == null)
                return;

            var unitTagList = clashWorld.AllUnitsCfg.ToList();
            _dropdownUnitTypeSelection.AddOptions(unitTagList);
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
        }

        // public override void OnClose()
        // {
        //     base.OnClose();
        // }

        public override void Dispose()
        {
            
        }

        private void OnTileTypeSelectionValueChanged(int index)
        {
            var clashWorld = ClashGame.Instance.GP.World;
            if (clashWorld == null)
                return;

            var cmdMeta = clashWorld.GetWorldMeta<CmdMeta>();

            TMP_Dropdown.OptionData opt = _dropdownTileTypeSelection.options[index];
            // Debug.Log(opt.text);

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

            cmdMeta.AddCmdChangeEditSelectedTerrainType(targetTerrainType);
            // Debug.Log(_selectingTieType);
        }

        private void OnUnitTypeSelectionValueChanged(int index)
        {
            
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
            var clashWorld = ClashGame.Instance.GP.World; 
            if (clashWorld != null)
            {
                var mouseCtrlMeta = clashWorld.GetWorldMeta<MouseCtrlMetaInfo>();

                _strMouseInfo.Clear();
                _strMouseInfo.Append($"mouse info: tile coordinate ({mouseCtrlMeta.TileX},{mouseCtrlMeta.TileY})");
                _txtMouseInfo.text = _strMouseInfo.ToString(); 
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
