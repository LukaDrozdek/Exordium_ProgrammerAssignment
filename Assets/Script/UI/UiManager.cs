using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject UiInventory;
    public GameObject UiEquipment;
    public GameObject UiAttributs;
    public GameObject UiButtonsPanel;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CloseAllUI", 0.01f);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            UiInventoryOpenClose();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UiEquipmentOpenClose();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UiAttributsOpenClose();
        }
    }
    public void UiInventoryOpenClose()
    {
        if(UiInventory.activeSelf)
        {
            UiInventory.SetActive(false);
            OpenUiButton();
        }
        else
        {
            UiInventory.SetActive(true);
            CloseUiButton();
        }
    }

    public void UiEquipmentOpenClose()
    {
        if (UiEquipment.activeSelf)
        {
            UiEquipment.SetActive(false);
            OpenUiButton();
        }
        else
        {
            UiEquipment.SetActive(true);
            CloseUiButton();
        }
    }

    public void UiAttributsOpenClose()
    {
        if (UiAttributs.activeSelf)
        {
            UiAttributs.SetActive(false);
            OpenUiButton();
        }
        else
        {
            UiAttributs.SetActive(true);
            CloseUiButton();
        }
    }

    public void CloseAllUI()
    {
        UiInventory.SetActive(false);
        UiEquipment.SetActive(false);
        UiAttributs.SetActive(false);
    }

    public void CloseUiButton()
    {
        
        UiButtonsPanel.SetActive(false);
    }

    public void OpenUiButton()
    {
        if (!UiInventory.activeSelf && !UiEquipment.activeSelf && !UiAttributs.activeSelf)
        {
            UiButtonsPanel.SetActive(true);
        }
;
    }
}
