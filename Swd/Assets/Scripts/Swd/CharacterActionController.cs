using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace swd.character
{
    public class CharacterActionController : MonoBehaviour
    {
        private SkinnedMeshRenderer _skinnedMeshRenderer = null;
        private Mesh _mesh = null;
        private int _blendShapeCnt = 0;
        
        private const float kDurationForEachBS = 0.08f;
        private float _totalAnimDuration = 0.0f;

        private float _elapsedTime = 0.0f;
        private bool _playing = true;
        
        private void Awake()
        {
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            _mesh = _skinnedMeshRenderer.sharedMesh;
            _blendShapeCnt = _mesh.blendShapeCount;
            
            _totalAnimDuration = kDurationForEachBS * _blendShapeCnt;
        }
        
        void Start()
        {
                
        }

        void Update()
        {
            if (_playing)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime > _totalAnimDuration)
                {
                    _elapsedTime -= _totalAnimDuration;
                }
                // float progress = GetAnimProgress(_elapsedTime);
                SetPoseWithProgress(_elapsedTime);
            }
        }

        public void Play()
        {
            _playing = true;
        }

        public void Pause()
        {
            _playing = false;
        }

        private float GetAnimProgress(float currentDuration)
        {
            float pct = currentDuration / _totalAnimDuration;
            return Mathf.Clamp(pct,0.0f,1.0f);
        }

        private void SetPoseWithProgress(float progress)
        {
            int currentBSIndex = (int)(progress / kDurationForEachBS);
            float currentBSPct = (progress - currentBSIndex * kDurationForEachBS) / kDurationForEachBS;

            for (int bsIdx = 0;bsIdx < _blendShapeCnt;bsIdx++) 
            {
                //float value = (bsIdx == currentBSIndex) ? currentBSPct: 0.0f;
                //value *= 100.0f;
                //float value = (bsIdx == currentBSIndex) ? 1.0f: 0.0f;
                //value *= 100.0f;

                float value = 0.0f;
                if (bsIdx == currentBSIndex - 1)
                {
                    value = 1.0f - currentBSPct;
                }
                else if (bsIdx == currentBSIndex)
                {
                    value = currentBSPct;
                }
                value *= 100.0f;


                _skinnedMeshRenderer.SetBlendShapeWeight(bsIdx,value);
            }
        }
    }
}

