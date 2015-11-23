using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmallProject.Context;
using SmallProject.Entity;

namespace SmallProject.Business
{
    public class BusinessMail
    {
        private BusinessSettings _businessSettings { get; set; }
        private Settings MailSetting { get; set; }
        private Repository<Email> db { get; }

        public BusinessMail()
        {
            _businessSettings=new BusinessSettings();
            MailSetting = _businessSettings.GetSettings(1);
            db=new Repository<Email>();
        }
        /// <summary>
        /// SMTP onaylı Tek mail gönderme Methodu
        /// </summary>
        /// <param name="subject">Mail başlığı</param>
        /// <param name="body">Mail İçeriği</param>
        /// <param name="ToMails">Mailin gideceği adreslerin liste hali</param>
        /// <param name="CCMails">Maili bilgilendirme olarak gidecek mail adresleri</param>
        /// <param name="attachments">MAil işe gidecek ek dosyaların path listesi</param>
        /// <returns>Mailin gönderilme durumu</returns>
        public bool MailSend(Email mail)
        {
            var email = new MailMessage {From = new MailAddress(MailSetting.Text)};

            var kontrol = true;
            //Mailin gideceği adresler
            foreach (var item in mail.ToMails)
            {
                email.To.Add(item.Tomail);
            }
            //Mailin bilgilendirme yani CC adresleri
            foreach (var item in mail.CcMails)
            {
                email.CC.Add(item.CcMail);
            }
            //Mailinde gidecek ekler
            foreach (var item in mail.Attachments)
            {
                email.Attachments.Add(new Attachment(@item.Attachment));
            }
            //Mail Konusu
            email.Subject = mail.Subject;
            //Mail İçeriği
            email.Body = mail.Body;
            //smtp ayarları
            var smtp = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(MailSetting.Text, MailSetting.Value),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };
            
            try
            {
                smtp.SendAsync(email, (object)email);
                db.Add(mail);
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                
            }
            return kontrol;
        }
        /// <summary>
        /// Toplu Mail yollama
        /// </summary>
        /// <param name="Mailler">Mail atlıcakları mail listesi olaran gönderin</param>
        public void MultiMailSend(List<Email> Mailler)
        {
            foreach (var item in Mailler)
            {
                MailSend(item);
            }
        }

       
    }
}
