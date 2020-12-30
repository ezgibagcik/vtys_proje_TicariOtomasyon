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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void PersonelListe()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"public\".\"TBL_PERSONELLER\"", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["MAIL"].Caption = "MAİL";
            gridView1.Columns["IL"].Caption = "İL";
            gridView1.Columns["ILCE"].Caption = "İLÇE";
            gridView1.Columns["GOREV"].Caption = "GÖREV";
        }

        //Sehir listesini cekme
        //void SehirListesi()
        //{
        //    NpgsqlCommand komut = new NpgsqlCommand("select IL from \"public\".\"TBL_ILLER\"", bgl.baglanti());
        //    NpgsqlDataReader dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        CmbIl.Properties.Items.Add(dr[0]);
        //    }
        //    bgl.baglanti().Close();
        //}

        //Textleri silme fonksiyonu
        void temizle()
        {
            TxtAd.Text = "";
            TxtId.Text = "";
            TxtSoyad.Text = "";
            MskTelefon1.Text = "";
            MskTc.Text = "";
            TxtMail.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            TxtGorev.Text = "";
            RchAdres.Text = "";            
            TxtAd.Focus();
        }
        
        ////Sehre gore ilce secme .
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

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            PersonelListe();
           // SehirListesi();
            temizle();
        }

        //Gridden textlere tasima islemi.
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (gridView1.RowCount > 0)
            {
                if (dr != null)
                {
                    TxtId.Text = dr["ID"].ToString();
                    TxtAd.Text = dr["AD"].ToString();
                    TxtSoyad.Text = dr["SOYAD"].ToString();
                    MskTelefon1.Text = dr["TELEFON"].ToString();
                    MskTc.Text = dr["TC"].ToString();
                    TxtMail.Text = dr["MAIL"].ToString();
                    CmbIl.Text = dr["IL"].ToString();
                    CmbIlce.Text = dr["ILCE"].ToString();
                    TxtGorev.Text = dr["GOREV"].ToString();
                    RchAdres.Text = dr["ADRES"].ToString();
                }
            }
        }

        //Yeni kayit ekleme islemi
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("insert into \"public\".\"TBL_PERSONELLER\" " +
                "(\"ID\",\"AD\",\"SOYAD\",\"TELEFON\",\"TC\",\"MAIL\",\"GOREV\",\"ADRES\") " +
                "values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", int.Parse(TxtId.Text));
            komut.Parameters.AddWithValue("@P2", TxtAd.Text);
            komut.Parameters.AddWithValue("@P3", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P4", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P5", MskTc.Text);
            komut.Parameters.AddWithValue("@P6", TxtMail.Text);
           // komut.Parameters.AddWithValue("@P6", CmbIl.Text);
           // komut.Parameters.AddWithValue("@P7", CmbIlce.Text);
            komut.Parameters.AddWithValue("@P7", TxtGorev.Text);
            komut.Parameters.AddWithValue("@P8", RchAdres.Text);
         

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Personel sisteme eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PersonelListe();
            temizle();
        }      

        //Kaydi Silme islemi.
        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Satışı Onaylıyor musunuz_?", "FAUK", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                // ONAY GELİNCE BURASI ÇALIŞIR, KODLARINIZ BURADA OLMALI
                NpgsqlCommand komut = new NpgsqlCommand("Delete from \"public\".\"TBL_PERSONELLER\" where \"ID\"=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                PersonelListe();
                temizle();
            }
        }

        //Kaydi guncelleme islemi
        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("update \"public\".\"TBL_PERSONELLER\" set " +
                "\"AD\"=@P1," +
                "\"SOYAD\"=@P2," +
                "\"TELEFON\"=@P3," +               
                "\"TC\"=@P4," +
                "\"MAIL\"=@P5," +
                "\"GOREV\"=@P6," +
                "\"ADRES\"=@P7" +
                " where \"ID\"=@P8", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTc.Text);
            komut.Parameters.AddWithValue("@p5", TxtMail.Text);
           // komut.Parameters.AddWithValue("@p6", CmbIl.Text);
          //  komut.Parameters.AddWithValue("@p7", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p6", TxtGorev.Text);
            komut.Parameters.AddWithValue("@p7", RchAdres.Text);
            komut.Parameters.AddWithValue("@p8", int.Parse(TxtId.Text));

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            PersonelListe();
            temizle();
        }

        //Textleri temizleme islemi.
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

    
    }
}
