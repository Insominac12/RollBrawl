using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
   public string tipToShow;
   private float timeToWait = 0.5f;
   public void OnPointerEnter(PointerEventData eventData)
   {
     StopAllCoroutines();
     StartCoroutine(StartTimer());
     Debug.Log("MERGE");
   }

   public void OnPointerExit(PointerEventData eventData)
   {
     StopAllCoroutines();
     HoverTipManager.OnMouseLoseFocus();
     Debug.Log("SPER CA MERGE");
   }

   private void ShowMessage()
   {
      HoverTipManager.OnMouseHover(tipToShow, Input.mousePosition);
   }

   private IEnumerator StartTimer()
   {
     yield return new WaitForSeconds(timeToWait);

     ShowMessage();

   }
}
