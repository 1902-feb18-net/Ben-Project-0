using System;
using System.Collections.Generic;
using System.Text;

namespace HelloEntityFramework
{
    class SecretConfiguration
    {
        public const string ConnectionString =
            "Server = tcp:mages1902sql.database.windows.net,1433; Initial Catalog = Project0; " +
            "Persist Security Info = False; User ID = magesbe; Password = Sben@gain8113; " +
            "MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; " +
            "Connection Timeout = 30;";
    }
}
