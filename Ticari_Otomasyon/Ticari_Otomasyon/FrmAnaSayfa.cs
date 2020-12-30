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
using System.Xml;
using Npgsql;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        //Azalan urun stoklarini belirtir.
        void Stoklar()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select \"URUNAD\",SUM(\"ADET\") as \"'ADET'\" from \"public\".\"TBL_URUNLER\" group by \"URUNAD\" having sum(\"ADET\")<=20 order by SUM(\"ADET\")", bgl.baglanti());
            da.Fill(dt);
            gridControlStoklar.DataSource = dt;
        }

        //Son 10 not belirtilir.
        void Ajanda()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select \"TARIH\",\"SAAT\",\"BASLIK\" from \"public\".\"TBL_NOTLAR\" order by \"ID\" desc limit 10", bgl.baglanti());
            da.Fill(dt);
            gridControlAjanda.DataSource = dt;
        }

        //Son yapilan Firma Hareketleri belirtilir.
        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT * FROM FirmaHareketleri2()", bgl.baglanti());
            da.Fill(dt);
            gridControlHareketler.DataSource = dt;
        }
    
        //Firma telefon ve ad belirtilir.
        void Fihrist()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select \"AD\",\"TELEFON1\" from \"public\".\"tbl_firmalar\"", bgl.baglanti());
            da.Fill(dt);
            gridControlFihrist.DataSource = dt;
        }

        //Haberler Çekilir.

        //void Haberler()
        //{
        //    XmlTextReader xmlOku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
        //    while (xmlOku.Read())
        //    {
        //        if (xmlOku.Name == "title")
        //        {
        //            listBox1.Items.Add(xmlOku.ReadString());
        //        }
        //    }
        //}

        //Anasayfa yüklendiginde hepsi cagirilir.
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            Stoklar();

            Ajanda();

            FirmaHareketleri();

            Fihrist();

            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");

            //Haberler();
        }
    }
}
