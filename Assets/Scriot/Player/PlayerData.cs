using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //�÷��̾� ���� ����, �����, �񸶸�
    private int hp;
    public int Hp { get { return hp; } set { hp = value; GameManager.Instance.UiManager.PlayerInformTextChange(); } }
   
    private int hungry;
    public int Hungry { get { return hungry; } set { hungry = value; GameManager.Instance.UiManager.PlayerInformTextChange(); } }

    private int thirsty;
    public int Thirsty { get { return thirsty; } set { thirsty = value; GameManager.Instance.UiManager.PlayerInformTextChange(); } }

    private void Start()
    {
        GameManager.Instance.PlayerData = this;
    }
}
