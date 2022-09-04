using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject publisherNode;

    [SerializeField]
    private GameObject rulesNode;

    [SerializeField]
    private GameObject continueNode;

    [SerializeField]
    private Material changeSceneMaterial;

    private float changeSceneAnimateDuration = 0.5f;

    private float changeSceneAmount = 0;

    private BreatheEffect backMenuTextAnim;

    public void StartGame()
    {
        StartCoroutine(ChangeSceneAnimation());
    }

    public void OpenRules()
    {
        mainMenu.SetActive(false);

        rulesNode.SetActive(true);

        continueNode.SetActive(true);
        backMenuTextAnim.LoopAnimation();
    }

    public void OpenPublisher()
    {
        mainMenu.SetActive(false);

        publisherNode.SetActive(true);

        continueNode.SetActive(true);
        backMenuTextAnim.LoopAnimation();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        rulesNode.SetActive(false);

        publisherNode.SetActive(false);

        backMenuTextAnim.StopLoopAnimation();
        continueNode.SetActive(false);

        mainMenu.SetActive(true);
    }

    private void Start()
    {
        backMenuTextAnim = continueNode.transform.GetChild(0).GetComponent<BreatheEffect>();

        changeSceneAmount = 1;
    }

    private IEnumerator ChangeSceneAnimation()
    {
        changeSceneAmount = 1;
        float nowTimer = 0;
        while (true)
        {
            nowTimer += Time.deltaTime;
            changeSceneAmount =  1 - Mathf.Lerp(0,1,nowTimer / changeSceneAnimateDuration);
            Debug.Log(changeSceneAmount);

            if (nowTimer > changeSceneAnimateDuration)
            {
                break;
            }
            else
            {
                yield return null;
            }
        }

        ChangeGameScene();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        changeSceneMaterial.SetFloat("_Amount", changeSceneAmount);
        Graphics.Blit(source, destination, changeSceneMaterial);
    }

    

    private void ChangeGameScene()
    {
        //SceneManager.LoadScene(1);
    }
}
