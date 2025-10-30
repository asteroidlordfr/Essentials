using Essentials.Notifications;
using Meta.Voice.UnityOpus;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using TMPro;

namespace Essentials.Patches
{
    internal class Startup
    {
        public static void Boot()
        {
            #region Startup Sequence
            Debug.Log(PluginInfo.ASCII);

            Debug.Log("loading ESSENTIALS info..");
            GameObject heading = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText");
            if (heading) heading.GetComponent<TextMeshPro>().text = "<color=#00FFFF>ESSENTIALS</color>";
            Debug.Log("updated ESSENTIALS info!");
            NotifiLib.SendNotification($"Welcome back, {PhotonNetwork.NickName}!");
            #endregion
            #region Console

            string ConsoleGUID = "goldentrophy_Console"; // Do not change this, it's used to get other instances of Console
            GameObject ConsoleObject = GameObject.Find(ConsoleGUID);

            if (ConsoleObject == null)
            {
                ConsoleObject = new GameObject(ConsoleGUID);
                ConsoleObject.AddComponent<Console.Console>();
            }
            else
            {
                if (ConsoleObject.GetComponents<Component>()
                    .Select(c => c.GetType().GetField("ConsoleVersion",
                        BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.FlattenHierarchy))
                    .Where(f => f != null && f.IsLiteral && !f.IsInitOnly)
                    .Select(f => f.GetValue(null))
                    .FirstOrDefault() is string consoleVersion)
                {
                    if (Console.ServerData.VersionToNumber(consoleVersion) < Console.ServerData.VersionToNumber(Console.Console.ConsoleVersion))
                    {
                        GameObject.Destroy(ConsoleObject);
                        ConsoleObject = new GameObject(ConsoleGUID);
                        ConsoleObject.AddComponent<Console.Console>();
                    }
                }
            }

            if (Console.ServerData.ServerDataEnabled)
                ConsoleObject.AddComponent<Console.ServerData>();

            Debug.Log("[Essentials] integrated console");
            #endregion
            #region Sodium
            Application.targetFrameRate = 144;
            QualitySettings.SetQualityLevel(1);
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.vSyncCount = 0;

            foreach (Camera camera in Camera.allCameras)
            {
                camera.allowMSAA = false;
                camera.focusDistance = 0;
                camera.farClipPlane = 50.0f;
                camera.focusDistance = 1f;
                camera.allowHDR = false;
            }
            Camera.main.farClipPlane = 50f;
            Camera.main.anamorphism = 0.0f;
            Debug.Log("[Essentials] integrated sodium");
            #endregion
            
            Boards.Sequence();
        }
    }
}
