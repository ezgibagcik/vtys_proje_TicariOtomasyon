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
using DevExpress.Charts;
using Npgsql;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        //Musteri hareket listesi
        void MusteriHareketleri()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from MusteriHareketleri()", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        //Firma hareket listesi
        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from FirmaHareketleri()", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;

        }

        //Cikis hareket listesi
        void CikisHareketleri()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from \"TBL_GİDERLER\"", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            

        }

        public string ad;

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;

            MusteriHareketleri();

            FirmaHareketleri();

            CikisHareketleri();

            //Toplam tutarı hesaplama
            NpgsqlCommand komut1 = new NpgsqlCommand("Select Sum(Tutar) From Tbl_FaturaDetay", bgl.baglanti());
            NpgsqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString()+ " ₺";
            }

            bgl.baglanti().Close();

            //Son Ayın Faturaları
            NpgsqlCommand komut2 = new NpgsqlCommand("select (ELEKTRIK+SU+DOGALGAZ) FROM TBL_GİDERLER ORDER BY ID ASC", bgl.baglanti());
            NpgsqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString()+ " ₺";
            }

            bgl.baglanti().Close();

            //Son Ayın Personel Maaslari
            NpgsqlCommand komut3 = new NpgsqlCommand("Select MAASLAR FROM TBL_GİDERLER ORDER BY ID ASC", bgl.baglanti());
            NpgsqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslar.Text = dr3[0].ToString()+ " ₺";
            }

            bgl.baglanti().Close();

            //Müsteri Sayisi
            NpgsqlCommand komut4 = new NpgsqlCommand("select count(*) from TBL_MUSTERILER", bgl.baglanti());
            NpgsqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayisi.Text = dr4[0].ToString();
            }

            bgl.baglanti().Close();

            //Firma Sayisi
            NpgsqlCommand komut5 = new NpgsqlCommand("select count(*)from TBL_FIRMALAR", bgl.baglanti());
            NpgsqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }

            bgl.baglanti().Close();

            //Toplam Firma Sehir Sayisi
            NpgsqlCommand komut6 = new NpgsqlCommand("select count(Distinct(IL))from TBL_FIRMALAR", bgl.baglanti());
            NpgsqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblFirmaSehirSayisi.Text = dr6[0].ToString();
            }

            bgl.baglanti().Close();

            //Toplam Müsteri Sehir Sayisi
            NpgsqlCommand komut7 = new NpgsqlCommand("select count(Distinct(IL))from TBL_MUSTERILER", bgl.baglanti());
            NpgsqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblMusteriSehirSayisi.Text = dr7[0].ToString();
            }

            bgl.baglanti().Close();

            //Toplam Personel Sayisi
            NpgsqlCommand komut8 = new NpgsqlCommand("select count(*) from TBL_PERSONELLER", bgl.baglanti());
            NpgsqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayisi.Text = dr8[0].ToString();
            }

            bgl.baglanti().Close();

            //Stok Sayisi
            NpgsqlCommand komut9 = new NpgsqlCommand("Select Sum(adet) from TBL_URUNLER", bgl.baglanti());
            NpgsqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblStokSayisi.Text = dr9[0].ToString();
            }

            bgl.baglanti().Close();

            

        }

        int sayac = 0;

        //Tabloların sayac sayesinde degismesi.
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            //Elektrik
            if (sayac > 0 && sayac <= 5) 
            {
                groupControl11.Text = "Elektrik";
                chartControl1.Series["Aylar"].Points.Clear();               
                NpgsqlCommand komut10 = new NpgsqlCommand("select top 4 Ay,Elektrık from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
                
            }
            //Su
            if (sayac>5&&sayac<=10)
            {
                groupControl11.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut11 = new NpgsqlCommand("select top 4 Ay,Su from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Doğalgaz
            if (sayac > 10 && sayac <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut12 = new NpgsqlCommand("select top 4 Ay,Dogalgaz from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr12 = komut12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
            }

            //Internet
            if (sayac > 15 && sayac <= 20)
            {
                groupControl11.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut13 = new NpgsqlCommand("select top 4 Ay,Internet from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr13 = komut13.ExecuteReader();
                while (dr13.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac > 20 && sayac <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut14 = new NpgsqlCommand("select top 4 Ay,Ekstra from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac==26)
            {
                sayac = 0;
            }
        }

        //Tabloların sayac sayesinde degismesi.
        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;

            //Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl12.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();
                NpgsqlCommand komut10 = new NpgsqlCommand("select top 4 Ay,Elektrık from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();

            }
            //Su
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl12.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut11 = new NpgsqlCommand("select top 4 Ay,Su from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Doğalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl12.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut12 = new NpgsqlCommand("select top 4 Ay,Dogalgaz from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr12 = komut12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
            }

            //Internet
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl12.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut13 = new NpgsqlCommand("select top 4 Ay,Internet from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr13 = komut13.ExecuteReader();
                while (dr13.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl12.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();

                NpgsqlCommand komut14 = new NpgsqlCommand("select top 4 Ay,Ekstra from TBL_GİDERLER ORDER BY ID DESC ", bgl.baglanti());
                NpgsqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
