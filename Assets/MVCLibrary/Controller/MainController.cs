using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : Singleton<MainController>
{
    public MainWindow mainWindow;

    private Enemy target;

    public void SetValue(float hp, float maxHp, float mana, float maxMana)
    {
        mainWindow.hpImage.fillAmount = hp / maxHp;
        mainWindow.mpImage.fillAmount = mana / maxMana; 
    }

    public void ShowTargetFrame(Enemy current)
    {
        target = current;
        mainWindow.target.SetActive(true);
        mainWindow.targetHead.sprite= current.MyHead;
        mainWindow.targetHp.fillAmount = current.MyHp / current.MyMaxHp; 
    }

    public void HideTargetFrame()
    {
        mainWindow.target.SetActive(false);
    }

    public void UpdateTargetFrame(float hp)
    {
        mainWindow.targetHp.fillAmount = hp / target.MyMaxHp;
    }
}
