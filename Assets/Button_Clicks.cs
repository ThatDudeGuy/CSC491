using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Clicks : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public void click_inventory(){
        
    }

    public void click_resume(){
        
    }

    public void click_quit(){
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("In the button: "+eventData);
        print(eventData.pointerEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Out of the button: "+eventData);
    }
    // Start is called before the first frame update
    // void Start()
    // {
    //     button.
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
