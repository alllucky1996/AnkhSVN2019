using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Common.Handlers.Helper
{
    public class XMail
    {
        //public static string From = "dungitfa@gmail.com";
        public string SMTPServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string From { get; set; }

        // contructor
        /// <summary>
        /// Khởi tạo đối tượng hỗ trợ gửi mail
        /// </summary>
        /// <param name="smtpServer">server gửi mail vd: smtp.gmail.com</param>
        /// <param name="userName">username vd: abc@gmail.com</param>
        /// <param name="Password">pass</param>
        /// <param name="from">from email vd: abc@gmail.com</param>
        /// <param name="port">cổng dịch vụ vd google là 587</param>
        /// <param name="enableSSL">Sử dụng chứng chỉ ssl? true : false </param>
        public XMail(string smtpServer, string userName, string Password, string from, int port, bool enableSSL = false)
        {
            this.SMTPServer = smtpServer;
            this.UserName = userName;
            this.Password = Password;
            this.From = from;
            this.Port = port;
            this.EnableSsl = enableSSL;
        }


        public void Send(String to, String subject, String body)
        {

            //Send(From, to, subject, body);
            String cc = "";
            String bcc = "";
            String attachments = "";
            Thread email = new Thread(delegate ()
            {
                SendAsyncEmail(From, to, cc, bcc, subject, body, attachments);
            });
            email.IsBackground = true;
            email.Start();
        }
        public bool Sended(String to, String subject, String body)
        {
            try
            {

                //Send(From, to, subject, body);
                String cc = "";
                String bcc = "";
                String attachments = "";
                Thread email = new Thread(delegate ()
                {
                    SendAsyncEmail(From, to, cc, bcc, subject, body, attachments);
                });
                email.IsBackground = true;
                email.Start();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Gửi email đơn giản thông qua tài khoản gmail
        /// </summary>
        /// <param name="From">Email người gửi</param>
        /// <param name="to">Email người nhận</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="body">Nội dung mail</param>
        public void Send(String From, String to, String subject, String body)
        {
            String cc = "";
            String bcc = "";
            String attachments = "";

            Thread email = new Thread(delegate ()
            {
                SendAsyncEmail(From, to, cc, bcc, subject, body, attachments);
            });
            email.IsBackground = true;
            email.Start();
        }

        /// <summary>
        /// Gửi email thông qua tài khoản gmail
        /// </summary>
        /// <param name="From">Email người gửi</param>
        /// <param name="to">Email người nhận</param>
        /// <param name="cc">Danh sách email những người cùng nhận phân cách bởi dấu phẩy</param>
        /// <param name="bcc">Danh sách email những người cùng nhận phân cách bởi dấu phẩy</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="body">Nội dung mail</param>
        /// <param name="attachments">Danh sách file định kèm phân cách bởi phẩy hoặc chấm phẩy</param>
        /// 

        public void Sends(String From, String to, String cc, String bcc, String subject, String body, String attachments)
        {


            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(From);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.Body = body;

            message.ReplyToList.Add(From);
            if (cc.Length > 0)
            {
                message.CC.Add(cc);
            }
            if (bcc.Length > 0)
            {
                message.Bcc.Add(bcc);
            }
            if (attachments.Length > 0)
            {
                String[] fileNames = attachments.Split(';', ',');
                foreach (var fileName in fileNames)
                {
                    message.Attachments.Add(new Attachment(fileName));
                }
            }

            // Kết nối GMail
            var client = new SmtpClient(SMTPServer, Port)
            {
                Credentials = new NetworkCredential(UserName, Password),
                EnableSsl = this.EnableSsl
            };
            // Gởi mail
            client.Send(message);
        }

        public void Send(String From, String to, String cc, String bcc, String subject, String body, String attachments)
        {

            Thread email = new Thread(delegate ()
            {
                SendAsyncEmail(From, to, cc, bcc, subject, body, attachments);
            });
            email.IsBackground = true;
            email.Start();
        }

        private void SendAsyncEmail(String From, String to, String CC, String BCC, String subject, String body, String attachments)
        {
            try
            {

                MailMessage message = new MailMessage();
                message.From = new MailAddress(From);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.ReplyToList.Add(From);


                if (to != null)
                {
                    String[] toes = to.Split(';', ',', ' ');
                    foreach (var t in toes)
                    {
                        message.To.Add(new MailAddress(t));
                    }


                }

                if (CC.Length > 0)
                {
                    String[] CCs = CC.Split(';', ',', ' ');
                    foreach (string c in CCs)
                    {
                        message.CC.Add(new MailAddress(c));
                    }
                }

                if (BCC.Length > 0)
                {
                    String[] BCCs = BCC.Split(';', ',', ' ');
                    foreach (string b in BCCs)
                    {
                        message.Bcc.Add(new MailAddress(b));
                    }
                }

                if (attachments.Length > 0)
                {
                    String[] fileNames = attachments.Split(';', ',');
                    foreach (var fileName in fileNames)
                    {
                        message.Attachments.Add(new Attachment(fileName));
                    }
                }

                var client = new SmtpClient(SMTPServer, Port)
                {
                    Credentials = new NetworkCredential(UserName, Password),
                    EnableSsl = this.EnableSsl
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recomment. Nên dùng khi các controller sử dụng luồng chờ để kiểm soát luồng
        /// Nếu không cần kiểm soát luồng thì sử dụng các hàm send ở trên
        /// </summary>
        /// <param name="tos"></param>
        /// <param name="CCs"></param>
        /// <param name="BCCs"></param>
        /// <param name="subject"></param>
        /// <param name="body">dạng html</param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(IEnumerable<string> tos, IEnumerable<string> CCs, IEnumerable<string> BCCs, string subject, string body, IEnumerable<string> attachments = null)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(From);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.ReplyToList.Add(From);

                if (tos.Any())
                {
                    foreach (var t in tos)
                    {
                        message.To.Add(new MailAddress(t));
                    }
                }

                if (CCs.Any())
                {
                    foreach (string c in CCs)
                    {
                        message.CC.Add(new MailAddress(c));
                    }
                }

                if (BCCs.Any())
                {
                    foreach (string b in BCCs)
                    {
                        message.Bcc.Add(new MailAddress(b));
                    }
                }

                if (attachments != null && attachments.Count() > 0)
                {
                    foreach (var fileName in attachments)
                    {
                        message.Attachments.Add(new Attachment(fileName));
                    }
                }

                var client = new SmtpClient(SMTPServer, Port)
                {
                    Credentials = new NetworkCredential(UserName, Password),
                    EnableSsl = this.EnableSsl
                };

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}