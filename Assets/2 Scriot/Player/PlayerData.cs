using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : MonoBehaviour
{
    //플레이어 정보 생명, 배고픔, 목마름
    private int hp = 1000;
    public int Hp { get { return hp; } set { hp = value; } }

    private int hungry = 1000;
    public int Hungry
    {
        get { return hungry; }
        set { hungry = value; GameManager.Instance.UiManager.PlayerInformImageValueChange((int)PlayerDataState.Hungry, Hungry); }
    }

    private int thirsty = 1000;
    public int Thirsty
    {
        get { return thirsty; }
        set { thirsty = value; GameManager.Instance.UiManager.PlayerInformImageValueChange((int)PlayerDataState.Thirsty, Thirsty); }
    }

    private void Start()
    {
        GameManager.Instance.PlayerData = this;
        GameManager.Instance.player = this.gameObject;
    }
}
public enum PlayerDataState
{
    Hungry, Thirsty
}
