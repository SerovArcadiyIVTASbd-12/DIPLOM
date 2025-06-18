using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSavetyPanel : MonoBehaviour
{
    public GameObject savetyPanel;

    public void ClosePanel()
    {
        savetyPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
