public interface IGameObjectView
{
    string Name { get; }
    void SetAlpha(float alpha);
    void SetVisible(bool value);
}