using Essentials.Notifications;
using Meta.Voice.UnityOpus;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Essentials.Patches
{
    internal class Startup
    {
        public static void Boot()
        {
            Debug.Log(PluginInfo.ASCII);

            Debug.Log("loading ESSENTIALS info..");
            GameObject heading = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText");
            if (heading) heading.GetComponent<TextMeshPro>().text = "<color=#00FFFF>ESSENTIALS</color>";
            Debug.Log("updated ESSENTIALS info!");
            NotifiLib.SendNotification($"Welcome back, {PhotonNetwork.NickName}!");
        }
    }
}
