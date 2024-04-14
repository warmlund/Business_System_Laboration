namespace Business_System_Laboration_4
{
    public class Movie : Product
    {
        private MovieFormatType _videoFormat;
        private int _playTime;

        public MovieFormatType VideoFormat { get { return _videoFormat; } set { _videoFormat = value; } }
        public int PlayTime{ get { return _playTime; } set { _playTime = value; } }

        public Movie(string id, int amount, float price, string name, MovieFormatType videoFormat, int playTime) :base(id, amount, price, name)
        {
            _videoFormat = videoFormat;
            _playTime = playTime;
        }
    }
}