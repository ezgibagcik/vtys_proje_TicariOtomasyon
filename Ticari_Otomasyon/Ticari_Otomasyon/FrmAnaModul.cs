using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
 
    public partial class Frm_AnaModul : DevExpress.XtraEditors.XtraForm
    {
        public Frm_AnaModul()
        {
            InitializeComponent();
        }

        public static bool formControl(string formName)
        {
            bool test = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == formName)
                {
                    frm.Activate();
                    test = true;
                }
            }
            return test;
        }

        //Urunler butonuna tiklandiginda yapilacaklar
        FrmUrunler fr;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmUrunler") == false)
            {
                fr = new FrmUrunler();
                fr.MdiParent = this;
                fr.Show();
                fr.Activate();
            }
        }

        //Musteriler butonuna tiklandiginda yapilacaklar
        FrmMusteriler fr2;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmMusteriler") == false)
            {
                fr2 = new FrmMusteriler();
                fr2.MdiParent = this;
                fr2.Show();
                fr2.Activate();
            }
        }

        //Firmalar butonuna tiklandiginda yapilacaklar
        FrmFirmalar fr3;
        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmFirmalar") == false)
            {
                fr3 = new FrmFirmalar();
                fr3.MdiParent = this;
                fr3.Show();
                fr3.Activate();
            }
        }

        //Personel butonuna tiklandiginda yapilacaklar
        FrmPersonel fr4;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmPersonel") == false)
            {
                fr4 = new FrmPersonel();
                fr4.MdiParent = this;
                fr4.Show();
                fr4.Activate();
            }
        }

        //Rehber butonuna tiklandiginda yapilacaklar
        FrmRehber fr5;
        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmRehber") == false)
            {
                fr5 = new FrmRehber();
                fr5.MdiParent = this;
                fr5.Show();
                fr5.Activate();
            }
        }

        //Giderler butonuna tiklandiginda yapilacaklar
        FrmGiderler fr6;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmGiderler") == false)
            {
                fr6 = new FrmGiderler();
                fr6.MdiParent = this;
                fr6.Show();
                fr6.Activate();
            }
        }

        //Bankalar butonuna tiklandiginda yapilacaklar
        FrmBankalar fr7;
        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmBankalar")==false)
            {
                fr7 = new FrmBankalar();
                fr7.MdiParent = this;
                fr7.Show();
                fr7.Activate();
            }
        }

        //Faturalar butonuna tiklandiginda yapilacaklar
        FrmFaturalar fr8;
        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmFaturalar") == false)
            {
                fr8 = new FrmFaturalar();
                fr8.MdiParent = this;
                fr8.Show();
                fr8.Activate();
            }
        }

        //Notlar butonuna tiklandiginda yapilacaklar
        FrmNotlar fr9;
        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmNotlar") == false)
            {
                fr9 = new FrmNotlar();
                fr9.MdiParent = this;
                fr9.Show();
                fr9.Activate();
            }
        }

        //Hareketler butonuna tiklandiginda yapilacaklar
        FrmHareketler fr10;
        private void btn_hareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmHareketler") == false)
            {
                fr10 = new FrmHareketler();
                fr10.MdiParent = this;
                fr10.Show();
                fr10.Activate();
            }
        }

        //Stoklar butonuna tiklandiginda yapilacaklar
        FrmStoklar fr11;
        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmStoklar")==false)
            {
                fr11 = new FrmStoklar();
                fr11.MdiParent = this;
                fr11.Show();
                fr11.Activate();
            }
        }

        //Raporlar butonuna tiklandiginda yapilacaklar
        FrmRaporlar fr12;
        private void btn_raporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmRaporlar") == false)
            {
                fr12 = new FrmRaporlar();
                fr12.MdiParent = this;
                fr12.Show();
                fr12.Activate();
            }
        }

        //Ayarlar butonuna tiklandiginda yapilacaklar
        FrmAyarlar fr13;
        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmAyarlar") == false)
            {
                fr13 = new FrmAyarlar();           
                fr13.Show();
                fr13.Activate();
            }
        }

        //Kasa butonuna tiklandiginda yapilacaklar
        FrmKasa fr14;
        private void BtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmKasa") == false) 
            {
                fr14 = new FrmKasa();
                fr14.ad = kullanici;
                fr14.MdiParent = this;
                fr14.Show();
                fr14.Activate();
            }
        }

        //AnaSayfa butonuna tiklandiginda yapilacaklar
        FrmAnaSayfa fr15;
        private void BtnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formControl("FrmAnaSayfa") == false)
            {
                fr15 = new FrmAnaSayfa();
                fr15.MdiParent = this;
                fr15.Show();
                fr15.Activate();
            }
        }

        public string kullanici;

        //Ana Modul Yuklendiginde yapilacaklar
        private void Frm_AnaModul_Load(object sender, EventArgs e)
        {
            if (formControl("FrmAnaSayfa") == false)
            {
                fr15 = new FrmAnaSayfa();
                fr15.MdiParent = this;
                fr15.Show();
                fr15.Activate();
            } 
        }

        
    }
}
