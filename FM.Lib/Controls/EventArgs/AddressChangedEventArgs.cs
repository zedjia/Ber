namespace FM.Lib.Controls.EventArgs
{
    public sealed class AddressChangedEventArgs : System.EventArgs
    {
        private readonly string _address;

        public AddressChangedEventArgs(string address)
        {
            _address = address;
        }

        public string Address
        {
            get { return _address; }
        }
    }
}
