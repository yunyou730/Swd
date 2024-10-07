using System;
using UnityEngine;
using UnityEngine.Animations;

namespace clash.gameplay
{
    public class ClashBaseMetaInfo : IDisposable
    {
        public virtual void Dispose()
        {
            Debug.Log("ClashBaseMetaInfo::Dispose()");
        }
    }
}