using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class languageText : MonoBehaviour
{
    public int language;
    public string[] text;
    private TextMeshProUGUI textline;

    private void Start()
    {
        textline = GetComponent<TextMeshProUGUI>();
        language = YandexGame.savesData.lg;
        textline.text = "" + text[language];
    }
}
