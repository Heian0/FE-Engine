using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public Canvas actionCanvas;
    public Canvas abilityCanvas;
    public Canvas attackCanvas;
    public Canvas attackCanvasW;
    public Canvas attackCanvasS;
    public Canvas attackCanvasA;

    public Canvas cassiusUICanvas;
    public Canvas koroUICanvas;
    public Canvas cynthiaUICanvas;

    public Canvas enemyThiefUICanvas;
    public Canvas enemyArcherUICanvas;
    public Canvas enemyMSvntUICanvas;

    public GameObject attackButton;
    public GameObject waitButton;
    public GameObject itemsButton;
    public GameObject abilitiesButton;

    public List<Canvas> UICanvasList = new List<Canvas>();

    private void Start()
    {
        UICanvasList.Add(cassiusUICanvas);
        UICanvasList.Add(koroUICanvas);
        UICanvasList.Add(cynthiaUICanvas);
        UICanvasList.Add(enemyThiefUICanvas);
        UICanvasList.Add(enemyArcherUICanvas);
        UICanvasList.Add(enemyMSvntUICanvas);
    }

    public void EnableCanvas(Canvas canvas)
    {
        canvas.enabled = true;
    }

    public void DisableCanvas(Canvas canvas)
    {
        canvas.enabled = false;
    }

    public void EnableButton(GameObject button)
    {
        button.SetActive(true);
    }

    public void DisableButton(GameObject button)
    {
        button.SetActive(false);
    }

    public void DisableAllUnitUICanvases()
    {
        foreach (Canvas UICanvas in UICanvasList)
        {
            UICanvas.enabled = false;
        }
    }

    public void EnableAllUnitUICanvases()
    {
        foreach(Canvas UICanvas in UICanvasList)
        {
            UICanvas.enabled = true;
        }
    }

    public void RemoveUICanvasFromList(Unit unit)
    {
        switch(unit.name)
        {
            case "Cassius":

                UICanvasList.Remove(cassiusUICanvas);

                break;

            case "Koro":

                UICanvasList.Remove(koroUICanvas);

                break;

            case "Cynthia":

                UICanvasList.Remove(cynthiaUICanvas);

                break;

            case "Enemy Thief":

                UICanvasList.Remove(enemyThiefUICanvas);

                break;

            case "Enemy Archer":

                UICanvasList.Remove(enemyArcherUICanvas);

                break;

            case "Enemy Mortal Savant":

                UICanvasList.Remove(enemyMSvntUICanvas);

                break;
        }
    }
}
