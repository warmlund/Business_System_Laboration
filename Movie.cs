using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class Movie : Product
    {
        private MovieFormatType _videoFormat;
        private string _playTime;

        public MovieFormatType VideoFormat { get { return _videoFormat; } set { _videoFormat = value; OnPropertyChanged(nameof(VideoFormat)); } }
        public string PlayTime{ get { return _playTime; } set { _playTime = value; OnPropertyChanged(nameof(PlayTime)); } }
        public string FormatDescription { get { return GetDescription(_videoFormat); } set { OnPropertyChanged(nameof(MovieFormatType)); } }

        public Movie(string id, int amount, float price, string name, MovieFormatType videoFormat, string playTime) :base(id, amount, price, name)
        {
            _videoFormat = videoFormat;
            _playTime = playTime;
        }

        public static string GetDescription(MovieFormatType value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}