using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image m_loading;

    private static Loading m_instance;

    private void Awake()
    {
        if(m_instance is not null)
        {
            if(m_instance.GetEntityId() != GetEntityId())
            {
                Destroy(gameObject);
            }

            return;
        }

        m_instance = this;
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);

        ServiceLocator.Register(this);
    }

    public void LoadScene(string nameScene)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(nameScene));
    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        m_loading.fillAmount = 0;

        const float maxProgress = 0.5f;
        const float steps = 10f;

        for (var i = 0; i < steps; i++)
        {
            yield return new WaitForSeconds(0.5f);

            m_loading.fillAmount += maxProgress / steps;
        }

        var operation = SceneManager.LoadSceneAsync(nameScene);
        yield return operation;

        m_loading.fillAmount = 1f;

        gameObject.SetActive(false);
    }
}