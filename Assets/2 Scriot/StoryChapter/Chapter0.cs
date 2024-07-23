using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter0 : MoveGuide, ChapterManager
{
    //���� é�Ϳ� ����
    public void ThisChapterPlay()
    {
        HowToPlay();
        IFPutTube();
    }

    //����ڿ��� ������ �˷��ش�.
    private void HowToPlay()
    {
        Debug.Log("����ڿ��� ���� �ܰ踦 ��������");
    }
    //Ʃ�긦 ����(Ư�� ���)�� ������ ��ġ�ϸ� �ߵ���
    public void IFPutTube()
    {
        //FadeOut�� ����ǰ� ���� ���ξ����� �Ѿ��.
        GameManager.Instance.UiManager.PlayerFadeInOut(0, 3, GameManager.Instance.PlayStoryManager.MoveNextMainScene);
        StartCoroutine(CorutineIsNextScene());
    }
    private IEnumerator CorutineIsNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        while (currentSceneIndex == 0)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(CalculateNearPlayerPos(0));
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
