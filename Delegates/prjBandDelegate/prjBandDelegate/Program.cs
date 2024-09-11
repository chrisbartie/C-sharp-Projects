namespace prjBandDelegate
{
    public delegate void BandAnnouncementDelegate(Band band);
    public delegate void StageAnnouncementDelegate();
    internal class Program
    {
        static void Main(string[] args)
        {
            FestivalPlanner fp = new FestivalPlanner();

            fp.RegisterBand(bandAnnouncement);

            //adding static data for adding bands
            fp.AddBand("The Killers", "Rock");
            fp.AddBand("Twenty One Pilots", "Alternative");

            fp.ScheduleFest("The Killers", "21:30");
            fp.ScheduleFest("Twenty One Pilots", "16:00");

            fp.FestivalSim();

        }

        static void bandAnnouncement(Band band)
        {
            Console.WriteLine($"We would like to announce: {band.bandName}, {band.Genre}\n");
        }
    }
}
