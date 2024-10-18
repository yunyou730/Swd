using UnityEngine.UIElements;

using UnityEngine;
namespace clash.gameplay
{
    public class GfxComponent : ClashBaseComponent
    {
        public UnityEngine.GameObject GO = null;
        
        public void SetVisible(bool bVisible)
        {
            GO.SetActive(bVisible);
        }
        
        public void SetPosition(Vector3 pos)
        {
            GO.transform.position = pos;
        }

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