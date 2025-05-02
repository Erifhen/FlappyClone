using UnityEngine;
using System.Collections.Generic;

public class MenuCntroller : MonoBehaviour
{
    public GameObject panelGacha;
    public GameObject panelStore;
    public GameObject panelSkin;
    public GameObject panelConfig;

    private Stack<GameObject> panelHistory = new Stack<GameObject>();
    private GameObject currentPanel;
    void Start()
    {
        OpenPanel(panelGacha);
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        if(currentPanel != null)
        {
            if(currentPanel !=panelToOpen)
            {
                currentPanel.SetActive(false);
                panelHistory.Push(currentPanel);
            }
        }
        panelToOpen.SetActive(true);
        currentPanel = panelToOpen;
    }

    public void Back()
    {
        if(panelHistory.Count > 0)
        {
            currentPanel.SetActive(false);
            currentPanel = panelHistory.Pop();
            currentPanel.SetActive(true);
        }
    }

        public void OpenStore()=> OpenPanel(panelStore);
        public void OpenSkin()=> OpenPanel(panelSkin);
        public void OpenConfig()=> OpenPanel(panelConfig);
        public void BackToGacha()=> OpenPanel(panelGacha);
}
