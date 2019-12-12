using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EasyWiFi.Core;
using UnityEngine.Events;

namespace EasyWiFi.ClientControls
{
    [AddComponentMenu("EasyWiFiController/Client/UserControls/Button")]
    public class ButtonClientController : MonoBehaviour, IClientController
    {

        public string controlName = "Button1";
        public Sprite buttonPressedSprite;

        ButtonControllerType button;
        public Image currentImage;
        public Sprite buttonRegularSprite;
        string buttonKey;
        Rect screenPixelsRect;
        int touchCount;
        bool pressed;

        public UnityEvent action;
        private bool actionAlreadyInvoked;
        private bool isDeactivated;

        // Use this for initialization
        void Awake()
        {
            buttonKey = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_BUTTON, controlName);
            button = (ButtonControllerType)EasyWiFiController.controllerDataDictionary[buttonKey];

            currentImage = gameObject.GetComponent<Image>();
            buttonRegularSprite = currentImage.sprite;

            actionAlreadyInvoked = false;
            isDeactivated = false;


        }

        void Start()
        {
            screenPixelsRect = EasyWiFiUtilities.GetScreenRect(currentImage.rectTransform);
        }

        //here we grab the input and map it to the data list
        void Update()
        {
             mapInputToDataStream();
        }

        public void DeactivateButton()
        {
            isDeactivated = true;
            Debug.Log("Desactivation!");
            //currentImage.sprite = buttonRegularSprite;
        }

        public void mapInputToDataStream()
        {

            //reset to default values;
            //touch count is 0
            button.BUTTON_STATE_IS_PRESSED = false;
            pressed = false;

            //mouse
            if (Input.GetKey(KeyCode.Mouse0))
            {
                /*if (Input.mousePosition.x >= screenPixelsRect.x &&
                       Input.mousePosition.x <= (screenPixelsRect.x + screenPixelsRect.width) &&
                       Input.mousePosition.y >= screenPixelsRect.y &&
                       Input.mousePosition.y <= (screenPixelsRect.y + screenPixelsRect.height))
               {
                   pressed = true;
               }*/
               if (screenPixelsRect.Contains(Input.mousePosition))
               { 
                   pressed = true;
               }

            }

            //touch
            touchCount = Input.touchCount;

            if (touchCount > 0)
            {
                for (int i = 0; i < touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);

                    //touch somewhere on control
                    /*if (touch.position.x >= screenPixelsRect.x &&
                            touch.position.x <= (screenPixelsRect.x + screenPixelsRect.width) &&
                            touch.position.y >= screenPixelsRect.y &&
                            touch.position.y <= (screenPixelsRect.y + screenPixelsRect.height))
                    {

                        pressed = true;
                        break;
                    }*/
                    if (screenPixelsRect.Contains(touch.position))
                    {
                        pressed = true;
                        break;
                    }
                }
            }

            //show the correct image
            if (pressed && !isDeactivated)
            {
                button.BUTTON_STATE_IS_PRESSED = true;
                if(actionAlreadyInvoked == false)
                    action.Invoke();
                actionAlreadyInvoked = true;
                currentImage.sprite = buttonPressedSprite;
            }
            else
            {
                button.BUTTON_STATE_IS_PRESSED = false;
                actionAlreadyInvoked = false;
                currentImage.sprite = buttonRegularSprite;

            }

        }

    }

}