public interface IBuff
{
    public string Id { get; }

    public void Intitialize(BuffContainer buffContainer);
    public void Deinitialize();

    public void Update(float deltaTime);

    public IBuff Clone();
}   