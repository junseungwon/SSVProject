using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Chapter0 : ChapterManager, ChapterInterFace
{
    public XRSocketInteractor socket = null;
    //현재 챕터에 시작
    public void ThisChapterPlay()
    {
        HowToPlay();
      IFPutTube();
        //GameManager.Instance.PlayerController.leftDirController.selectEntered.AddListener(LeftGrabRockItem);
    }

    private void LeftGrabRockItem(SelectEnterEventArgs arg0)
    {
        Debug.Log("19999");
    }

    public void GrabRockItem(SelectEnterEventArgs arg0)
    {
        Debug.Log("hello");
    }
    //사용자에게 사용법을 알려준다.
    private void HowToPlay()
    {
        Debug.Log("사용자에게 현재 단계를 설명해줌");
    }
    //튜브를 소켓(특정 장소)에 물건을 배치하면 발동함
    public void IFPutTube()
    {
       // string name= socket.selectTarget.gameObject.name;
       // if (name == "Tube")
        {
            GameManager.Instance.UiManager.PlayerFadeInOut(0, 3, GameManager.Instance.PlayStoryManager.MoveNextMainScene);
            StartCoroutine(CorutineIsNextScene());
        }
        //FadeOut이 실행되고 다음 메인씬으로 넘어간다.
    }
    private IEnumerator CorutineIsNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        while (currentSceneIndex == 0)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.Instance.player.transform.position = new Vector3(335.0f, 96, 2136);
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, BallNearPlayer);
    }

    //배구공이 플레이어와 가까이 갔을 때
    public void BallNearPlayer()
    {
        //Plan에 대해서 설명을 해준다.
        Debug.Log("plan에 대해서 설명해줌");
        //설명이 끝나면 미션 수첩을 획득한다.
        Debug.Log("plan노트를 획득하셨습니다.");
        GameManager.Instance.PlanNote.gameObject.SetActive(true);
        GameManager.Instance.PlayStoryManager.PlayNextChapter();
    }
}
