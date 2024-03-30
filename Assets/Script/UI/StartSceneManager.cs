using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class StartSceneManager : MonoBehaviour
{
    public GameObject PressAnyButton;
    public GameObject Fade;
    public float fadeTime;

    private bool isButtonPressed = true;
    public GameObject startMenu;

    public float menuDistance;
    public float menuTime;

    public string sceneName;
    public Fade fadeOut;


    public int level;
    public Vector3 savePos;
    public List<string> sceneNames = new List<string>();

    void Start()
    {
        Fade.GetComponent<Image>().DOFade(0f, fadeTime).OnComplete(() =>
        {
            PressAnyButton.SetActive(true);
            var textMeshPro = PressAnyButton.GetComponent<TextMeshProUGUI>();
            // �����ʹ�õ��� TextMeshPro (���� TMP for UI)����ʹ���������д��룺
            // var textMeshPro = PressAnyButton.GetComponent<TextMeshPro>();

            textMeshPro.DOFade(1f, fadeTime);
            isButtonPressed = false;
        });
        var gameData = this.GetComponent<SaveGameData>();
        gameData.LoadFromJson();

        level = gameData.level;
        savePos = gameData.savePosition;

    }

    void Update()
    {
        // ������ⰴ�����벢�Ұ�ť��δ������
        if (Input.anyKeyDown && !isButtonPressed)
        {
            isButtonPressed = true;
            OnPressAnyButton();
        }
    }

    void OnPressAnyButton()
    {
        // �������д�������º�Ҫ�������߼�
        Debug.Log("Press Any Button is pressed!");
        PressAnyButton.SetActive(false);
        startMenu.transform.DOMoveY(menuDistance,menuTime);
    }

    public void StartGamePlay()
    {
        startMenu.SetActive(false);
        fadeOut.fadeImage.DOFade(1f, fadeOut.fadetime).OnComplete(() => SceneManager.LoadScene(sceneName));

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ContinueGamePlay()
    {
        
        var gameData = this.GetComponent<SaveGameData>();
        Debug.Log(gameData.SaveFileExists());
        if (gameData.SaveFileExists())
        {
            gameData.LoadFromJson();

            level = gameData.level;
            savePos = gameData.savePosition;

            startMenu.SetActive(false);
            fadeOut.fadeImage.DOFade(1f, fadeOut.fadetime).OnComplete(() => SceneManager.LoadScene(sceneNames[level]));

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            StartGamePlay();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}

