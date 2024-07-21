using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter0 : MoveGuide, ChapterManager
{
    //���� é�Ϳ� ����
    public void ThisChapterPlay()
    {
        HowToPlay();
    }

    //����ڿ��� ������ �˷��ش�.
    private void HowToPlay()
    {
        Debug.Log("����ڿ��� ���� �ܰ踦 ��������");
    }
    //Ʃ�긦 ����(Ư�� ���)�� ������ ��ġ�ϸ� �ߵ���
    private void IFPutTube()
    {
        //FadeOut�� ����ȴ�.
        GameManager.Instance.UiManager.PlayerFadeIn();
        //���� ���� ������ �̵��Ѵ�.
        GameManager.Instance.PlayStoryManager.MoveNextMainScene();
        GuideNextMovingArea(0);
    }

    protected override void NearPlayer()
    {
        BallNearPlayer();
        Debug.Log("�÷��̾�� ������.");
    }

    //�豸���� �÷��̾�� ������ ���� ��
    public void BallNearPlayer()
    {
        //Plan�� ���ؼ� ������ ���ش�.
        Debug.Log("plan�� ���ؼ� ��������");
        //������ ������ �̼� ��ø�� ȹ���Ѵ�.
        Debug.Log("plan��Ʈ�� ȹ���ϼ̽��ϴ�.");
        GameManager.Instance.PlanNote.gameObject.SetActive(true);
        //����é�ͷ� ����
        //GameManager.Instance.PlayStoryManager.PlayNextChapter();
    }
}
