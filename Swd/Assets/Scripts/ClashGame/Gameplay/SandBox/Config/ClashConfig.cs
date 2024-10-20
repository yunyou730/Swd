﻿using System;
using UnityEngine;

namespace clash.gameplay
{
    [Serializable,CreateAssetMenu(fileName = "ClashGameConfig", menuName = "ClashGame/ScriptableObjects/ClashGameConfig")]
    public class ClashConfigSettings : ScriptableObject
    {
        public float kTileSize = 1.0f;
        public float kTileBaseX = 0;
        public float kTileBaseZ = 0;
        
        public int kLogicFPS = 16;
        
        public float kCameraMoveSpeed = 5.0f;
        public bool kReverseMouseMidBtnDir = false;

        public float kCameraZoomSpeed = 5.0f;
    }
}