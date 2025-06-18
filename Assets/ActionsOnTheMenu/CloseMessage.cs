using System.Collections;
using System.Collections.Generic;
using PG;
using UnityEngine;

public class CloseMessage : MonoBehaviour
{
    public GameObject panelMessageDamage;

    public void ClosePanel()
    {
        panelMessageDamage.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        SkeletDamage.returnPanelButton();
        DamageHuman.activateBoolStatusShemHuman();
    }
    public void CloseSavetyPanel()
    {
        panelMessageDamage.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
}
