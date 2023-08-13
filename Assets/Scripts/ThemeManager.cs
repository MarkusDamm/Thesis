using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Theme[] themes;
    [Range(0, 5)]
    public int activeTheme;
    [Range(1, 5)]
    public int themeChangeDuration = 3;
    public Transform rotatingFloorParent;

    private bool isChangingTheme = false;

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

        for (int i = 0; i < themes.Length; i++)
        {
            if (themes[i].title == oldTheme.title)
                themes[i].OnThemeChange();

            if (_newTheme == themes[i])
            {
                activeTheme = i;
                break;
            }
        }
        _newTheme.OnThemeChange();
        // determineGroundRotation(oldTheme, themes[activeTheme]);
    }
    /***
    / Can change multiple Themes
    ***/
    public void ChangeTheme(string _newThemeTitle)
    {
        if (_newThemeTitle == themes[activeTheme].title || isChangingTheme) return;
        isChangingTheme = true;
        Theme oldTheme = themes[activeTheme];

        int newTheme = 0;
        for (int i = 0; i < themes.Length; i++)
        {
            if (themes[i].title == oldTheme.title)
                themes[i].OnThemeChange();

            if (_newThemeTitle == themes[i].title)
            {
                newTheme = i;
                themes[i].OnThemeChange();
            }
        }
        activeTheme = newTheme;
        // determineGroundRotation(oldTheme, themes[activeTheme]);
    }
    public void ChangeTheme(int _newThemeInt)
    {
        if (_newThemeInt == activeTheme || isChangingTheme) return;
        isChangingTheme = true;
        Theme oldTheme = themes[activeTheme];

        for (int i = 0; i < themes.Length; i++)
        {
            if (themes[i].title == oldTheme.title)
                themes[i].OnThemeChange();
        }
        themes[_newThemeInt].OnThemeChange();
        activeTheme = _newThemeInt;
        // determineGroundRotation(oldTheme, themes[activeTheme]);
    }

    private void determineGroundRotation(Theme _oldTheme, Theme _newTheme)
    {
        if (_oldTheme is ThemeRotating oldTheme)
        {
            Debug.Log("Old Theme Rotates");
            // StartCoroutine(oldTheme.rotateFloor(rotatingFloorParent));
            oldTheme.handleGroundRotation();
        }
        else if (_newTheme is ThemeRotating newTheme)
        {
            Debug.Log("New Theme Rotates");
            // StartCoroutine(newTheme.rotateFloor(rotatingFloorParent));
            newTheme.handleGroundRotation();
        }
    }
}
