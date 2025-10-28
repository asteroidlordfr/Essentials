
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static Essentials.Plugin;

namespace Essentials.Patches
{
    internal class Boards
    {
        // took this tos thing from the big iidk

        private static bool acceptedTOS;
        public static void Tweaks()
        {
            GameObject RoomObject = GameObject.Find("Miscellaneous Scripts").transform.Find("PrivateUIRoom_HandRays").gameObject;
            if (RoomObject == null)
                return;

            HandRayController HandRayController = RoomObject.GetComponent<HandRayController>();
            PrivateUIRoom PrivateUIRoom = RoomObject.GetComponent<PrivateUIRoom>();

            if (!acceptedTOS && PrivateUIRoom)
            {
                HandRayController.DisableHandRays();
                PrivateUIRoom.StopForcedOverlay();

                RoomObject.SetActive(false);
                acceptedTOS = true;
            }
        }

        public static void UpdateB()
        {
            if (PhotonNetwork.InRoom)
            {
                GameObject titleData = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
                if (titleData) titleData.GetComponent<TextMeshPro>().richText = true;
                if (titleData) titleData.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Midline;
                if (titleData) titleData.GetComponent<TextMeshPro>().text = $"Thanks for using Essentials, your on version {PluginInfo.Version} and there is {PhotonNetwork.CountOfPlayersInRooms} players online.\n\nRoom Info:\n\nRoom: {PhotonNetwork.CurrentRoom}\nNickname: {PhotonNetwork.NickName}\nPlayers in room: {PhotonNetwork.CountOfPlayers}/10\nFPS: {FPS.FPSCount}";
            }
            else
            {
                GameObject titleData = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
                if (titleData) titleData.GetComponent<TextMeshPro>().richText = true;
                if (titleData) titleData.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Midline;
                if (titleData) titleData.GetComponent<TextMeshPro>().text = $"Thanks for using Essentials, your on version {PluginInfo.Version} and there is {PhotonNetwork.CountOfPlayersInRooms} players online.\n\nRoom Info:\n<color=red>You must be in a room to fetch room information.";
            }
        }
    }
}
