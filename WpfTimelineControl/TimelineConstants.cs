namespace WpfTimelineControl
{
    public static class TimelineConstants
    {
        private const double defaultScalingFactor = 3.0;
        private static readonly int[] defaultIntervalOptions = new int[] { 10, 30, 60, 300, 600, 1800, 3600 };

        private static readonly object lockObject = new object();

        private static double scalingFactor = defaultScalingFactor;
        private static int[] intervalOptions = defaultIntervalOptions;

        /// <summary>
        /// If the total duration of the events on the timeline is x times greater than the current interval, use the next one up.
        /// </summary>
        public static double GetScalingFactor()
        {
            lock (lockObject) return scalingFactor;
        }

        public static void SetScalingFactor(double value)
        {
            lock (lockObject) scalingFactor = value;
        }

        public static void ResetScalingFactor()
        {
            lock (lockObject) scalingFactor = defaultScalingFactor;
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

        public static void ResetIntervalOptions()
        {
            lock (lockObject) intervalOptions = defaultIntervalOptions;
        }
    }
}
