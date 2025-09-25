using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;
[CSharpCallLua]
public delegate void CustomCall();
public class GameManager : SingletonMono<GameManager>
{
    public Text score;
    public List<WeaponDataSO> allWeaponData;
    public Transform weaponFather;

    public GameObject weaponPrefab;
    CustomCall call;
    void Start()
    {
        call = LuaMgr.GetInstance().Global.Get<CustomCall>("ShowShopPanel");
        EventCenter.Instance.AddListener("AddWeapon", AddWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = SharedData.gameScore.Data.ToString();
        if (SharedData.gameScore.Data % 10 == 0 && SharedData.gameScore.Data != 0)
        {
            call();
            Time.timeScale = 0;
        }
    }
    void OnDisable()
    {
        EventCenter.Instance.RemoveListener("AddWeapon", AddWeapon);
    }
    public int GetGameScore()
    {
        return SharedData.gameScore.Data;
    }
    public void ReleaseScore()
    {
        SharedData.gameScore.Data -= 10;
    }
    public void AddWeapon() //GameDataCenter中用字典存储名字为键
    {
        Debug.Log("添加武器");
        GameObject obj = Instantiate(weaponPrefab);
        obj.transform.SetParent(weaponFather, false);
    }

}
