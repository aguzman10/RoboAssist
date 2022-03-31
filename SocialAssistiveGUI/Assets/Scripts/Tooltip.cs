using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI nameField;

    public TextMeshProUGUI descriptionField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string desc = "", string objName = "")
    {
        //if the object doesn't have a description, don't desplay it
        if (string.IsNullOrEmpty(desc))
        {
            descriptionField.text = " ";
            descriptionField.gameObject.SetActive(false);
        }
        else
        {
            descriptionField.gameObject.SetActive(true);
            descriptionField.text = desc;
        }

        nameField.text = objName;

        //size the tooltip box according to the length of the text
        int nameLength = nameField.text.Length;
        int descriptionLength = descriptionField.text.Length;

        layoutElement.enabled = (nameLength > characterWrapLimit || descriptionLength > characterWrapLimit) ? true : false;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int nameLength = nameField.text.Length;
            int descriptionLength = descriptionField.text.Length;

            layoutElement.enabled = (nameLength > characterWrapLimit || descriptionLength > characterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, 0.5f);
        position += new Vector2(60, -40);
        transform.position = position;
    }
}
