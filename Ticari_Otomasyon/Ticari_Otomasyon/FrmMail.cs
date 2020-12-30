using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;

        private void FrmMail_Load(object sender, EventArgs e)
        {
            TxtMailAdresi.Text = mail;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("ibrahimkoca111@hotmail.com", "12323233");
            istemci.Port = 587;
            istemci.Host = "smpt.live.com";
            istemci.EnableSsl = true;
            mesajim.To.Add("ibrahimkoca111@gmail.com");
            mesajim.From = new MailAddress("ibrahimkoca111@gmail.com");
            mesajim.Subject = TxtKonu.Text;
            mesajim.Body = RchMesaj.Text;
            istemci.Send(mesajim);

        }
    }
}
