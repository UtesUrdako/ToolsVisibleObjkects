using UnityEditor;
using UnityEngine;

namespace Tools.ObjectVisible
{
    public class GameObjectSlotModel : ISlotModel
    {
        private Renderer renderer;

        private bool isSelected;
        private bool isVisible;

        public bool IsSelected => isSelected;
        public bool IsVisible => isVisible;
        public float Alpha => renderer.material.color.a;
        public string Name => renderer.name;

        public GameObjectSlotModel(Renderer renderer)
        {
            this.renderer = renderer;
        }

        public void ChangeSelect()
        {
            isSelected = !isSelected;
        }

        public void ChangeVisible()
        {
            isVisible = !isVisible;
            if (isVisible)
                SceneVisibilityManager.instance.Show(renderer.gameObject, true);
            else
                SceneVisibilityManager.instance.Hide(renderer.gameObject, true);
        }

        public void SetAlpha(float alpha)
        {
            if (renderer.material != null && !Mathf.Approximately(alpha, renderer.material.color.a))
                foreach (Material material in renderer.materials)
                {
                    Color color = new Color(material.color.r, material.color.g, material.color.b, alpha);
                    material.color = color;
                }
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObject go)
                return renderer.gameObject.Equals(go);
            else
                return false;
        }
    }
}