using UnityEditor;
using UnityEngine;

public class GameObjectInEditorView : MonoBehaviour, IGameObjectView
{
    [SerializeField] private new Renderer renderer;

    public string Name => name;

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
        if (value)
            SceneVisibilityManager.instance.Show(renderer.gameObject, true);
        else
            SceneVisibilityManager.instance.Hide(renderer.gameObject, true);
    }
}
