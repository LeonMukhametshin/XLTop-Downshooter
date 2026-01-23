using UnityEngine;

public interface IBuff
{
    public string id { get; }
    public Sprite icon { get; }
    public BuffType type { get; }

    public void Intitialize(BuffContainer buffContainer);
    public void Deinitialize();

    public void Update(float deltaTime);

    public IBuff Clone();
}   

public interface ITimeBuff : IBuff
{
    public float duration { get; }
    public float timer { get; }
}