/*
 * essentials
 * 
 * yet another open-sourced mod for gorilla tag
 * just gives you cool stuff, tweaks, integrated mods, fps boosts, info board, etc.
 * 
 * so yeah
 */

using BepInEx;
using Essentials.Patches;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Essentials
{
    [BepInPlugin(PluginInfo.Package, PluginInfo.Name, PluginInfo.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInit);
        }

        void OnGameInit()
        {
            Startup.Boot();

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
                        Destroy(ConsoleObject);
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
            QualitySettings.antiAliasing = 0;
            QualitySettings.shadows = 0;
            QualitySettings.particleRaycastBudget = 0;
            QualitySettings.pixelLightCount = 0;
            QualitySettings.anisotropicFiltering = 0;
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.globalTextureMipmapLimit = 0;
            QualitySettings.lodBias = 0.0f;
            QualitySettings.pixelLightCount = 0;
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.vSyncCount = 0;
            QualitySettings.enableLODCrossFade = false;
            QualitySettings.maximumLODLevel = 0;
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
            #region Tweaks
            Patches.Boards.Tweaks();
            Debug.Log("[Essentials] sucessfully added tweaks");
            #endregion
        }

        void LateUpdate()
        {
            Boards.UpdateB();
        }

        public class FPS : MonoBehaviour
        {
            public static int FPSCount { get; private set; }
            private float delta = 0f;
            void Update()
            {
                delta += (Time.unscaledDeltaTime - delta) * 0.1f;
                FPSCount = Mathf.RoundToInt(1f / delta);
            }
        }
    }
}