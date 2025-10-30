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
using static System.Net.Mime.MediaTypeNames;

namespace Essentials
{
    [BepInPlugin(PluginInfo.Package, PluginInfo.Name, PluginInfo.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        float delta;
        int fps;
        float timer;
        static float people;
        private TextMeshPro text;

        void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInit);
        }

        void OnGameInit()
        {
            var obj = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
            text = obj.GetComponent<TextMeshPro>();
            Startup.Boot();
        }

        void LateUpdate()
        {
            if (Boards.starttime)
            {
                delta += (Time.unscaledDeltaTime - delta) * 0.1f;
                fps = Mathf.RoundToInt(1f / delta);
                timer += Time.unscaledDeltaTime;
                if (timer >= 1f)
                {
                    timer = 0f;
                    string whattimeisit = DateTime.Now.ToString("h:mmtt").Replace(" ", "");
                    if (PhotonNetwork.InRoom)
                    {
                        text.text = $"Thanks for using Essentials, your on version {PluginInfo.Version} and there is {people} players online.\n\nRoom Info:\n\n{PhotonNetwork.CurrentRoom}\nNickname: {PhotonNetwork.NickName}\nPlayers in room: {PhotonNetwork.CurrentRoom.PlayerCount}/10\nFPS: {fps}\nPing: {PhotonNetwork.GetPing()}\nRegion: {PhotonNetwork.CloudRegion}\n\nTime: {whattimeisit}";
                    }
                    else
                    {
                        text.text = $"Thanks for using Essentials, your on version {PluginInfo.Version} and there is {people} players online.\n\nRoom Info:\n<color=red>You must be in a room to fetch room information.";
                    }
                }
            }
        }

    }
}