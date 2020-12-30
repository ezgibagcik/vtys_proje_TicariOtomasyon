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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 4);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 6);
            //chartControl1.Series["Series 1"].Points.AddPoint("Adana", 8);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 5);

            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select UrunAd,Sum(adet) as 'Miktar' from TBL_URUNLER group by URUNAD", bgl.baglanti());
            DataTable dt = new DataTable(); ;
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Charta Stok Miktarı Listeleme
            NpgsqlCommand komut = new NpgsqlCommand("Select UrunAd,Sum(adet) as 'Miktar' from Tbl_Urunler group by Urunad",bgl.baglanti());
            NpgsqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));

            }
            bgl.baglanti().Close();

            //Charta Firma Şehir Sayısı Çekme
            NpgsqlCommand komut2 = new NpgsqlCommand("Select IL,count(*) from Tbl_Fırmalar group by IL" , bgl.baglanti());
            NpgsqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();

           
        }

        //Cift tiklama yapildiginda stok detaya yonlendirme.
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.ad = dr["URUNAD"].ToString();
            }
            fr.Show();
        }
    }
}
