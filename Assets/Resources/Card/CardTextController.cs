using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class CardTextData
{
    public string cardTop;
    public string cardMiddle;
    public string cardBottom;
    public string cardImage;
}

public class CardTextController : MonoBehaviour
{
    [SerializeField] private Text CardTopText;
    [SerializeField] private Text CardMiddleText;
    [SerializeField] private Text CardBottomText;
    [SerializeField] private SpriteRenderer cardImageRenderer;
    [SerializeField] private Sprite[] cardSprites;

    public void UpdateCardText(string jsonData)
    {
        Debug.Log($"Received Card text {jsonData}");
        try
        {
            CardTextData textData = JsonUtility.FromJson<CardTextData>(jsonData);
            
            if (!string.IsNullOrEmpty(textData.cardTop) && CardTopText != null)
            {
                CardTopText.text = textData.cardTop;
            }
            
            if (!string.IsNullOrEmpty(textData.cardMiddle) && CardMiddleText != null)
            {
                CardMiddleText.text = textData.cardMiddle;
            }

            if (!string.IsNullOrEmpty(textData.cardBottom) && CardBottomText != null)
            {
                CardBottomText.text = textData.cardBottom;
            }

            if (!string.IsNullOrEmpty(textData.cardImage) && cardImageRenderer != null)
            {
                Sprite selectedSprite = Array.Find(cardSprites, sprite => sprite.name.Equals(textData.cardImage, StringComparison.OrdinalIgnoreCase));
                if (selectedSprite != null)
                {
                    cardImageRenderer.sprite = selectedSprite;
                }
                else
                {
                    Debug.LogWarning($"Sprite for {textData.cardImage} not found.");
                }
            }

            Debug.Log($"Card text updated - Top: {textData.cardTop}, Middle: {textData.cardMiddle}, Bottom: {textData.cardBottom}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error updating card text: {e.Message}");
            // Set default text if no parameters provided
            if (CardTopText != null) CardTopText.text = "Happy Birthday!";
            if (CardBottomText != null) CardBottomText.text = "Have a wonderful day!";
            if (CardBottomText != null) CardBottomText.text = "Love Jane!";
        }
    }
}