using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show(string desc="", string objName="")
    {
        current.tooltip.SetText(desc, objName);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }

}
