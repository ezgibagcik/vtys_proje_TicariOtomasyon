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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from \"public\".\"TBL_NOTLAR\"", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }

        void temizle()
        {
            TxtId.Text = "";
            TxtBaslik.Text = "";
            RchDetay.Text = "";
            TxtOlusturan.Text = "";
            TxtHitap.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";         
        }
       
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();

            temizle();
        }

        //Tek tiklamada textlere tasima islemi.
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                TxtId.Text = dr["ID"].ToString();
                TxtBaslik.Text = dr["BASLIK"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
                TxtOlusturan.Text = dr["OLUSTURAN"].ToString();
                TxtHitap.Text = dr["HITAP"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
            }
        }

        //Cift tiklamada not detayini goruntuleme
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.metin = dr["DETAY"].ToString();
            }
            fr.Show();
        }

        //Yeni Not kaydi islemi.
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("insert into \"public\".\"TBL_NOTLAR\" (\"ID\",\"TARIH\",\"SAAT\",\"BASLIK\",\"DETAY\",\"OLUSTURAN\",\"HITAP\") values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
            komut.Parameters.AddWithValue("@p2", MskTarih.Text);
            komut.Parameters.AddWithValue("@p3", MskSaat.Text);
            komut.Parameters.AddWithValue("@p4", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@p5", RchDetay.Text);
            komut.Parameters.AddWithValue("@p6", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@p7", TxtHitap.Text);
           

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Bilgisi Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();    
        }

        //Kayit silme islemi
        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Not Silenecek Onaylıyor musunuz ?", "FAUK", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                NpgsqlCommand komut = new NpgsqlCommand("delete from \"public\".\"TBL_NOTLAR\" where \"ID\"=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", int.Parse(TxtId.Text));

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Not Sistemden Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                temizle();
                listele();
            }
        }

        //Textleri Temizleme islemi.
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //Kaydi guncelleme islemi.
        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("update \"public\".\"TBL_NOTLAR\" set \"TARIH\"=@P1,\"SAAT\"=@P2,\"BASLIK\"=@P3,\"DETAY\"=@P4,\"OLUSTURAN\"=@P5,\"HITAP\"=@P6 where \"ID\"=@P7 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTarih.Text);
            komut.Parameters.AddWithValue("@p2", MskSaat.Text);
            komut.Parameters.AddWithValue("@p3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@p4", RchDetay.Text);
            komut.Parameters.AddWithValue("@p5", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@p6", TxtHitap.Text);
            komut.Parameters.AddWithValue("@p7", int.Parse(TxtId.Text));

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Bilgisi Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }
        
    }
}
