using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Npgsql;

namespace Ticari_Otomasyon
{
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        //Yapilan Firma hareket listesi.
        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from FirmaHareketleri()", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;

        }

        //Yapilan Musteri hareket listesi.
        void MusteriHareketleri()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Exec MusteriHareketleri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }

        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            FirmaHareketleri();
            //MusteriHareketleri();
        }
    }
}
