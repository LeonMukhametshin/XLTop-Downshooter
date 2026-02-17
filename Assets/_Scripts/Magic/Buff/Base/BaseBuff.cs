using System;
using UnityEngine;

[Serializable]
public abstract class BaseBuff : IBuff
{
    [field: SerializeField] public string id { get; private set; }

    protected BuffContainer container { get; private set; }

    public Sprite icon { get; private set; }

    public BuffType type { get; private set; }

    public BaseBuff() { }

    public BaseBuff(string id, Sprite icon, BuffType type)
    {
        this.id = id;
        this.icon = icon;
        this.type = type;
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