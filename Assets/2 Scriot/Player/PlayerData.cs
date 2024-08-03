using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : MonoBehaviour
{
    //플레이어 정보 생명, 배고픔, 목마름
    private int hp = 1000;
    public int Hp { get { return hp; } set { hp = value; } }

    private float hungry = 1000;
    public float Hungry
    {
        get { return hungry; }
        set { hungry = value; GameManager.Instance.UiManager.PlayerInformImageValueChange((int)PlayerDataState.Hungry, Hungry); }
    }

    private float thirsty = 1000;
    public float Thirsty
    {
        get { return thirsty; }
        set { thirsty = value; GameManager.Instance.UiManager.PlayerInformImageValueChange((int)PlayerDataState.Thirsty, Thirsty); }
    }

    private void Awake()
    {
        GameManager.Instance.PlayerData = this;
        GameManager.Instance.player = this.gameObject;
    }

    public float reduceSpeed = 1.0f;
    
    //배고픔과  목마름 수치 감소
    public void ReduceNumerical()
    {
        StartCoroutine(CorutineReduceNumerical());
    }

    //계속해서 수치들 감소
    private IEnumerator CorutineReduceNumerical()
    {
        while (reduceSpeed > 0)
        {
            //배고픔 목마름 수치 감소

            Hungry -= reduceSpeed;
            Thirsty -= reduceSpeed;
            //Debug.Log(Hungry);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
public enum PlayerDataState
{
    Hungry, Thirsty
}
