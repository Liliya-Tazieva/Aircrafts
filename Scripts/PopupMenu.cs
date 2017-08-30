using System.Runtime.InteropServices;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    public bool Hidden;
    public float minVelocity;
    public float maxVelocity;
    public float angularVelocity;
    private CanvasGroup canvasGroup;

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        Hidden = true;
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Hidden = false;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        minVelocity = 5f;
        maxVelocity = 15f;
        angularVelocity = 10f;
        Hide();
    }

    void Update()
    {
        
    }
}
