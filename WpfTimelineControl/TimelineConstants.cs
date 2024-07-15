namespace WpfTimelineControl
{
    public static class TimelineConstants
    {
        /// <summary>
        /// If the total duration of the events on the timeline is x times greater than the current interval, use the next one up.
        /// </summary>
        public static int TimelineScalingFactor = 3;

        /// <summary>
        /// The interval options available to select from, based on the total duration of the events on the timeline.
        /// Unit is seconds.
        /// </summary>
        public static int[] IntervalOptions = new int[] { 10, 30, 60, 300, 600, 1800, 3600 };
    }
}
