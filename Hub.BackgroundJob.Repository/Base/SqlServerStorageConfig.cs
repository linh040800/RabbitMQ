namespace Hub.BackgroundJob.Repository.Base
{
    public class SqlServerStorageConfig
    {
        public string DefaultConnection { get; set; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString
        {
            get { return DefaultConnection; }
        }
    }
}
