using System;

namespace Tools.ObjectVisible
{
    public class GameObjectSlotModel : ISlotModel
    {
        private Guid id;
        private bool isSelected;
        private bool isVisible;
        private float alpha;
        private string name;

        public GameObjectSlotModel(Guid id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public event Action<float> onSetAlpha;
        public event Action<bool> onSetVisible;
        public event Action<bool> onSetSelect;

        public bool IsSelected => isSelected;
        public bool IsVisible => isVisible;
        public float Alpha => alpha;
        public string Name => name;
        public Guid Id => id;

        public void ChangeSelect()
        {
            isSelected = !isSelected;
            onSetSelect?.Invoke(isSelected);
        }

        public void ChangeVisible()
        {
            isVisible = !isVisible;
            onSetVisible?.Invoke(isVisible);
        }

        public void SetAlpha(float alpha)
        {
            this.alpha = alpha;
            onSetAlpha?.Invoke(alpha);
        }

        public override bool Equals(object obj)
        {
            if (obj is string id)
                return this.id.Equals(id);
            else
                return false;
        }
    }
}