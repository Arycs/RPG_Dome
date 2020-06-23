using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : Singleton<StoreController>
{
    public StoreWindow storeWindow;
    public void SaveProp(Prop prop)
    {
        StoreModel.Instance.Add(prop);
    }

    public Prop GetProp(int id)
    {
        return StoreModel.Instance.propDic[id];
    }

    public void ShowDrop(int propId)
    {
        storeWindow.dropText.text = StoreModel.Instance.propDic[propId].name;
        storeWindow.dropDescribe.text = StoreModel.Instance.propDic[propId].describe;
        storeWindow.dropPrice.text = StoreModel.Instance.propDic[propId].price.ToString();

    }
}
