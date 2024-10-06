using UnityEngine.UIElements;

using UnityEngine;
namespace clash.gameplay
{
    public class GfxComponent : ClashBaseComponent
    {
        public UnityEngine.GameObject GO = null;
        
        public override void Dispose()
        {
            base.Dispose();
            Debug.Log("ClashBaseComponent::Dispose()");
            if (GO != null)
            {
                UnityEngine.GameObject.Destroy(GO);
                GO = null;
            }
        }
    }
}