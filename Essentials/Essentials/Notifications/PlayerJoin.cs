﻿using HarmonyLib;
using Essentials.Notifications;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;

namespace Essentials.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class JoinPatch : MonoBehaviour
    {
        private static void Prefix(Player newPlayer)
        {
            if (newPlayer != oldnewplayer)
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=green>+</color><color=grey>] </color><color=green>" + newPlayer.NickName + "</color>");
                oldnewplayer = newPlayer;
            }
        }

        private static Player oldnewplayer;
    }
}