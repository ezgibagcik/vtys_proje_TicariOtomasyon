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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
            //gridView2.Columns["YEKILIADSOYAD"].Caption = "YETKİLİ";
            //gridView2.Columns["TELEFON1"].Caption = "TELEFON 1";
            //gridView2.Columns["TELEFON2"].Caption = "TELEFON 2";
            //gridView2.Columns["TELEFON3"].Caption = "TELEFON 3";
            //gridView2.Columns["MAIL"].Caption = "MAİL";
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //Musteri Bilgileri.
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select \"AD\",\"SOYAD\",\"TELEFON\",\"TELEFON2\",\"MAIL\" from \"public\".\"TBL_MUSTERILER\"", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["TELEFON2"].Caption = "TELEFON 2";
            gridView1.Columns["MAIL"].Caption = "MAİL";


            //Firma Bilgileri.
            DataTable dt2 = new DataTable();
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("Select \"AD\",\"YETKILIADSOYAD\",\"TELEFON1\",\"TELEFON2\",\"TELEFON3\",\"MAIL\",\"FAX\" from \"public\".\"tbl_firmalar\"", bgl.baglanti());
            da2.Fill(dt2);
            gridControl1.DataSource = dt;
        }

        //Gridden secerek mail gonderme sekmesine yonlendirme.
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);          

            if (dr!=null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }

        //Gridden secerek mail gonderme sekmesine yonlendirme.
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm2 = new FrmMail();
            DataRow dr2 = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dr2 != null)
            {
                frm2.mail = dr2["MAIL"].ToString();
            }
            frm2.Show();
        }
    }
}
