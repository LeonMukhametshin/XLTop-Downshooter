using System;
using UnityEngine;

[Serializable]
public abstract class BaseBuff : IBuff
{
    [field: SerializeField] public string Id { get; private set; }

    protected BuffContainer container { get; private set; }

    public BaseBuff() { }

    public BaseBuff(string id)
    {
        Id = id;
    }

    public void Intitialize(BuffContainer container)
    {
        this.container = container;

        OnInitialize();
    }

    protected virtual void OnInitialize() { }

    public void Deinitialize()
    {
        OnDeinitializing();

        container.Remove(this);
        container = null;
    }

    protected virtual void OnDeinitializing() { }


    public virtual void Update(float deltaTime) { }

    public abstract IBuff Clone();
}