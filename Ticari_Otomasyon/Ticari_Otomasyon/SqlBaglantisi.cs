using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;

namespace Ticari_Otomasyon
{
    class SqlBaglantisi
    {
        public NpgsqlConnection baglanti()
        {
            NpgsqlConnection baglan = new NpgsqlConnection("Server=localhost; Port=5432; Database=b191210071_vtproje; User Id=postgres; Password=0703;");
            baglan.Open();
            return baglan;
        }
    }
}
