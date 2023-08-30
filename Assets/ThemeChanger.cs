using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChanger : MonoBehaviour
{
    public ThemeManager themeManager;
    [SerializeField] float timeBetweenThemeChange;
    int themeCounter = 0;
    bool isWorking = false;

    // Start is called before the first frame update
    void Start()
    {
        themeCounter = themeManager.activeTheme;
    }

    public void startThemeChanger()
    {
        if (isWorking == false)
        {
            isWorking = true;
            adjustTheme();
        }
    }

    void adjustTheme()
    {
        if (themeCounter >= themeManager.themes.Length - 1)
            themeCounter = -1;

        themeCounter++;
        themeManager.ChangeTheme(themeCounter);
        Invoke("adjustTheme", timeBetweenThemeChange);

    }
}
