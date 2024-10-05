using System;
using UnityEngine;

namespace clash.gameplay
{
    public abstract class ClashBaseComponent : IDisposable
    {
        public void Dispose()
        {
            Debug.Log("ClashBaseComponent::Dispose()");
        }
    }
}