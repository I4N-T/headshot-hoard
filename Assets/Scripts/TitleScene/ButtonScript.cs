using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image headSprite;
    public Sprite crossHairHead;
    public Sprite zombieHead;

    public void OnPointerEnter(PointerEventData eventData)
    {
        headSprite.sprite = crossHairHead;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        headSprite.sprite = zombieHead;
    }


}
