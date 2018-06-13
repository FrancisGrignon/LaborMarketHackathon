namespace Labor.Elastic
{
    public class ConnectionString
    {
        public ConnectionString()
        {
            // Empty
        }

        public ConnectionString(string schema, string host, int port)
        {
            Scheme = schema;
            Host = host;
            Port = port;
        }

        public string Scheme {get;set;}

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
