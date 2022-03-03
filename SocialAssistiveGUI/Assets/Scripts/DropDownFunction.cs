using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownFunction : MonoBehaviour
{
    public GameObject defaultList;
    public GameObject neckList;
    public GameObject lArmList;
    public GameObject rArmList;
    public GameObject lLegList;
    public GameObject rLegList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropDownFunctioning(int value)
    {
        if (value == 0)
        {
            defaultList.SetActive(true);
            neckList.SetActive(false);
            lArmList.SetActive(false);
            rArmList.SetActive(false);
            lLegList.SetActive(false);
            rLegList.SetActive(false);
        }
        if (value == 1)
        {
            defaultList.SetActive(false);
            neckList.SetActive(true);
            lArmList.SetActive(false);
            rArmList.SetActive(false);
            lLegList.SetActive(false);
            rLegList.SetActive(false);
        }
        if (value == 2)
        {
            defaultList.SetActive(false);
            neckList.SetActive(false);
            lArmList.SetActive(true);
            rArmList.SetActive(false);
            lLegList.SetActive(false);
            rLegList.SetActive(false);
        }
        if (value == 3)
        {
            defaultList.SetActive(false);
            neckList.SetActive(false);
            lArmList.SetActive(false);
            rArmList.SetActive(true);
            lLegList.SetActive(false);
            rLegList.SetActive(false);
        }
        if (value == 4)
        {
            defaultList.SetActive(false);
            neckList.SetActive(false);
            lArmList.SetActive(false);
            rArmList.SetActive(false);
            lLegList.SetActive(true);
            rLegList.SetActive(false);
        }
        if (value == 5)
        {
            defaultList.SetActive(false);
            neckList.SetActive(false);
            lArmList.SetActive(false);
            rArmList.SetActive(false);
            lLegList.SetActive(false);
            rLegList.SetActive(true);
        }
    }
}
