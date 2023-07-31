using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private const string _loading = "Loading";
    private string _nextScene;
    private float _target;
    public float Target => _target;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartScene(string sceneName)
    {
        _nextScene = sceneName;
        LoadLevel(_loading);
    }
    private async void LoadLevel(string sceneName)
    {
        _target = 0;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; 
        do
        {
            if (sceneName != _loading)
            {
                await Task.Delay(100);
            }
            _target = asyncLoad.progress;
        }
        while (asyncLoad.progress < 0.9f);
        if (sceneName != _loading)
        {
            await Task.Delay(1000);
        }
        asyncLoad.allowSceneActivation = true;
        if (_nextScene != null)
        {
           LoadLevel(_nextScene);
        }
        _nextScene = null;
    }
    
}
