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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void GiderListesi()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"public\".\"TBL_GİDERLER\" order by \"ID\" asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["ELEKTRIK"].Caption = "ELEKTRİK";
            gridView1.Columns["DOGALGAZ"].Caption = "DOĞALGAZ";
            gridView1.Columns["INTERNET"].Caption = "İNTERNET";
            gridView1.Columns["MAASLAR"].Caption = "MAAŞLAR";
        }

        //Textleri temizleme islemi
        void temizle()
        {
            TxtId.Text = "";
            CmbAy.Text = "";
            CmbYil.Text = "";
            TxtElektrik.Text = "";
            TxtSu.Text = "";
            TxtDogalgaz.Text = "";
            TxtInternet.Text = "";
            TxtMaaslar.Text = "";
            TxtEkstra.Text = "";
            RchNotlar.Text = "";  
            CmbAy.Focus();
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            GiderListesi();
            temizle();
        }   

        //Gridden textlere tasima islemi.
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                TxtId.Text = dr["ID"].ToString();
                CmbAy.Text = dr["AY"].ToString();
                CmbYil.Text = dr["YIL"].ToString();
                TxtElektrik.Text = dr["ELEKTRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                TxtInternet.Text = dr["INTERNET"].ToString();
                TxtMaaslar.Text = dr["MAASLAR"].ToString();
                TxtEkstra.Text = dr["EKSTRA"].ToString();
                RchNotlar.Text = dr["NOTLAR"].ToString();
            }
        }

        //Yeni gider kaydi.
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("insert into \"public\".\"TBL_GİDERLER\"" +
                "(\"AY\",\"YIL\",\"ELEKTRIK\",\"SU\",\"DOGALGAZ\",\"INTERNET\",\"MAASLAR\",\"EKSTRA\",\"NOTLAR\",\"ID\") values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
            komut.Parameters.AddWithValue("@P10", int.Parse(TxtId.Text));

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider tabloya eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GiderListesi();
            temizle();
        }

        //Kayit textlerini temizleme islemi
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //Kayit Silme islemi
        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Gider Silenecek Onaylıyor musunuz_?", "FAUK", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                // ONAY GELİNCE BURASI ÇALIŞIR, KODLARINIZ BURADA OLMALI
                NpgsqlCommand komut = new NpgsqlCommand("Delete from \"public\".\"TBL_GİDERLER\" where \"ID\"=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                GiderListesi();
                temizle();
            }
        }

        //Kayit guncelleme islemi
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("update \"public\".\"TBL_GİDERLER\" set " +
               "\"AY\"=@P1," +
               "\"YIL\"=@P2," +
               "\"ELEKTRIK\"=@P3," +
               "\"SU\"=@P4," +
               "\"DOGALGAZ\"=@P5," +
               "\"INTERNET\"=@P6," +
               "\"MAASLAR\"=@P7," +
               "\"EKSTRA\"=@P8," +
               "\"NOTLAR\"=@P9" +              
               " where \"ID\"=@P10", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", CmbAy.Text);
            komut.Parameters.AddWithValue("@P2", CmbYil.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@P5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@P8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@P9", RchNotlar.Text);
            komut.Parameters.AddWithValue("@P10", int.Parse(TxtId.Text));

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            GiderListesi();
            temizle();
        }
    }
}
