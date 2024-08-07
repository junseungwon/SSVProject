using System;
using System.Collections;
using UnityEngine;

public class PlayMoveGuideManager : MonoBehaviour
{
    [Header("é�� ������ �迭")]
    //��é�͵� �迭
    public Transform[] chapter0MovePos = new Transform[5];
    public Transform[] chapter1MovePos = new Transform[5];
    public Transform[] chapter2MovePos = new Transform[5];
    public Transform[] chapter3MovePos = new Transform[5];
    public Transform[] chapter4MovePos = new Transform[5];

    [Header("�ڷ���Ʈ�� ����� ������Ʈ")]
    [SerializeField]
    private GameObject teleportGuidePrefab = null;

    private GameObject teleportGudieObj = null;
    private void Awake()
    {
        GameManager.Instance.PlayMoveGuideManager = this;
    }
    private IEnumerator CalculateNearPlayerPos(int num, Action action, Transform[] transArray)
    {
        float distance = 100.0f;
        while (distance >= 1.0f)
        {
            distance = Vector3.Distance(GameManager.Instance.player.transform.position, transArray[num].position);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("�÷��̾ ���� �Ÿ��� �����߽��ϴ�.");
        teleportGudieObj.SetActive(false);
        action.Invoke();
    }

    public void GuideNextMovingArea(int num, Action action)
    {
        Debug.Log("������ ���̵尡 ����Ǿ����ϴ�.");
        Transform[] array = ClassifyMoveArray(GameManager.Instance.PlayStoryManager.chapterStep);
        if (teleportGudieObj== null)
        {
           teleportGudieObj= Instantiate(teleportGuidePrefab);
           teleportGudieObj.SetActive(true);
            Debug.Log("����");
        }
        else
        {
            Debug.Log("���");
            teleportGudieObj.SetActive(true);
        }
        //������Ʈ ��ġ �� ȸ���� ����
        teleportGudieObj.transform.position = array[num].position;
        teleportGudieObj.transform.rotation = array[num].rotation;
        StartCoroutine(CalculateNearPlayerPos(num, action, array));
    }
    //�ش�Ǵ� �迭�� return 
    private Transform[] ClassifyMoveArray(int num)
    {
        switch (num)
        {
            case 0:
                return chapter0MovePos;
            case 1:
                return chapter1MovePos;
            case 2:
                return chapter2MovePos;
            case 3:
                return chapter3MovePos;
            case 4:
                return chapter4MovePos;
            default:
                Debug.LogError("�˼�����");
                return null;
        }
    }

}
