using GorillaNetworking;
using Photon.Pun;
using System;
using TMPro;
using UnityEngine;

namespace Essentials.Patches
{
    internal class Boards : MonoBehaviourPunCallbacks
    {
        static TextMeshPro text;
        float delta;
        int fps;
        float timer;
        static float people;

        public static bool starttime;

        public static void Sequence()
        {
            var obj = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
            text = obj.GetComponent<TextMeshPro>();
            text.richText = true;
            text.alignment = TextAlignmentOptions.Midline;
            people = PhotonNetworkController.Instance.TotalUsers() + 1000; // slightly not accurate but i couldn't get it to show the right player count, it would only show the first time it updated 
            starttime = true;
        }

        void LateUpdate()
        {

        }
    }
}