using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MagicSystem : MonoBehaviour
{
    public event Action<MagicState> StateChanged;
    public event Action SpellCanceled;
    public event Action<IReadOnlyList<ElementType>> ElementChanged
    {
        add => spellPreporation.elementsChanged += value;
        remove => spellPreporation.elementsChanged -= value;
    }

    [SerializeField] private MagicConfig m_config;
    [SerializeField] private MouseResolver m_mouseResolver;

    private MagicState m_state;
    private SpellPreporation m_spellPreporation;

    public MagicState state
    {
        get => m_state;
        set
        {
            if (m_state != value)
            {
                m_state = value;
                StateChanged?.Invoke(m_state);
            }
        }
    }

    private SpellCaster m_custer;

    private SpellPreporation spellPreporation =>
        m_spellPreporation ??= new SpellPreporation(m_config);

    private void OnEnable() =>
          spellPreporation.overflowReccured += CancelSpell;

    private void OnDisable() =>
          spellPreporation.overflowReccured -= CancelSpell;

    private void Awake()
    {
        m_custer = new(transform);
    }

    private void CancelSpell()
    {
        if(state is MagicState.Preporation)
        {
            spellPreporation.Clear();
            SpellCanceled?.Invoke();

            StartCooldown();
        }
    }

    private Coroutine m_cooldownCoroutine;

    private void StartCooldown()
    {
        if(m_cooldownCoroutine is not null)
        {
            StopCoroutine(m_cooldownCoroutine);
        }

        m_cooldownCoroutine = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        state = MagicState.Cooldown;
        yield return new WaitForSeconds(m_config.cancelCooldown);
        state = MagicState.Idle;

        m_cooldownCoroutine = null;
    }

    public void AddElement(ElementType element) 
    {
        if(state is MagicState.Cooldown or MagicState.Casting)
        {
            return;
        }

        m_spellPreporation.AddElement(element);
        state = MagicState.Preporation;
    }

    public void TryCastSpell()
    {
        if(state is not MagicState.Preporation)
        {
            return;
        }

        if(spellPreporation.TryGetSpell(out var spell))
        {
            state = MagicState.Casting;

            m_custer.Cast(spell, m_mouseResolver.GetCursoureWorldPosition().Value);

            spellPreporation.Clear();
            state = MagicState.Idle;
        }
        else
        {
            CancelSpell();
        }
    }
}