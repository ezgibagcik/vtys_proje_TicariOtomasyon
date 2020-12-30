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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void listele()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from \"TBL_FATURABILGI\"", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtAlici.Text = "";
            TxtId.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            TxtTeslimEden.Text = "";
            TxtTeslimAlan.Text = "";
            TxtVergiDaire.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";

        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (gridView1.RowCount > 0)
            {
                if (dr != null)
                {
                    TxtId.Text = dr["FATURABILGIID"].ToString();
                    TxtSiraNo.Text = dr["SIRANO"].ToString();
                    TxtSeri.Text = dr["SERI"].ToString();
                    MskTarih.Text = dr["TARIH"].ToString();
                    MskSaat.Text = dr["SAAT"].ToString();
                    TxtAlici.Text = dr["ALICI"].ToString();
                    TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                    TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                    TxtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
                }
            }
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr2 = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr2!=null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void BtnKaydet2_Click(object sender, EventArgs e)
        {  

            if (TxtFaturaID.Text == "" )
            {
                NpgsqlCommand komut = new NpgsqlCommand("insert into TBL_FATURABILGI(SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN)" +
                    "VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSiraNo.Text);
                komut.Parameters.AddWithValue("@p3", MskTarih.Text);
                komut.Parameters.AddWithValue("@p4", MskSaat.Text);
                komut.Parameters.AddWithValue("@p5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@p6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@p7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura bilgisi kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }

            //Firma Carisi

            if (TxtFaturaID.Text != "" && comboBox1.Text == "Firma")
            {
                double miktar, fiyat, tutar;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                NpgsqlCommand komut2 = new NpgsqlCommand("insert into TBL_FATURADETAY" +
                    "(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values(@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("p5", TxtFaturaID.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();


                //Hareket tablosuna veri girişi
                NpgsqlCommand komut3 = new NpgsqlCommand("insert into Tbl_FırmaHareketler (UrunId,adet,personel,fırma,fıyat,toplam,faturaıd,tarıh) values(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", TxtUrunID.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaID.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();


                //Stok sayısını azaltma
                NpgsqlCommand komut4 = new NpgsqlCommand("update tbl_urunler set adet=adet-@s1 where ıd=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunID.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Fatura Ait Ürün Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }


            //Müşteri Carisi

            if (TxtFaturaID.Text != "" && comboBox1.Text == "Müşteri")
            {
                double miktar, fiyat, tutar;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                NpgsqlCommand komut2 = new NpgsqlCommand("insert into TBL_FATURADETAY" +
                    "(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values(@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("p5", TxtFaturaID.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();


                //Hareket tablosuna veri girişi
                NpgsqlCommand komut3 = new NpgsqlCommand("insert into TBL_MUSTERIHAREKET (UrunId,adet,personel,musteri,fıyat,toplam,faturaıd,tarıh) values(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", TxtUrunID.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaID.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();


                //Stok sayısını azaltma
                NpgsqlCommand komut4 = new NpgsqlCommand("update tbl_urunler set adet=adet-@s1 where ıd=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunID.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Fatura Ait Ürün Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Textleri temizleme islemi
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //Kayit silme islemi
        //private void Btn_Sil_Click(object sender, EventArgs e)
        //{
        //    DialogResult Secim = new DialogResult();

        //    Secim = MessageBox.Show("Fatura Silinecek Onaylıyor Musunuz ?", "FAUK", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

        //    if (Secim == DialogResult.Yes)
        //    {
        //        SqlCommand komut = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());
        //        komut.Parameters.AddWithValue("@p1", TxtId.Text);
        //        komut.ExecuteNonQuery();
        //        bgl.baglanti().Close();
        //        listele();
        //        MessageBox.Show("Fatura silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        //        temizle();
        //    }
        //}

        //Kayit guncelleme islemi
        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("Update TBL_FATURABILGI set " +
                "SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8" +
                " WHERE FATURABILGIID=@P9",
                bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            
            komut.Parameters.AddWithValue("@P9", TxtId.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        //Urun ıdsi ile urun bulma islemi
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("Select \"URUNAD\",\"SATISFIYAT\" from \"public\".\"TBL_URUNLER\" where \"ID\"=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtUrunID.Text));
            NpgsqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
