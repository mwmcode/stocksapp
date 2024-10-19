namespace api.Helpers
{
    public class QueryParams
    {
        const int _maxSize = 50;
        private int _size = 20;

        public string? Symbol { get; set; } = null;
        public string? Company { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int Page { get; set; } = 1;
        public int Size
        {
            get { return _size; }
            set
            {
                _size = Math.Min(_maxSize, value);
            }
        }
    }
}