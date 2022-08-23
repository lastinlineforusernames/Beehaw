using UnityEngine;

public class MoreCreditsButton : MonoBehaviour
{
    [SerializeField] private GameObject[] creditsPages;
    private int creditsPageIndex = 0;

    public void NextCreditsPage()
    {
        creditsPages[creditsPageIndex].SetActive(false);
        if (creditsPageIndex + 1 < creditsPages.Length)
        {
            creditsPageIndex++;
        } else
        {
            creditsPageIndex = 0;
        }
        creditsPages[creditsPageIndex].SetActive(true);
    }
}
