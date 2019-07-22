using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Menu[] menus;

    public void SetEnable(int id) {
        menus[id].Enable(true);
    }
    public void SetDisable (int id)
    {
        menus[id].Enable(false);
    }
    public void SwitchEnable(int id) {
        menus[id].menuObject.SetActive(!menus[id].menuObject.activeSelf);
    }
}

[Serializable]
public class Menu {
    public string Name;
    public GameObject menuObject;

    public void Enable(bool b = true) {
        menuObject.SetActive(b);
    }
}
