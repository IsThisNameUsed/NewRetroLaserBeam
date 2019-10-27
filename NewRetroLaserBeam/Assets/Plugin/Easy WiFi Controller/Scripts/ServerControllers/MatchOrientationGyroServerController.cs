using UnityEngine;
using System.Collections;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ServerControls
{

    [AddComponentMenu("EasyWiFiController/Server/UserControls/Match Orientation Gyro")]
    public class MatchOrientationGyroServerController : MonoBehaviour, IServerController
    {
        public string control = "Gyro";
        public EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.Player1;
        //runtime variables
        GyroControllerType[] gyro = new GyroControllerType[EasyWiFiConstants.MAX_CONTROLLERS];
        int currentNumberControllers = 0;
        Quaternion orientation;
        Vector3 neutralOrientation;
        Vector3 rotationCorrections;
        bool firstInput = true;
        public GameObject gunsight;
        public float gunsightZStart;
        public Quaternion gunsightStartRotation;
        private float camZStart;
      
        void OnEnable()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;

            //do one check at the beginning just in case we're being spawned after startup and after the callbacks
            //have already been called
            if (gyro[0] == null && EasyWiFiController.lastConnectedPlayerNumber >= 0)
            {
                EasyWiFiUtilities.checkForClient(control, (int)player, ref gyro, ref currentNumberControllers);
            }
        }

        void OnDestroy()
        {
            EasyWiFiController.On_ConnectionsChanged -= checkForNewConnections;
        }

        void Awake()
        {
            Quaternion startOrientation = transform.rotation;
            Vector3 neutralOrientation = new Vector3(startOrientation.eulerAngles.x, startOrientation.eulerAngles.y, startOrientation.eulerAngles.z);

            gunsightZStart = gunsight.transform.position.z;
            camZStart = Camera.main.transform.position.z;
            gunsightStartRotation = gunsight.transform.rotation;
        }
        // Update is called once per frame
        void Update()
        {
            //iterate over the current number of connected controllers
            for (int i = 0; i < currentNumberControllers; i++)
            {
                if (gyro[i] != null && gyro[i].serverKey != null && gyro[i].logicalPlayerNumber != EasyWiFiConstants.PLAYERNUMBER_DISCONNECTED)
                {
                    mapDataStructureToAction(i);
                }
            }
            //Debug.Log(rotationCorrections);
        }

        public void CalculateCorrection(int index)
        {
            orientation.w = gyro[index].GYRO_W;
            orientation.x = gyro[index].GYRO_X;
            orientation.y = gyro[index].GYRO_Y;
            orientation.z = gyro[index].GYRO_Z;
            Debug.Log(gyro[index].GYRO_Z);
            Quaternion newRot = new Quaternion(orientation.x, orientation.y, orientation.z, orientation.w);
            Debug.Log(newRot);
            Vector3 conversion = new Vector3(newRot.eulerAngles.x, newRot.eulerAngles.y, newRot.eulerAngles.z);
            Debug.Log(conversion);
            float Xcorrection = neutralOrientation.x - conversion.x;
            float Ycorrection = neutralOrientation.y - conversion.y;
            float Zcorrection = neutralOrientation.z - conversion.z;
            rotationCorrections = new Vector3(Xcorrection, Ycorrection, Zcorrection);
            Debug.Log(rotationCorrections);
        }

        public void mapDataStructureToAction(int index)
        {
            orientation.w = gyro[index].GYRO_W;
            orientation.x = gyro[index].GYRO_X;
            orientation.y = gyro[index].GYRO_Y;
            orientation.z = gyro[index].GYRO_Z;

            if (firstInput && gyro[index].GYRO_Z!=0)
            {
                CalculateCorrection(index);
                firstInput = false;
            }

            Quaternion newRot = new Quaternion(orientation.x, orientation.y, orientation.z, orientation.w);
            Vector3 conversion = new Vector3(-(newRot.eulerAngles.x + rotationCorrections.x), -(newRot.eulerAngles.z + rotationCorrections.z), 0f);
            //Vector3 conversion = new Vector3(newRot.eulerAngles.x , -newRot.eulerAngles.z , 0f);
            transform.localRotation = Quaternion.Euler(conversion);

            //Gunsight freeze on Z axis
            float camZ = Camera.main.transform.position.z;
            gunsight.transform.position = new Vector3(gunsight.transform.position.x, gunsight.transform.position.y, gunsightZStart -(camZStart-camZ));
            gunsight.transform.rotation = gunsightStartRotation;
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref gyro, ref currentNumberControllers);
        }
    }

}
