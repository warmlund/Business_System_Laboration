namespace Business_System_Laboration_4
{
    public class Movie : Product
    {
        private MovieFormatType movieFormat;
        private string _playTime;

        public MovieFormatType VideoFormat { get { return movieFormat; } set { movieFormat = value; OnPropertyChanged(nameof(VideoFormat)); } }
        public string PlayTime { get { return _playTime; } set { _playTime = value; OnPropertyChanged(nameof(PlayTime)); } }
        public string FormatDescription { get { return EnumDescriptionExtractor<MovieFormatType>.GetDescription(movieFormat); } set { OnPropertyChanged(nameof(MovieFormatType)); } }

        public Movie(string id, int amount, float price, string name, MovieFormatType videoFormat, string playTime) : base(id, amount, price, name)
        {
            movieFormat = videoFormat;
            _playTime = playTime;
        }
    }
}