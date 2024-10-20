using System;
using UnityEngine;

namespace clash.gameplay
{
    public abstract class ClashBaseComponent : IDisposable
    {
        public virtual void Dispose()
        {
            Debug.Log("ClashBaseComponent::Dispose()");
        }
    }
}