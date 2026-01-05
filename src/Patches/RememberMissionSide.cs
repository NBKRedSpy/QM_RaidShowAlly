using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_RaidShowAlly.Patches
{
    /// <summary>
    /// The data for the "remember mission side" feature.   
    /// </summary>
    internal static class RememberMissionSide
    {
        /// <summary>
        /// This is a signal to indicate that on PrepareRaidScreen configure, the UI should be updated with the reverse status.
        /// 
        /// </summary>
        public static bool ExecuteMissionSideReversal = false;

        /// <summary>
        /// Set this value to true to indicate the mission should be reversed.
        /// IMPORTANT: This must *only* be set in the PrepareRaidScreen's initial opening.  Not Configure
        /// </summary>
        public static bool ReverseMission = false;

        /// <summary>
        /// The mission that the ReverseMission flag applies to.    
        /// </summary>
        public static WeakReference<Mission> CurrentMission = new WeakReference<Mission>(null);


        /// <summary>
        /// Sets the data for the mission reversal.  Call when the side button has changed to update the flag.
        /// </summary>
        /// <param name="mission"></param>
        /// <param name="reverse"></param>
        public static void SetMissionSide(Mission mission, bool reverse)
        {
            
            CurrentMission.SetTarget(mission);
            ReverseMission = reverse;

            //Not technically needed, but clearer.
            ExecuteMissionSideReversal = false; 
        }
    }
}
