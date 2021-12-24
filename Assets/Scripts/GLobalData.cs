using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GlobalData {
        private static GlobalData instance = null;
        public static GlobalData SharedInstance {
            get {
                if (instance == null) {
                    instance = new GlobalData ();
                }
                return instance;
            }
        }
        public static int activeEmo = 1;
        public static float playerHealth = 1f;

        public static int triggerCount = 13;
        public static bool[] triggersActivated = new bool[triggerCount];
        public static int[][] triggersInRoom = new int[][] { 
            new int[]{0,1,2,3},
            new int[]{4},
            new int[]{5},
            new int[]{6},
            new int[]{7},
            new int[]{8,9,10,11},
            new int[]{12}
        };
        public static bool[] completedRooms = new bool[triggersInRoom.Length];
        public static Transform[] respawns;
        public static Transform respawn;
        public static int currentRoom=0;

        public static void checkRoom()
        {
            for (int i = 0; i < completedRooms.Length; i++)
            {
                bool flag = true;
                foreach(int battery in triggersInRoom[i])
                {
                    flag = flag && triggersActivated[battery];
                }
                completedRooms[i]=flag;
            }

            for (int i = 0; i < completedRooms.Length; i++)
            {
                if (!completedRooms[i])
                {
                    currentRoom = i;
                    break;
                }
                currentRoom = i;
            } 
            respawn = respawns[currentRoom];
        }
    } 
}