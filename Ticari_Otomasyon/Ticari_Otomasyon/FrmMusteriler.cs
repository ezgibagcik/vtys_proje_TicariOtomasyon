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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        FrmFirmalar fr = new FrmFirmalar(); 
        SqlBaglantisi bgl = new SqlBaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * From \"public\".\"TBL_MUSTERILER\"",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["TELEFON2"].Caption = "TELEFON 2";
            gridView1.Columns["MAIL"].Caption = "MAİL";
            gridView1.Columns["IL"].Caption = "İL";
            gridView1.Columns["ILCE"].Caption = "İLÇE";
            gridView1.Columns["VERGIDAIRE"].Caption = "VERGİ DAİRESİ";
        }

        //Textleri temizleme fonksiyonu.
        void temizle()
        {
            TxtAd.Text = "";
            TxtId.Text = "";
            TxtMail.Text = "";
            TxtSoyad.Text = "";
            TxtVergi.Text = "";
            MskTc.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            RchAdres.Text = "";
            TxtAd.Focus();
        }

        //Sehir listesi cagrilmasi
        //void SehirListesi()
        //{
        //    NpgsqlCommand komut = new NpgsqlCommand("select Sehir from TBL_ILLER", bgl.baglanti());
        //    NpgsqlDataReader dr = komut.ExecuteReader();
        //    while(dr.Read())
        //    {
        //        CmbIl.Properties.Items.Add(dr[0]);
        //    }
        //    bgl.baglanti().Close();
        //}

        //Il'e gore ilce listesi
        //private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CmbIlce.Properties.Items.Clear();
        //    NpgsqlCommand komut = new NpgsqlCommand("select ILCE from TBL_ILCELER where Sehir=@p1", bgl.baglanti());
        //    komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
        //    NpgsqlDataReader dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        CmbIlce.Properties.Items.Add(dr[0]);
        //    }
        //    bgl.baglanti().Close();
        //}

        //Gridden textlere tasima islemi
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskTc.Text = dr["TC"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            Listele();
            //SehirListesi();
            temizle();
        }   

        //Yeni Musteri kaydi.
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("insert into \"public\".\"TBL_MUSTERILER\"" +
                "(\"ID\",\"AD\",\"SOYAD\",\"TELEFON\",\"TELEFON2\",\"TC\",\"MAIL\",\"ADRES\",\"VERGIDAIRE\") values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p5", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p6", MskTc.Text);
            komut.Parameters.AddWithValue("@p7", TxtMail.Text);
            //komut.Parameters.AddWithValue("@p7", CmbIl.Text);
            //komut.Parameters.AddWithValue("@p8", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p8", RchAdres.Text);
            komut.Parameters.AddWithValue("@p9", TxtVergi.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            temizle();

        }

        //Kayit silme islemi
        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Satışı Onaylıyor musunuz_?", "FAUK", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                NpgsqlCommand komut = new NpgsqlCommand("Delete from \"public\".\"TBL_MUSTERILER\" where \"ID\"=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Müşteri Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Listele();
                temizle();
            }
           
        }

        //Kayit guncelleme islemi.
        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("update \"public\".\"TBL_MUSTERILER\" set " +
                "\"AD\"=@P1," +
                "\"SOYAD\"=@P2," +
                "\"TELEFON\"=@P3," +
                "\"TELEFON2\"=@P4," +
                "\"TC\"=@P5," +
                "\"MAIL\"=@P6," +
                "\"VERGIDAIRE\"=@P7," +
                "\"ADRES\"=@P8" +
                " where \"ID\"=@P9",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTc.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            //komut.Parameters.AddWithValue("@p7", CmbIl.Text);
            //komut.Parameters.AddWithValue("@p8", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p7", TxtVergi.Text);
            komut.Parameters.AddWithValue("@p8", RchAdres.Text);
            komut.Parameters.AddWithValue("@p9", int.Parse(TxtId.Text));


            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            temizle();
        }

        //Textleri temizleme islemi.
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
            
        }
    }
}
