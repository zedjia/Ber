namespace FM.Lib.Controls.EventArgs
{
    public sealed class TitleChangedEventArgs : System.EventArgs
    {
        private readonly string _title;

        public TitleChangedEventArgs(string title)
        {
            _title = title;
        }

        public string Title
        {
            get { return _title; }
        }
    }
}
