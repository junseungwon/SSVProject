using System;
using System.Collections;
using UnityEngine;

public class PlayMoveGuideManager : MonoBehaviour
{
    [Header("챕터 움직임 배열")]
    //각챕터들 배열
    public Transform[] chapter0MovePos = new Transform[5];
    public Transform[] chapter1MovePos = new Transform[5];
    public Transform[] chapter2MovePos = new Transform[5];
    public Transform[] chapter3MovePos = new Transform[5];
    public Transform[] chapter4MovePos = new Transform[5];

    [Header("텔로포트에 사용할 오브젝트")]
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
        Debug.Log("플레이어가 일정 거리에 도달했습니다.");
        teleportGudieObj.SetActive(false);
        action.Invoke();
    }

    public void GuideNextMovingArea(int num, Action action)
    {
        Debug.Log("움직임 가이드가 실행되었습니다.");
        Transform[] array = ClassifyMoveArray(GameManager.Instance.PlayStoryManager.chapterStep);
        if (teleportGudieObj== null)
        {
           teleportGudieObj= Instantiate(teleportGuidePrefab);
           teleportGudieObj.SetActive(true);
            Debug.Log("생성");
        }
        else
        {
            Debug.Log("사용");
            teleportGudieObj.SetActive(true);
        }
        //오브젝트 위치 및 회전값 설정
        teleportGudieObj.transform.position = array[num].position;
        teleportGudieObj.transform.rotation = array[num].rotation;
        StartCoroutine(CalculateNearPlayerPos(num, action, array));
    }
    //해당되는 배열을 return 
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
                Debug.LogError("알수없음");
                return null;
        }
    }

}
