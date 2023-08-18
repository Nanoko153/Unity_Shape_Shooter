using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneSystem : PersistentSingleton<SceneSystem>
{
    Canvas loadingCanvas;
    CanvasGroup loadingCanvasGroup;
    public string currentSceneName => SceneManager.GetActiveScene().name;
    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadTestRoom ()
    {
        LoadSceneAsync("Test_Scene");
    }

    void Start()
    {
        loadingCanvas = GetComponentInChildren<Canvas>();
        loadingCanvasGroup = GetComponentInChildren<CanvasGroup>();

        EventCenter.Instance.AddEventListener("ReturnMainMenu", ReturnMainMenuScene);
        EventCenter.Instance.AddEventListener("LoadGamePlayScene", LoadGamePlayScene);
        EventCenter.Instance.AddEventListener("LoadPlayerDataScene", LoadPlayerDataScene);
        EventCenter.Instance.AddEventListener("LoadGameOverRoomScene", LoadGameOverRoomScene);

        loadingCanvas.enabled = false;
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ReturnMainMenu", ReturnMainMenuScene);
        EventCenter.Instance.RemoveEventListener("LoadGamePlayScene", LoadGamePlayScene);
        EventCenter.Instance.RemoveEventListener("LoadPlayerDataScene", LoadPlayerDataScene);
        EventCenter.Instance.RemoveEventListener("LoadGameOverRoomScene", LoadGameOverRoomScene);
    }

    #region ���¼��س���
    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region ����GamePlay����
    public void LoadGamePlayScene()
    {
        LoadSceneAsync("GamePlay");
        GameManager.Instance.StartGamePlaying();
    }
    #endregion

    #region ����PlayerData����
    public void LoadPlayerDataScene()
    {
        LoadSceneAsync("Score");
    }
    #endregion

    #region ����GameOverRoom����
    public void LoadGameOverRoomScene()
    {
        LoadSceneAsync("GameOverRoom");
    }
    #endregion

    #region �������˵�
    public void ReturnMainMenuScene()
    {
        LoadSceneAsync("MainMenu");
    }
    #endregion

    #region ֱ�Ӽ��س���
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }

    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex,LoadSceneMode.Single);
    }
    #endregion


    #region �첽���س���
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    public void LoadSceneAsync(int sceneBuildIndex)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneBuildIndex));
    }

    IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        asyncLoad.allowSceneActivation = false;

        //�����л�UIЧ��
        loadingCanvas.enabled = true;
        loadingCanvasGroup.DOFade(1, 0.5f);
        yield return new WaitUntil(() => loadingCanvasGroup.alpha == 1);

        yield return new WaitForSeconds(0.5f);

        while(asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Activate the new scene
        asyncLoad.allowSceneActivation = true;

        //�ȴ�һ������ڹ��ɳ�����Ϣ
        yield return new WaitForSeconds(0.5f);

        //�����л�UIЧ��
        loadingCanvasGroup.DOFade(0, 0.5f);
        yield return new WaitUntil(() => loadingCanvasGroup.alpha == 0);
        loadingCanvas.enabled = false;

        yield return null;
    }

    IEnumerator LoadSceneAsyncCoroutine(int sceneBuildIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);

        asyncLoad.allowSceneActivation = false;

        //�����л�UIЧ��
        loadingCanvas.enabled = true;
        loadingCanvasGroup.DOFade(1, 0.5f);
        yield return new WaitUntil(() => loadingCanvasGroup.alpha == 1);

        yield return new WaitForSeconds(0.5f);

        while(asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Activate the new scene
        asyncLoad.allowSceneActivation = true;

        //�ȴ�һ������ڹ��ɳ�����Ϣ
        yield return new WaitForSeconds(0.5f);

        //�����л�UIЧ��
        loadingCanvasGroup.DOFade(0, 0.5f);
        yield return new WaitUntil(() => loadingCanvasGroup.alpha == 0);
        loadingCanvas.enabled = false;

        yield return null;
    }
    #endregion
}
