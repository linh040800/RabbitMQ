namespace Hub.BackgroundJob.Repository.Base
{
    public class ConfigAudience
    {
        /// <summary>
        ///
        /// </summary>
        public string Secret { set; get; }

        /// <summary>
        /// Token issuers
        /// </summary>
        public string Iss { set; get; }

        /// <summary>
        /// Object using token
        /// </summary>
        public string Aud { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Sub { set; get; }
    }

    public class Identity
    {
        public string Authority { set; get; }
        public string ClientId { set; get; }
        public string ClientSecret { set; get; }
        public string Scopes { set; get; }
        public string PermissonUrl { set; get; }
        public string CheckPermissonUrl
        {
            get
            {
                return $"{this.Authority}/{this.PermissonUrl}";
            }

        }

    }
}
