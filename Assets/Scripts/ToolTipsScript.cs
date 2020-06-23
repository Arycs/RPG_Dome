using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipsScript : MonoBehaviour
{
    private static ToolTipsScript instance;
    public static ToolTipsScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ToolTipsScript>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Vector3 offset;

    private CanvasGroup canvasGroup;

    [SerializeField]
    private Text text;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowToolTips(Item item)
    {
        transform.position = Input.mousePosition + offset;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        text.text = item.GetDescription();

    }

    public void HideToolTips()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
