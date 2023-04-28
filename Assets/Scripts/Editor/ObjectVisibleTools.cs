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

            List<IGameObjectView> gameObjects = Selection.gameObjects
                .Where(go => go.TryGetComponent(out GameObjectInEditorView gameObject))
                .Select(go => go.GetComponent<IGameObjectView>()).ToList();
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
                    cashRenrerers.Add(new GameObjectSlotModel(gameObject.Name, gameObject.Name));
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
                            SetVisible(gameObjects, cashRenrerers);
                        }
                        if (GUILayout.Button(isSelected ? checkBoxEnable.texture : checkBoxDisable.texture, GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            isSelected = !isSelected;
                            SetSelected(cashRenrerers);
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
                    PaintGameObjectSlots(gameObjects);
                }
                GUILayout.EndArea();
            }

            SetAlpha(gameObjects, cashRenrerers);
        }

        private void PaintGameObjectSlots(List<IGameObjectView> slotView)
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
                    slotView[i].SetVisible(cashRenrerers[i].IsVisible);
                }
            }
        }

        private void SetAlpha(List<IGameObjectView> slotView, List<ISlotModel> slots)
        {
            for (int i = 0; i < slots.Count; i++)
                if (slots[i].IsSelected)
                {
                    slots[i].SetAlpha(alpha);
                    slotView[i].SetAlpha(alpha);
                }
        }

        private void SetVisible(List<IGameObjectView> slotView, List<ISlotModel> slots)
        {
            for (int i = 0; i < slots.Count; i++)
                if (slots[i].IsVisible != isVisible)
                {
                    slots[i].ChangeVisible();
                    slotView[i].SetVisible(slots[i].IsVisible);
                }
        }

        private void SetSelected(List<ISlotModel> slots)
        {
            foreach (var slot in slots)
                if (slot.IsSelected != isSelected)
                    slot.ChangeSelect();
        }

        private bool IsChangedSelectedList(List<IGameObjectView> gameObjects)
        {
            if (cashRenrerers == null && gameObjects != null) return true;
            if (cashRenrerers.Count != gameObjects.Count) return true;

            for (int i = 0; i < cashRenrerers.Count; i++)
                if (!cashRenrerers[i].Equals(gameObjects[i].Name))
                    return true;

            return false;
        }
    }
}