using System;
using UnityEngine;

namespace clash.gameplay
{
    public class ClashBaseWorldComponent : IDisposable
    {
        public virtual void Dispose()
        {
            Debug.Log("ClashBaseWorldComponent::Dispose()");
        }
    }
}