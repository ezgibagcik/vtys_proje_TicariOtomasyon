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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from \"public\".\"TBL_URUNLER\"", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["URUNAD"].Caption = "ÜRÜN AD";
            gridView1.Columns["ALISFIYAT"].Caption = "ALIŞ FİYAT";
            gridView1.Columns["SATISFIYAT"].Caption = "SATIŞ FİYAT";
        }

        //Textleri temizleme fonksiyonu.
        void temizle()
        {
            TxtAd.Text = "";
            TxtId.Text = "";
            TxtAlıs.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            TxtSatis.Text = "";
            NudAdet.EditValue = null;
            MskYil.Text = "";
            RchDetay.Text = "";
            MskYil.EditValue = null;
            TxtAd.Focus();
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        //Verileri Kaydetme
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("insert into \"public\".\"TBL_URUNLER\"" +
                "(\"URUNAD\",\"MARKA\",\"MODEL\",\"YIL\",\"ADET\",\"ALISFIYAT\",\"SATISFIYAT\",\"DETAY\") values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@p3", TxtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlıs.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün sisteme eklendi", "Bilgi", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            listele();
            temizle();
        }

        //Kayit silme islemi.
        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Satışı Onaylıyor musunuz_?", "FAUK", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                NpgsqlCommand komutsil = new NpgsqlCommand("Delete from \"public\".\"TBL_URUNLER\" where \"ID\"=@p1", bgl.baglanti());
                komutsil.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
                komutsil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Ürün silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listele();
                temizle();
            }
        }

        //Kayit Guncelleme islemi.
        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("update \"public\".\"TBL_URUNLER\" set \"URUNAD\"=@P1,\"MARKA\"=@P2,\"MODEL\"=@P3,\"YIL\"=@P4,\"ADET\"=@P5,\"ALISFIYAT\"=@P6,\"SATISFIYAT\"=@P7,\"DETAY\"=@P8 WHERE \"ID\"=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@p3", TxtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlıs.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            komut.Parameters.AddWithValue("@P9", int.Parse(TxtId.Text));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Ürün bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        //Cift tiklama oldugunda tasima islemi.
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount>0)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["URUNAD"].ToString();
                TxtMarka.Text = dr["MARKA"].ToString();
                TxtModel.Text = dr["MODEL"].ToString();
                MskYil.Text = dr["YIL"].ToString();
                NudAdet.Value = decimal.Parse(dr["ADET"].ToString());
                TxtAlıs.Text = dr["ALISFIYAT"].ToString();
                TxtSatis.Text = dr["SATISFIYAT"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
            }   
        }

        //Textleri temizleme islemi.
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
