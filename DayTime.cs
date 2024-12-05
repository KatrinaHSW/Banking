public struct DayTime
{
    private long minutes;

    public DayTime(long minutes)
    {
        this.minutes = minutes;
    }

    public static DayTime operator +(DayTime lhs, int minute)
    {
        return new DayTime(lhs.minutes + minute);
    }

    public override string ToString()
    {
        long totalMinutes = minutes;

        long years = totalMinutes / 518_400;
        totalMinutes %= 518_400;

        long months = totalMinutes / 43_200;
        totalMinutes %= 43_200;

        long days = totalMinutes / 1_440;
        totalMinutes %= 1_440;

        long hours = totalMinutes / 60;
        long remainingMinutes = totalMinutes % 60;

        return $"{2023 + years:D4}-{months + 1:D2}-{days + 1:D2} {hours:D2}:{remainingMinutes:D2}";
    }
}
