
using UnityEngine;

public class Selection : MonoBehaviour
{
    public Material defaultMat;
    public Material closeMat;
    public Material rangeMat;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ChangeSelectionToDefault()
    {
        GetComponent<Renderer>().material = defaultMat;
    }
    public void ChangeSelectionToCloseRange()
    {
        GetComponent<Renderer>().material = closeMat;
    }
    public void ChangeSelectionToRange()
    {
        GetComponent<Renderer>().material = rangeMat;
    }
}
