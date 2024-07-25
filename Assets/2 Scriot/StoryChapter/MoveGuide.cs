using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGuide : MonoBehaviour
{
    public Transform[] movePos;

    [SerializeField]
    private GameObject teleportGuidePrefab;

    private GameObject teleportGudieObj;

    protected IEnumerator CalculateNearPlayerPos(int num, Action action)
    {
        float distance = 100.0f;
        while (distance >= 1.0f)
        {
            //Debug.Log(distance);
            distance = Vector3.Distance(GameManager.Instance.player.transform.position, movePos[num].position);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("플레이어가 일정 거리에 도달했습니다.");
        teleportGuidePrefab.SetActive(false);
        action.Invoke();
    }

    protected void GuideNextMovingArea(int num, Action action)
    {
        Debug.Log("움직임 가이드가 실행되었습니다.");
        teleportGudieObj = Instantiate(teleportGuidePrefab, movePos[num].transform.position, Quaternion.identity);
        teleportGudieObj.SetActive(true);
        StartCoroutine(CalculateNearPlayerPos(num, action));
    }
}
