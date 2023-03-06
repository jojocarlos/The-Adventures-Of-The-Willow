using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private string sceneToUnload;

    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string[] additiveSceneNames;

    private void Start()
    {
        // Defina o valor inicial do slider e do texto como 0
        loadingBar.value = 0f;
        loadingText.text = "0%";
        // Inicie o processo de carregamento em uma rotina separada
        StartCoroutine(DelayedLoadGameSceneWithAdditives(1f));
    }
    private IEnumerator DelayedLoadGameSceneWithAdditives(float delayTime)
    {
        // Aguarde o tempo especificado
        yield return new WaitForSeconds(delayTime);

        // Inicie o processo de carregamento em uma rotina separada
        StartCoroutine(LoadGameSceneWithAdditives());
    }
    private IEnumerator LoadGameSceneWithAdditives()
    {
        // Comece a carregar a cena de jogo
        AsyncOperation gameSceneOperation = SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);

        // Desative a ativação automática para permitir o carregamento assíncrono
        gameSceneOperation.allowSceneActivation = false;

        // Enquanto a cena de jogo não estiver completamente carregada
        while (!gameSceneOperation.isDone)
        {
            // Atualize o valor do slider e o texto de carregamento
            float progress = Mathf.Clamp01(gameSceneOperation.progress / 0.9f); // O valor 0.9f é usado para permitir que a cena seja ativada apenas quando estiver 90% carregada
            loadingBar.value = progress;
            loadingText.text = $"Carregando {progress * 100f:0}%";

            // Se a cena de jogo estiver quase completamente carregada
            if (progress >= 0.9f)
            {
                // Carregue as cenas aditivas em segundo plano
                AsyncOperation[] additiveSceneOperations = new AsyncOperation[additiveSceneNames.Length];
                for (int i = 0; i < additiveSceneNames.Length; i++)
                {
                    additiveSceneOperations[i] = SceneManager.LoadSceneAsync(additiveSceneNames[i], LoadSceneMode.Additive);
                    additiveSceneOperations[i].allowSceneActivation = false;
                }

                // Enquanto as cenas aditivas não estiverem completamente carregadas
                bool allAdditivesLoaded = false;
                while (!allAdditivesLoaded)
                {
                    allAdditivesLoaded = true;
                    float additiveProgressSum = 0f;
                    for (int i = 0; i < additiveSceneOperations.Length; i++)
                    {
                        if (!additiveSceneOperations[i].isDone)
                        {
                            allAdditivesLoaded = false;
                            additiveProgressSum += additiveSceneOperations[i].progress;
                        }
                    }

                    // Atualize o valor do slider e o texto de carregamento
                    float additiveProgress = Mathf.Clamp01(additiveProgressSum / (0.9f * additiveSceneNames.Length)); // O valor 0.9f é usado para permitir que as cenas sejam ativadas apenas quando estiverem 90% carregadas
                    float totalProgress = Mathf.Max(progress, additiveProgress);
                    loadingBar.value = totalProgress;
                    loadingText.text = $"Carregando {totalProgress * 100f:0}%";

                    // Se todas as cenas aditivas estiverem quase completamente carregadas
                    if (additiveProgress >= 0.9f)
                    {
                        // Permita a ativação automática para todas as cenas
                        gameSceneOperation.allowSceneActivation = true;
                        for (int i = 0; i < additiveSceneOperations.Length; i++)
                        {
                            additiveSceneOperations[i].allowSceneActivation = true;
                        }

                        // Aguarde um breve momento antes de desativar a tela de carregamento
                        yield return new WaitForSeconds(1f);

                        // Desative a tela de carregamento
                        gameObject.SetActive(false);
                    }

                    yield return null;
                }
            }

            yield return null;
        }
    }
}
