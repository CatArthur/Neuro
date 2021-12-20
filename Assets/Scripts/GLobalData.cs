using System.Collections.Generic;

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
        public static int activeEmo = -1;
        public static float playerHealth = 1f;
        public static bool[] fullBatteries = new bool[4];
        
    } 
}