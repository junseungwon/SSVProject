using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : MonoBehaviour
{
    //�÷��̾� ���� ����, �����, �񸶸�
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
    
    //����İ�  �񸶸� ��ġ ����
    public void ReduceNumerical()
    {
        StartCoroutine(CorutineReduceNumerical());
    }

    //����ؼ� ��ġ�� ����
    private IEnumerator CorutineReduceNumerical()
    {
        while (reduceSpeed > 0)
        {
            //����� �񸶸� ��ġ ����

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
