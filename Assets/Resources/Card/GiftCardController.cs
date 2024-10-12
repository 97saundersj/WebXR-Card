using UnityEngine;

public class GiftCardController : MonoBehaviour
{
    [SerializeField] private string cardUrl = "https://www.amazon.co.uk/gp/r.html?C=SVULMK1YZBEX&M=urn:rtn:msg:202408152051151b2327a50da94a128b254b8fa5f0p0eu&R=2QOO8FXFZSC1E&T=C&U=https%3A%2F%2Fwww.amazon.co.uk%2Fg%2F6HYEGBSUS6EM8H%3Fref_%3Dpe_3434961_257669011_TC0301BT&H=SR7HZUA08MCQSMRBBUFFRHEFLPIA&ref_=pe_3434961_257669011_TC0301BT";

    void Start()
    {
    }

    void OnMouseDown()
    {
        OpenCardUrl();
    }

    void OpenCardUrl()
    {
        if (!string.IsNullOrEmpty(cardUrl))
        {
            Debug.Log($"Opening URL: {cardUrl}");
            Application.OpenURL(cardUrl);
        }
        else
        {
            Debug.LogWarning("Card URL is not set.");
        }
    }
}