using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 检测在快捷键上的物品数量
/// </summary>
public delegate void ItemCountChanged(Item item);

/// <summary>
/// 检测角色血量变化委托
/// </summary>
/// <param name="health"></param>
public delegate void HealthChanged(float health);

/// <summary>
/// 检测角色移除委托
/// </summary>
public delegate void CharacterRemoved();

/// <summary>
/// 检测可视化 栈 的委托
/// </summary>
public delegate void UpdateStackEvent();

/// <summary>
/// 检测极杀数量的 委托
/// </summary>
/// <param name="character"></param>
public delegate void KillConfirmed(Character character);

public class GameManager : MonoSingleton<GameManager>
{
    public event KillConfirmed KillConfirmedEvent;

    private Camera mainCamra;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Enemy currentTarget;

    private void Start()
    {
        mainCamra = Camera.main;
    }

    private void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamra.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Clickable"));
            if (hit.collider != null && hit.collider.tag == "Enemy")
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                currentTarget = hit.collider.GetComponent<Enemy>();

                player.MyTarget = currentTarget.Select();

                ////MainController 显示 Target 血条
                //MainController.Instance.ShowTargetFrame(currentTarget);

            }
            else
            {
                ////MainController 隐藏 Target 血条
                //MainController.Instance.HideTargetFrame();

                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                currentTarget = null;
                player.MyTarget = null;
            }
        }
        else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Clickable"));

            if (hit.collider != null)
            {
                IInteractable entity = hit.collider.gameObject.GetComponent<IInteractable>();
                if (hit.collider != null && (hit.collider.tag == "Enemy" || hit.collider.tag == "Interactable") && (hit.collider.gameObject.GetComponent<IInteractable>() == player.MyInteractable))
                {
                    entity.Interact();
                }
            }
        }
    }

    public void OnKillConfirmed(Character character)
    {
        KillConfirmedEvent?.Invoke(character);
    }
}
