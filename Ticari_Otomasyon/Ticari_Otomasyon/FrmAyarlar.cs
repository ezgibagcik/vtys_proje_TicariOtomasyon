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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        //Listeleme islemi.
        void listele()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from \"public\".\"TBL_ADMIN\"", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        //Temizleme islemi.
        void temizle()
        {
            TxtKullaniciAdi.Text = "";
            TxtSifre.Text = "";
            TxtId.Text = "";
        }
        
        //Ayarlar Formu yuklendiginde yapilacaklar
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        //Yeni admini sisteme kaydetme islemi.
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (BtnKaydet.Text=="Kaydet")
            {
                
                NpgsqlCommand komut = new NpgsqlCommand("insert into \"public\".\"TBL_ADMIN\" values(@p1,@p2,@p3)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
                komut.Parameters.AddWithValue("@p2", TxtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@p3", TxtSifre.Text);
            

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Yeni Admin Sisteme Kaydedildi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
        }

        //Sistemdeki kayitlari guncelleme islemi
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut2 = new NpgsqlCommand("update \"public\".\"TBL_ADMIN\" set \"KULLANICIAD\"=@p1,\"SIFRE\"=@p2 where \"ID\"=@p3", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", TxtKullaniciAdi.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p3", int.Parse(TxtId.Text));
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Kayıt Güncellendi!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        //Kullanicilari textlere tasima islemi.
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                TxtKullaniciAdi.Text = dr["KULLANICIAD"].ToString();
                TxtSifre.Text = dr["SIFRE"].ToString();
                TxtId.Text = dr["ID"].ToString();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
