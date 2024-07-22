namespace WpfTimelineControl
{
    public static class TimelineConstants
    {
        private static readonly object lockObject = new object();

        private static double timelineScalingFactor = 3.0;
        private static int[] intervalOptions = new int[] { 10, 30, 60, 300, 600, 1800, 3600 };

        /// <summary>
        /// If the total duration of the events on the timeline is x times greater than the current interval, use the next one up.
        /// </summary>
        public static double GetTimelineScalingFactor()
        {
            lock (lockObject) return timelineScalingFactor;
        }

        public static void SetTimelineScalingFactor(double value)
        {
            lock (lockObject) timelineScalingFactor = value;
        }

        /// <summary>
        /// The interval options available to select from, based on the total duration of the events on the timeline.
        /// Unit is seconds.
        /// </summary>
        public static int[] GetIntervalOptions()
        {
            lock(lockObject) return intervalOptions;
        }

        public static void SetIntervalOptions(int[] value)
        {
            lock (lockObject) intervalOptions = value;
        }
    }
}
