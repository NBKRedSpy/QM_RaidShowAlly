using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QM_RaidShowAlly.Patches.RememberMissionSide
{
    /// <summary>
    /// The data for the "remember mission side" feature.   
    /// </summary>
    internal static class RememberMissionSide
    {


        /// <summary>
        /// The list of stations missions that need to be reversed.
        /// </summary>
        /// <remarks>Using ConditionalWeakTable with an object to not need to deal with collection cleanup.</remarks>
        private static ConditionalWeakTable<Mission, object> ReversedStationMissions = new ConditionalWeakTable<Mission, object>();


        /// <summary>
        /// This is a signal to indicate that on PrepareRaidScreen configure, the UI should be updated with the reverse status.
        /// 
        /// </summary>
        public static bool ExecuteMissionSideCheck = false;

        /// <summary>
        /// Sets the data for the mission reversal.  Call when the side button has changed to update the flag.
        /// </summary>
        /// <param name="mission">used only if the mission is being reversed.</param>
        /// <param name="reverse"></param>
        public static void SetAsNormalMission(Missions missions, string stationId)
        {
            Mission reversedMission = missions.Get(stationId, true);

            //Get the inverse of the mission since the mission passed in is normal, not reversed mission.
            //The attack/defend (aka normal and reversed) missions are different objects.
            ReversedStationMissions.Remove(reversedMission);

            //Not technically needed, but clearer.
            ExecuteMissionSideCheck = false;
        }


        /// <summary>
        /// Sets the data for the mission reversal.  Call when the side button has changed to update the flag.
        /// </summary>
        /// <param name="mission">The already reversed mission</param>
        public static void SetAsReverseMission(Mission mission)
        {
            ReversedStationMissions.GetOrCreateValue(mission);

            //Not technically needed, but clearer.
            ExecuteMissionSideCheck = false;
        }

        public static bool ShouldReverseMission(Missions missions, string stationId) 
        {
            //Check if the user reversed the mission.
            Mission mission = missions.Get(stationId, true);

            return ReversedStationMissions.TryGetValue(mission, out _);

        }
    }
}
