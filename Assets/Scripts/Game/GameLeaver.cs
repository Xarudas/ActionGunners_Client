using DarkRift;
using MeatInc.ActionGunnersShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeatInc.ActionGunnersClient.Game
{
    public class GameLeaver : MonoBehaviour
    {
        public void Leave()
        {
            using (Message message = Message.CreateEmpty(Tags.Game.LeaveRequest))
            using (Message message1 = Message.CreateEmpty(Tags.Room.LeaveRequest))
            {
                NetworkManager.Instance.Client.SendMessage(message, SendMode.Reliable);
                NetworkManager.Instance.Client.SendMessage(message1, SendMode.Reliable);
            }
            SceneManager.LoadScene("Main");
        }
    }
}
