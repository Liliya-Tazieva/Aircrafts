using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GUIclass : MonoBehaviour
{
    public Canvas MenuCanvas;
    public CanvasGroup Explanation;
    public Slider MinVelocity;
    public Slider MaxVelocity;
    public Slider AngularVelocity;
    public CanvasGroup PopupTextCanvas;
    private PopupMenu menu;

    public void StartAgain()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitFromMainScene()
    {
        SceneManager.LoadScene("ExitMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OK()
    {
        var popupMenu = MenuCanvas.GetComponent<PopupMenu>();
        popupMenu.minVelocity = MinVelocity.value;
        popupMenu.maxVelocity = MaxVelocity.value;
        popupMenu.angularVelocity = AngularVelocity.value;
        MinVelocity.value = MinVelocity.minValue;
        MaxVelocity.value = MaxVelocity.minValue;
        AngularVelocity.value = AngularVelocity.maxValue/2;
        HidePopupText();
        popupMenu.Hide();
    }

    public void GotIt()
    {
        Explanation.alpha = 0f;
        Explanation.blocksRaycasts = false;
    }

    private void ShowPopupText()
    {
        PopupTextCanvas.alpha = 1f;
        PopupTextCanvas.blocksRaycasts = true;
    }

    private void HidePopupText()
    {
        PopupTextCanvas.alpha = 0f;
        PopupTextCanvas.blocksRaycasts = false;
    }

    void Start()
    {
        HidePopupText();
        menu = MenuCanvas.GetComponent<PopupMenu>();
    }

    void Update()
    {
        if (Input.GetKeyDown("h") && !menu.Hidden)
        {
            ShowPopupText();
        }
    }
}
