using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GUI;

namespace Tools.ObjectVisible
{
    public class ObjectVisibleTools : EditorWindow
    {
        [SerializeField] private Sprite showEye;
        [SerializeField] private Sprite hideEye;
        [SerializeField] private Sprite checkBoxEnable;
        [SerializeField] private Sprite checkBoxDisable;

        [SerializeField] private Texture2D showEyeT;
        [SerializeField] private Texture2D hideEyeT;
        [SerializeField] private Texture2D checkBoxEnableT;
        [SerializeField] private Texture2D checkBoxDisableT;

        private List<ISlotModel> cashRenrerers = new();

        private float alpha = 1f;
        private bool isVisible = true;
        private bool isSelected = true;
        private bool isExpanded = false;

        [MenuItem("Tools/Object visible tools")]
        public static void ShowWindow()
        {
            GetWindow<ObjectVisibleTools>("Object visible tools");
        }

        private void OnGUI()
        {

            var gameObjects = Selection.gameObjects.Where(go => go.TryGetComponent(out Renderer renderer)).ToArray();
            if (gameObjects == null || gameObjects.Count() == 0)
            {
                cashRenrerers.Clear();
                return;
            }

            if (IsChangedSelectedList(gameObjects))
            {
                //В контроллер добавлять с атрибутом params
                Debug.Log("Change selected objects");
                cashRenrerers?.Clear();
                foreach (var gameObject in gameObjects)
                    cashRenrerers.Add(new GameObjectSlotModel(gameObject.GetComponent<Renderer>()));
            }

            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    alpha = GUILayout.HorizontalSlider(alpha, 0f, 1f, GUILayout.MinWidth(150f));
                }
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button(isVisible ? showEye.texture : hideEye.texture, GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            isVisible = !isVisible;
                            SetVisible(isVisible, cashRenrerers.ToArray());
                        }
                        if (GUILayout.Button(isSelected ? checkBoxEnable.texture : checkBoxDisable.texture, GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            isSelected = !isSelected;
                            SetSelected(isSelected, cashRenrerers.ToArray());
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Space(20);

            isExpanded = EditorGUILayout.Foldout(isExpanded, "Points");
            if (isExpanded)
            {
                GUILayout.BeginArea(new Rect(0, 60, Screen.width, cashRenrerers.Count * 30));
                {
                    PaintGameObjectSlots();
                }
                GUILayout.EndArea();
            }

            SetAlpha(cashRenrerers);
        }

        private void PaintGameObjectSlots()
        {
            for (int i = 0; i < cashRenrerers.Count; i++)
            {
                Color color = EditorStyles.iconButton.normal.textColor;

                Box(new Rect(0, 30 * i, Screen.width, 26), $"{cashRenrerers[i].Name}");
                if (Button(new Rect(3, 30 * i + 3, 20, 20), cashRenrerers[i].IsSelected ? checkBoxEnable.texture : checkBoxDisable.texture)) //new Texture2D(18, 18)
                {
                    cashRenrerers[i].ChangeSelect();
                }
                if (Button(new Rect(Screen.width - 3 - 20, 30 * i + 3, 20, 20), cashRenrerers[i].IsVisible ? showEye.texture : hideEye.texture)) //new Texture2D(25, 25)
                {
                    cashRenrerers[i].ChangeVisible();
                }
            }
        }

        private void SetAlpha(List<ISlotModel> slots)
        {
            foreach (var slot in slots)
                if (slot.IsSelected)
                    slot.SetAlpha(alpha);
        }

        private void SetVisible(bool isVisible, params ISlotModel[] slots)
        {
            foreach (var slot in slots)
                if (slot.IsVisible != isVisible)
                    slot.ChangeVisible();
        }

        private void SetSelected(bool isSelected, params ISlotModel[] slots)
        {
            foreach (var slot in slots)
                if (slot.IsSelected != isSelected)
                    slot.ChangeSelect();
        }

        private bool IsChangedSelectedList(GameObject[] gameObjects)
        {
            if (cashRenrerers == null && gameObjects != null) return true;
            if (cashRenrerers.Count != gameObjects.Length) return true;

            for (int i = 0; i < cashRenrerers.Count; i++)
                if (!cashRenrerers[i].Equals(gameObjects[i]))
                    return true;

            return false;
        }
    }
}