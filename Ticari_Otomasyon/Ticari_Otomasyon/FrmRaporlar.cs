using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmRaporlar : Form
    {
        public FrmRaporlar()
        {
            InitializeComponent();
        }

        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet4.TBL_PERSONELLER' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TBL_PERSONELLERTableAdapter.Fill(this.DboTicariOtomasyonDataSet4.TBL_PERSONELLER);
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet3.TBL_GİDERLER' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TBL_GİDERLERTableAdapter.Fill(this.DboTicariOtomasyonDataSet3.TBL_GİDERLER);
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet2.TBL_MUSTERILER' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TBL_MUSTERILERTableAdapter.Fill(this.DboTicariOtomasyonDataSet2.TBL_MUSTERILER);
            // TODO: Bu kod satırı 'DboTicariOtomasyonDataSet.TBL_FIRMALAR' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.TBL_FIRMALARTableAdapter.Fill(this.DboTicariOtomasyonDataSet.TBL_FIRMALAR);

            //Araclari yenileme islemi.
            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
            this.reportViewer4.RefreshReport();
            this.reportViewer5.RefreshReport();
        }
    }
}
