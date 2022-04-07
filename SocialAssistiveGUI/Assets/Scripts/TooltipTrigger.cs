using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Multiline()]
    public string desc;

    public string objName;

    private static LTDescr delay;

    public void setDescription(string desc)
    {
        this.desc = desc;
    }

    public void setName(string objName)
    {
        this.objName = objName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            TooltipSystem.Show(desc, objName);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}
