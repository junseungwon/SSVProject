using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Chapter0 : ChapterManager, ChapterInterFace
{
    public XRSocketInteractor socket = null;
    //���� é�Ϳ� ����
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
    //����ڿ��� ������ �˷��ش�.
    private void HowToPlay()
    {
        Debug.Log("����ڿ��� ���� �ܰ踦 ��������");
    }
    //Ʃ�긦 ����(Ư�� ���)�� ������ ��ġ�ϸ� �ߵ���
    public void IFPutTube()
    {
       // string name= socket.selectTarget.gameObject.name;
       // if (name == "Tube")
        {
            GameManager.Instance.UiManager.PlayerFadeInOut(0, 3, GameManager.Instance.PlayStoryManager.MoveNextMainScene);
            StartCoroutine(CorutineIsNextScene());
        }
        //FadeOut�� ����ǰ� ���� ���ξ����� �Ѿ��.
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

    //�豸���� �÷��̾�� ������ ���� ��
    public void BallNearPlayer()
    {
        //Plan�� ���ؼ� ������ ���ش�.
        Debug.Log("plan�� ���ؼ� ��������");
        //������ ������ �̼� ��ø�� ȹ���Ѵ�.
        Debug.Log("plan��Ʈ�� ȹ���ϼ̽��ϴ�.");
        GameManager.Instance.PlanNote.gameObject.SetActive(true);
        GameManager.Instance.PlayStoryManager.PlayNextChapter();
    }
}
