using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreModel : Singleton<StoreModel>
{
    public Dictionary<int, Prop> propDic = new Dictionary<int, Prop>();

    public void Add(Prop prop)
    {
        if (!propDic.ContainsKey(prop.id))
        {
            propDic[prop.id] = prop;
        }
    }
}

public class Prop
{
    public int id;
    public string name;
    public string describe;
    public int price;

    public Prop(int id ,string name, string describe, int price)
    {
        this.id = id;
        this.name = name;
        this.describe = describe;
        this.price = price;
    }
}