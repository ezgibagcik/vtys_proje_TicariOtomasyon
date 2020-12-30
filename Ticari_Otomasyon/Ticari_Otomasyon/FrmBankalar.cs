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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        ////Sehir listesi cekme.
        //void SehirListesi()
        //{
        //    NpgsqlCommand komut = new NpgsqlCommand("select Sehir from TBL_ILLER", bgl.baglanti());
        //    NpgsqlDataAdapter dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        CmbIl.Properties.Items.Add(dr[0]);
        //    }
        //    bgl.baglanti().Close();
        //}

        ////İlceleri sehre gore cekme
        //private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CmbIlce.Properties.Items.Clear();
        //    NpgsqlCommand komut = new NpgsqlCommand("select ILCE from TBL_ILCELER where Sehir=@p1", bgl.baglanti());
        //    komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
        //    NpgsqlDataAdapter dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        CmbIlce.Properties.Items.Add(dr[0]);
        //    }
        //    bgl.baglanti().Close();
        //}

        void listele()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT * FROM \"bankalar\"()", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void FirmaListesi()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select \"ID\",\"AD\" from \"public\".\"tbl_firmalar\"", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtBankaAd.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            TxtSube.Text = "";
            TxtIBAN.Text = "";
            TxtHesapNo.Text = "";
            TxtYetkili.Text = "";
            MskTelefon1.Text = "";
            MskTarih.Text = "";
            TxtHesapTürü.Text = "";
            lookUpEdit1.Text = "";
            TxtBankaAd.Focus();
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
           listele();

            //SehirListesi();

            FirmaListesi();

            temizle();
            
        }

        //Sisteme yeni banka kaydetme islemi.
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("insert into \"public\".\"TBL_BANKALAR\"(\"BANKAAD\",\"IL\",\"ILCE\",\"SUBE\",\"IBAN\",\"HESAPNO\",\"YETKILI\",\"TELEFON\",\"TARIH\",\"HESAPTURU\",\"FIRMAID\")VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", CmbIl.Text);
            komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIBAN.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p9", MskTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTürü.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgisi kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
      
        }

        //Gridden textlere tasima islemi.
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                TxtId.Text = dr["id"].ToString();
                TxtBankaAd.Text = dr["bankaad"].ToString();
                CmbIl.Text = dr["il"].ToString();
                CmbIlce.Text = dr["ilce"].ToString();
                TxtSube.Text = dr["sube"].ToString();
                TxtIBAN.Text = dr["iban"].ToString();
                TxtHesapNo.Text = dr["hesapno"].ToString();
                TxtYetkili.Text = dr["yetkili"].ToString();
                MskTelefon1.Text = dr["telefon"].ToString();
                MskTarih.Text = dr["tarih"].ToString();
                TxtHesapTürü.Text = dr["hesapturu"].ToString();
            }
        }

        //Textleri temizleme islemi.
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //Kaydi Silme İslemi.
        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("delete from \"public\".\"TBL_BANKALAR\" where \"ID\"=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            MessageBox.Show("Banka bilgisi sistemden silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        //Kaydi guncelleme islemi
        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("update \"public\".\"TBL_BANKALAR\" set " +
               "\"BANKAAD\"=@P1," +
               "\"IL\"=@P2," +
               "\"ILCE\"=@P3," +
               "\"SUBE\"=@P4," +
               "\"IBAN\"=@P5," +
               "\"HESAPNO\"=@P6," +
               "\"YETKILI\"=@P7," +
               "\"TELEFON\"=@P8," +
               "\"TARIH\"=@P9," +
               "\"HESAPTURU\"=@P10," +
               "\"FIRMAID\"=@P11" +
               " where \"ID\"=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@P2", CmbIl.Text);
            komut.Parameters.AddWithValue("@P3", CmbIlce.Text);
            komut.Parameters.AddWithValue("@P4", TxtSube.Text);
            komut.Parameters.AddWithValue("@P5", TxtIBAN.Text);
            komut.Parameters.AddWithValue("@P6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P9", MskTarih.Text);
            komut.Parameters.AddWithValue("@P10", TxtHesapTürü.Text);
            komut.Parameters.AddWithValue("@P11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@P12", int.Parse(TxtId.Text));

            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);    
            temizle();
        }
    }
}
