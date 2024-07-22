using System;
using System.Collections;
using UnityEngine;

public class Chapter0 : MoveGuide, ChapterManager
{
    //현재 챕터에 시작
    public void ThisChapterPlay()
    {
        HowToPlay();
        //GameManager.Instance.UiManager.PlayerFadeInOut(0, 3);
    }

    //사용자에게 사용법을 알려준다.
    private void HowToPlay()
    {
        Debug.Log("사용자에게 현재 단계를 설명해줌");
    }
    //튜브를 소켓(특정 장소)에 물건을 배치하면 발동함
    public void IFPutTube()
    {
        //FadeOut이 실행되고 다음 메인씬으로 넘어간다.
        GameManager.Instance.UiManager.PlayerFadeInOut(0, 3, GameManager.Instance.PlayStoryManager.MoveNextMainScene);
        StartCoroutine(CorutineIsNextScene());
    }
    private IEnumerator CorutineIsNextScene()
    {
        while (GameManager.Instance.PlayStoryManager.sceneNumber != 1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        CalculateNearPlayerPos(0);
    }
    protected override void NearPlayer()
    {
        BallNearPlayer();
        Debug.Log("플레이어와 근접함.");
    }

    //배구공이 플레이어와 가까이 갔을 때
    public void BallNearPlayer()
    {
        //Plan에 대해서 설명을 해준다.
        Debug.Log("plan에 대해서 설명해줌");
        //설명이 끝나면 미션 수첩을 획득한다.
        Debug.Log("plan노트를 획득하셨습니다.");
        GameManager.Instance.PlanNote.gameObject.SetActive(true);
        //다음챕터로 변경
        //GameManager.Instance.PlayStoryManager.PlayNextChapter();
    }
}
