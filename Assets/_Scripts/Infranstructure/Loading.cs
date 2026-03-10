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
            Destroy(m_instance.gameObject); 
            m_instance = null;
        }

        m_instance = this;
        gameObject.SetActive(false);
        DontDestroyOnLoad(this);
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
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        for (var i = 0; i < steps; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            m_loading.fillAmount += maxProgress / steps;
        }
    
        var operation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
                
        yield return operation;
        yield return new WaitForEndOfFrame();

        m_loading.fillAmount = 1f;

        gameObject.SetActive(false);
    }
}