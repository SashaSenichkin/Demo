using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public enum trackableType
{
    start,
    furniture,
    food,
    lipstick
}
public class Manager : MonoBehaviour {
    [SerializeField]private Text PromoText;
    Dictionary<trackableType, string> TextDict;
    public static Manager Instance;
    public List<Trackable> AllProxy;
    public GameObject Canvas;
    public Button DownloadButton;
    public Button ResetButton;
    private void Start()
    {
        Instance = this;
        TextDict = new Dictionary<trackableType, string>();
        TextDict.Add(trackableType.start, "Привет!");
        TextDict.Add(trackableType.food, "Приятного аппетита!");
        TextDict.Add(trackableType.furniture, "Оцени товар лицом!");
        TextDict.Add(trackableType.lipstick, "Здесь мог бы быть твой бренд!");
    }
    public void SetTextByType(trackableType type)
    {
        PromoText.text = TextDict[type];
    }
    public void OnBtnResetClick()
    {
        foreach (var item in AllProxy)
        {
            item.PositionReset();
        }
    }
    public void OnTrackableDetect()
    {
        ResetButton.gameObject.SetActive(true);
        DownloadButton.interactable = false;
        Canvas.SetActive(true);
    }
    public void OnTrackableLost()
    {
        Canvas.SetActive(false);
    }
    public void OnDownloadBtnClick()
    {
        Application.OpenURL("https://bigreal.ru/markers.html");
    }
}
