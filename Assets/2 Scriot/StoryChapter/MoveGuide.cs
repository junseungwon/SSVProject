using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGuide : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePos;

    [SerializeField]
    private GameObject teleportGuidePrefab;

    private GameObject teleportGudieObj;

    protected IEnumerator CalculateNearPlayerPos(int num)
    {
        float distance = 100.0f;
        while (distance >= 1.0f)
        {
            distance = Vector3.Distance(GameManager.Instance.player.transform.position, movePos[num].position);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("플레이어가 일정 거리에 도달했습니다.");
        teleportGuidePrefab.SetActive(false);
        NearPlayer();
    }
    protected virtual void NearPlayer()
    {

    }

    protected void GuideNextMovingArea(int num)
    {
        teleportGudieObj = Instantiate(teleportGuidePrefab, movePos[num].transform.position, Quaternion.identity);
        StartCoroutine(CalculateNearPlayerPos(num));
    }
}
