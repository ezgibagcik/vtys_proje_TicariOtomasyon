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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void FirmaListesi()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from  \"public\".\"tbl_firmalar\"", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["YETKILISTATU"].Caption = "YETKİLİ STATÜ";
            gridView1.Columns["YETKILIADSOYAD"].Caption = "YETKİLİ AD-SOYAD";
            gridView1.Columns["YETKILITC"].Caption = "YETKİLİ TC";
            gridView1.Columns["SEKTOR"].Caption = "SEKTÖR";
            gridView1.Columns["TELEFON1"].Caption = "TELEFON 1";
            gridView1.Columns["TELEFON2"].Caption = "TELEFON 2";
            gridView1.Columns["TELEFON3"].Caption = "TELEFON 3";
            gridView1.Columns["MAIL"].Caption = "MAİL";
           // gridView1.Columns["IL"].Caption = "İL";
           /// gridView1.Columns["ILCE"].Caption = "İLÇE";
            gridView1.Columns["VERGIDAIRE"].Caption = "VERGİ DAİRESİ";
            gridView1.Columns["OZELKOD1"].Caption = "ÖZEL KOD 1";
            gridView1.Columns["OZELKOD2"].Caption = "ÖZEL KOD 2";
            gridView1.Columns["OZELKOD3"].Caption = "ÖZEL KOD 3";
        }

        //Sehir listesi cagirma
        //void SehirListesi()
        //{
        //    NpgsqlCommand komut = new NpgsqlCommand("select Sehir from TBL_ILLER", bgl.baglanti());
        //    NpgsqlDataReader dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        CmbIl.Properties.Items.Add(dr[0]);
        //    }
        //    bgl.baglanti().Close();
        //}

        //Secilen sehre gore ilce listesi
        //private void CmbIl_SelectedIndexChanged_1(object sender, EventArgs e)
        //{
        //    CmbIlce.Properties.Items.Clear();
        //    NpgsqlCommand komut = new NpgsqlCommand("select \"ILCE\" from \"public\".\"TBL_ILCELER\" where \"Sehir\"=@p1", bgl.baglanti());
        //    komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
        //    NpgsqlDataReader dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        CmbIlce.Properties.Items.Add(dr[0]);
        //    }
        //    bgl.baglanti().Close();
        //}

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            FirmaListesi();
            //SehirListesi();
            temizle();
        }

        void carikodAciklamalar()
        {
            NpgsqlCommand komut = new NpgsqlCommand("Select \"FIRMAKOD1\" from \"public\".\"TBL_KODLAR\"", bgl.baglanti());
            NpgsqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        //Textleri temizleme fonksiyonu.
        void temizle()
        {
            TxtAd.Text = "";
            TxtId.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtMail.Text = "";
            TxtSektor.Text = "";
            TxtVergi.Text = "";
            TxtYetkili.Text = "";
            TxtYetkiliGorev.Text = "";
            MskFax.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            MskYekiliTC.Text = "";
            RchAdres.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtAd.Focus();
        }  

        //Yeni kayit ekleme islemi
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("insert into " +
               "\"public\".\"tbl_firmalar\"(\"AD\",\"YETKILISTATU\",\"YETKILIADSOYAD\",\"YETKILITC\",\"SEKTOR\",\"TELEFON1\",\"TELEFON2\",\"TELEFON3\",\"MAIL\",\"FAX\",\"VERGIDAIRE\",\"ADRES\",\"OZELKOD1\",\"OZELKOD2\",\"OZELKOD3\")" +
               "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15) ",
               bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskYekiliTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", TxtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskFax.Text);
           // komut.Parameters.AddWithValue("@P11", CmbIl.Text);
            //komut.Parameters.AddWithValue("@P12", CmbIlce.Text);
            komut.Parameters.AddWithValue("@P11", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P12", RchAdres.Text);
            komut.Parameters.AddWithValue("@P13", RchKod1.Text);
            komut.Parameters.AddWithValue("@P14", RchKod2.Text);
            komut.Parameters.AddWithValue("@P15", RchKod3.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FirmaListesi();
            temizle();
        }

        //Kayit silme islemi.
        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Satışı Onaylıyor musunuz_?", "FAUK", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                NpgsqlCommand komut = new NpgsqlCommand("Delete from \"public\".\"tbl_firmalar\" where \"ID\"=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                FirmaListesi();
                MessageBox.Show("Firma listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                temizle();
            }
            
        }

        //Kayitlari guncelleme islemi
        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("Update \"public\".\"tbl_firmalar\" set " +
                "\"AD\"=@P1,\"YETKILISTATU\"=@P2,\"YETKILIADSOYAD\"=@P3,\"YETKILITC\"=@P4,\"SEKTOR\"=@P5,\"TELEFON1\"=@P6,\"TELEFON2\"=@P7,\"TELEFON3\"=@P8," +
                "\"MAIL\"=@P9,\"FAX\"=@P10,\"VERGIDAIRE\"=@P11,\"ADRES\"=@P12,\"OZELKOD1\"=@P13,\"OZELKOD2\"=@P14,\"OZELKOD3\"=@P15 WHERE \"ID\"=@P16", 
                bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskYekiliTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", TxtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskFax.Text);
           // komut.Parameters.AddWithValue("@P11", CmbIl.Text);
           // komut.Parameters.AddWithValue("@P12", CmbIlce.Text);
            komut.Parameters.AddWithValue("@P11", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P12", RchAdres.Text);
            komut.Parameters.AddWithValue("@P13", RchKod1.Text);
            komut.Parameters.AddWithValue("@P14", RchKod2.Text);
            komut.Parameters.AddWithValue("@P15", RchKod3.Text);
            komut.Parameters.AddWithValue("@P16", int.Parse(TxtId.Text));

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            FirmaListesi();
            temizle();

        }

        //Textleri temizleme islemi.
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //Gridden textlere tasima islemi
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (gridView1.RowCount > 0)
            {
                if(dr != null)
                {
                    TxtId.Text = dr["ID"].ToString();
                    TxtAd.Text = dr["AD"].ToString();
                    TxtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                    TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                    MskYekiliTC.Text = dr["YETKILITC"].ToString();
                    TxtSektor.Text = dr["SEKTOR"].ToString();
                    MskTelefon1.Text = dr["TELEFON1"].ToString();
                    MskTelefon2.Text = dr["TELEFON2"].ToString();
                    MskTelefon3.Text = dr["TELEFON3"].ToString();
                    TxtMail.Text = dr["MAIL"].ToString();
                    MskFax.Text = dr["FAX"].ToString();
                   // CmbIl.Text = dr["IL"].ToString();
                   // CmbIlce.Text = dr["ILCE"].ToString();
                    TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                    RchAdres.Text = dr["ADRES"].ToString();
                    TxtKod1.Text = dr["OZELKOD1"].ToString();
                    TxtKod2.Text = dr["OZELKOD2"].ToString();
                    TxtKod3.Text = dr["OZELKOD3"].ToString();
                }
            }           
        }
    }
}
