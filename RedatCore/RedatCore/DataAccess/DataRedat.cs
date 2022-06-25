using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace  RedatCore.DataAccess

{
    class DataRedat
    {
        static OracleConnection con = new OracleConnection();
        readonly static string  conString = "User Id=atpc_redat;Password=Infinity_redat@20C; Data Source=redatdev1_high;";


        public DataRedat()
        {
           
            con.ConnectionString = conString;

            OracleConfiguration.TnsAdmin = @"E:\Projects\ESEC\Med_Tran\archive\Wallet_REDATDEV1";
            OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;

            if (con.State != System.Data.ConnectionState.Open)
                con.Open();

        }

        public static OracleConnection GetConnection()
        {

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.ConnectionString = conString;

                OracleConfiguration.TnsAdmin = @"E:\Projects\ESEC\Med_Tran\archive\Wallet_REDATDEV1";
                OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;

                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();
            }

            return con;        
        
        }
    }
}
