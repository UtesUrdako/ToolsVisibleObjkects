using UnityEngine;

namespace Tools.ObjectVisible
{
    public class GameObjectInGameView : MonoBehaviour, IGameObjectView
    {
        private new Renderer renderer;

        public string Name => name;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        public void Initialize(ISlotModel model)
        {
            model.onSetVisible += SetVisible;
            model.onSetAlpha += SetAlpha;
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

        public void SetVisible(bool value)
        {
            renderer.enabled = !value;
        }
    }
}