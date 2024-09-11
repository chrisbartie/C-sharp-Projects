using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjBandDelegate
{
    internal class FestivalPlanner
    {
        private List<Band> bands = new List<Band>();
        private BandAnnouncementDelegate bandDel;
        private StageAnnouncementDelegate stageDel;

        public void AddBand(string  bandName, string genre)
        {
            bands.Add(new Band { bandName = bandName, Genre = genre });
            Console.WriteLine($"Added: {bandName}\n");
        }

        public void ScheduleFest(string bandName, string timeSlot)
        {
            Band foundBand = null;
            foreach (Band band in bands)
            {
                if (band.bandName == bandName)
                {
                    foundBand = band;
                    break;
                }
            }
            if (foundBand != null)
            {
                foundBand.timeSlot = timeSlot;
                Console.WriteLine($"{bandName} are scheduled to perform at {timeSlot}\n");
            } else
            {
                Console.WriteLine($"{bandName} not found !\n");
            }
        }
        
        public void RegisterBand(BandAnnouncementDelegate announceDel)
        {
            bandDel += announceDel;
        }

        public void RegisterStage(StageAnnouncementDelegate announceStageDel)
        {
            stageDel += announceStageDel;
        }

        public void FestivalSim()
        {
            for(int i = 0; i < bands.Count; i++)
            {
                Band band = bands[i];
                if (bandDel != null)
                {
                    bandDel(band);
                }
            }
            if (stageDel != null)
            {
                stageDel();
            }
        }

    }
}
