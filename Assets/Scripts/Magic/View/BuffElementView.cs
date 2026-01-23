using UnityEngine;
using UnityEngine.UI;

public class BuffElementView : MonoBehaviour
{
    [SerializeField] private Image m_iconImage;
    [SerializeField] private Image m_timerImage;

    private IBuff m_buff;

    public void Initialize(IBuff buff)
    {
        m_buff = buff;
        gameObject.SetActive(true);
        m_iconImage.sprite = buff.icon;
        m_timerImage.fillAmount = 1;
    }

    public void DeInitilize()
    {
        m_buff = null;
        gameObject.SetActive(false);
        m_timerImage.fillAmount = 0;
    }


    private void Update()
    {
        if(m_buff is ITimeBuff timeBuff)
        {
            m_timerImage.fillAmount = timeBuff.timer / timeBuff.duration;
        }
    }
}
