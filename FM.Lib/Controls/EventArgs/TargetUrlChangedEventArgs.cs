namespace FM.Lib.Controls.EventArgs
{
    public sealed class TargetUrlChangedEventArgs : System.EventArgs
    {
        private readonly string _targetUrl;

        public TargetUrlChangedEventArgs(string targetUrl)
        {
            _targetUrl = targetUrl;
        }

        public string TargetUrl
        {
            get { return _targetUrl; }
        }
    }
}
