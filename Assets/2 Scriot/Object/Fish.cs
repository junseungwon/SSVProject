using UnityEngine;

public class Fish : MonoBehaviour
{
    public GetAction GetAction = null;
    public int itemCode = 0;
    public int state = 0;

    public GameObject tranObj = null;
    //물체랑 접촉했을 때 아이템이 소모형 아이템이면 흡수 아니면 흡수안함
    private void OnCollisionEnter(Collision collision)
    {
        //해당 아이템이 산딸기나 오디일 경우 섭취 가능하게
        if (collision.gameObject.tag == "Item")
        {
            SwichFishState(int.Parse(collision.gameObject.name), collision.gameObject);

        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    //해당 아이템이 산딸기나 오디일 경우 섭취 가능하게
    //    if(other.tag == "Item")
    //    {
    //    SwichFishState(int.Parse(other.gameObject.name), other.gameObject);

    //    }
    //}
    //물고기를 나무창으로 죽이고 난 후 잡을 수 있게 만듬
    //나무가지로 꽂음 
    //꼬치 물고기가 됨
    //물고기를 모닥불 근처에 꽂아둠 꽂으면 천천히 물고기가 익어가고(메테리얼 override)
    //물고기가 다익었으면 다익었다고 표시가 나오고 
    //물고기를 입에다가 닿으면 먹어짐

    private void SwichFishState(int codeName, GameObject obj)
    {
        switch (state)
        {  
            case (int)FishState.Normal:
                DeadFish(codeName, obj);
                break;
            case (int)FishState.Dead:
                SkewerFish(codeName, obj);
                break;
            case (int)FishState.Skewer:
                BarbecuedFish(codeName, obj);
                break;
        }
    }
    private void Dead()
    {
        Debug.Log(gameObject.name);
        tranObj.transform.localRotation = Quaternion.Euler(0,180,0);
    }
    private void DeadFish(int codeName, GameObject obj)
    {
        //기본 상태일 경우 죽은 물고기로 변경
        if (codeName == 150)
        {
            Debug.Log("물고기를 잡으셨습니다.");
            //해당 코드를 전달
            itemCode = codeName;
            //목마름이랑 배부름 수치 증가 
            //이벤트를 실행
            if (GetAction != null)
            {
                GetAction();
            }
            GetAction = null;
            state = (int)FishState.Dead;
            Dead();
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    //나무 찔리는 꼬챙이 상태
    private void SkewerFish(int codeName, GameObject obj)
    {
        //나무가지에 접근
        if (codeName == 100)
        {
            Debug.Log("나무가지에 물고기가 꽂아짐");
            obj.name = "250";
            transform.parent = obj.transform;
            //물고기가 막대기 자식으로 이동하고 rotation이랑 position변경됨
            transform.localPosition = new Vector3(0, 1.5f, 0.139f);
            transform.localRotation = Quaternion.Euler(0, 0, -180);
            state = (int)FishState.Skewer;
        }
    }
    private void BarbecuedFish(int codeName, GameObject obj)
    {
        Debug.Log(codeName);
        //모닥불에 접근 
        if (codeName == 160)
        {
            Debug.Log("베이쿡 된 상태");
            //물고기의 메테리얼이 변경됨
            Material[] materials = transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials;
            //물체의 알파값만 변경하고 다시 return
            Color color = materials[1].color;
            color.a = 1f;
            materials[1].color = color;
            transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = materials;
            //이벤트를 실행
            state = (int)FishState.Barbecued;
            PlayerMouth mouth = GameManager.Instance.player.transform.GetChild(5).gameObject.GetComponent<PlayerMouth>();
            mouth.gameObject.SetActive(true);
        }
    }
    private enum FishState
    {
        Normal, Dead, Skewer, Barbecued, Eat
    }
}
