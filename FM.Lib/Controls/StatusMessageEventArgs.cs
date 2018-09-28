namespace FM.Lib.Controls
{
    public sealed class StatusMessageEventArgs : System.EventArgs
    {
        private readonly string _value;

        public StatusMessageEventArgs(string value)
        {
            _value = value;
        }

        public string Value { get { return _value; } }
    }
}
