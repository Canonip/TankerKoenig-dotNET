namespace TankerKoenig.Api
{
    public class Details
    {
        public OpeningTime[] OpeningTimes { get; set; }
        public string[] Overrides { get; set; }
        public bool WholeDay { get; set; }
        public string State { get; set; }
    }

    public class OpeningTime
    {
        public string Text { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}