using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Theme[] themes;
    [Range(0, 5)]
    public int activeTheme;
    public Transform rotatingFloorParent;   // use coroutine for rotating, subscribe rotating floor to coroutine 

    private bool isChangingTheme = false;

    // Start is called before the first frame update
    // void Start()
    // {
    // Maybe for checking the first Theme
    // }

    public bool IsThemeActive(Theme _thisTheme)
    {
        Debug.Log("current active theme is " + themes[activeTheme].title);
        isChangingTheme = false;
        if (_thisTheme.title == themes[activeTheme].title) { return true; }
        else return false;
        // return false;
    }

    public void ChangeTheme(Theme _newTheme)
    {
        if (_newTheme.title == themes[activeTheme].title || isChangingTheme) return;
        isChangingTheme = true;
        Theme oldTheme = themes[activeTheme];
        oldTheme.OnThemeChange();

        for (int i = 0; i < themes.Length; i++)
        {
            if (_newTheme == themes[i])
            {
                activeTheme = i;
            }
        }

        _newTheme.OnThemeChange();
    }
    public void ChangeTheme(int _newThemeInt)
    {
        if (_newThemeInt == activeTheme || isChangingTheme) return;
        isChangingTheme = true;
        Theme oldTheme = themes[activeTheme];
        oldTheme.OnThemeChange();

        themes[_newThemeInt].OnThemeChange();
        activeTheme = _newThemeInt;
    }
}
