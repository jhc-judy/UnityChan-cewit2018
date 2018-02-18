using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

public class ClickExit : MonoBehaviour {

    [SerializeField] public VRInteractiveItem m_InteractiveItem;
    [SerializeField] public Camera camera;
    private Text exit;

    private void Start()
    {
        exit = camera.GetComponentInChildren<Text>();
        exit.enabled = false;
    }

    public void OnEnable()
    {
        exit = camera.GetComponentInChildren<Text>();
        exit.enabled = true;
        m_InteractiveItem.OnClick += HandleClick;
    }

    private void HandleClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
