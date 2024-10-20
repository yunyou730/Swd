using System;
using clash.gameplay;
using UnityEngine;

namespace clash.Gameplay.UserCtrl
{
    public class CameraController : IDisposable
    {
        private ClashWorld _clashWorld = null;
        private Camera _mainCamera = null;

        // private ClashConfig _clashCfg = null;
        private Vector2? _prevMouseHoldMidBtnPos = null;
        private const int kMouseMidBtnKey = 2;

        public CameraController(ClashWorld world,Camera mainCamera)
        {
            _clashWorld = world;
            _mainCamera = mainCamera;
        }


        public void OnStart()
        {
            
        }

        public void OnUpdate(float dt)
        {
            MoveCamera(dt);
            ZoomCamera(dt);
        }
        
        private void MoveCamera(float dt)
        {
            if (Input.GetMouseButtonDown(kMouseMidBtnKey))
            {
                // Mouse mid button pressed
                _prevMouseHoldMidBtnPos = Input.mousePosition;
            }
            else if(Input.GetMouseButtonUp(kMouseMidBtnKey))
            {
                // Mouse mid button released
                _prevMouseHoldMidBtnPos = null;
            }
            else if (Input.GetMouseButton(kMouseMidBtnKey) && _prevMouseHoldMidBtnPos != null)
            {
                // Mouse mid button holding
                DoMoveCamera(dt);
            }
        }


        private void ZoomCamera(float dt)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // Debug.Log($"scroll: {scroll}");
            if (Mathf.Abs(scroll) > float.Epsilon)
            {
                DoZoomCamera(scroll,dt);
            }
        }


        private void DoMoveCamera(float dt)
        {
            // do move by mouse position
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 offset = currentMousePos - _prevMouseHoldMidBtnPos.Value;
                
            Vector3 offset3D = new Vector3(offset.x,0,offset.y) * (_clashWorld.ClashCfg.kCameraMoveSpeed * dt);
            Vector3 currentCameraPos = _mainCamera.transform.position;
                
            if (_clashWorld.ClashCfg.kReverseMouseMidBtnDir)
            {
                _mainCamera.transform.position = currentCameraPos + offset3D;
            }
            else
            {
                _mainCamera.transform.position = currentCameraPos - offset3D;
            }

            // for next frame,hold prev frame data
            _prevMouseHoldMidBtnPos = currentMousePos;
        }


        private void DoZoomCamera(float scroll,float dt)
        {
            float offset = scroll * _clashWorld.ClashCfg.kCameraZoomSpeed * dt;
            Vector3 offset3D = _mainCamera.transform.forward * offset;
            _mainCamera.transform.position += offset3D;
        }

        public void Dispose()
        {
            
        }
    }
}