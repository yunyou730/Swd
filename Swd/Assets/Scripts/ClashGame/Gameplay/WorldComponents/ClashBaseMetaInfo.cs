using System;
using UnityEngine;

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