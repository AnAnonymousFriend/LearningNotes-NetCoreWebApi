using MimeKit;
using System;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Common.Helper
{
    public class EmailUtil
    {

        static string emailAddress = Appsettings.app(new string[] { "Email", "EmailAddress" });
        static string emailpassword = Appsettings.app(new string[] { "Email", "EmailPassword" });
        static string mailboxName = Appsettings.app(new string[] { "Email", "MailboxName" });

        /// <summary>
        ///发送邮件
        /// </summary>
        /// <param name="receive">接收人</param>
        /// <param name="subject">标题</param>
        /// <param name="text">内容</param>
        /// <param name="hopeText">警告</param>
        /// <returns></returns>         
        [Obsolete]
        public static bool SendMail(string receive, string subject, string text, string hopeText)
        {
            //todo 用户名密码和邮件模版 可配置
            if (subject == "")
            {
                subject = "无标题";
            }

            bool flag = false;

            var fromMailAddress = new MailboxAddress(mailboxName, emailAddress);
            //发送标题中添加指定的地址
            string[] list = receive.ToString().Split(','); //多个收件人 ,隔开
            try
            {
                foreach (var item in list)
                {
                    //初始化MIMEKIT MIME消息类的新实例
                    var mailMessage = new MimeMessage();
                    //from报头中添加指定的地址
                    mailMessage.From.Add(fromMailAddress);
                    //发送标题中添加指定的地址
                    mailMessage.To.Add(new MailboxAddress(item));
                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        var replyTo = new MailboxAddress(mailboxName, emailAddress);
                        mailMessage.ReplyTo.Add(replyTo);//添加答复标题中的地址
                    }
                    string str = htmlContextTest(item, text, DateTime.Now.ToString(), hopeText);
                    var bodyBuilder = new BodyBuilder() { HtmlBody = str };
                    mailMessage.Body = bodyBuilder.ToMessageBody();
                    mailMessage.Subject = subject;
                    Task.Run(() => { flag = SendMail(mailMessage); });
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }


        private static bool SendMail(MimeMessage mailMessage)
        {
            try
            {
                var smtpClient = new MailKit.Net.Smtp.SmtpClient();
                smtpClient.Timeout = 10 * 1000;   //设置超时时间
                string host = "smtp.exmail.qq.com";//要连接的主机名
                int port = 25;

                smtpClient.Connect(host, port, MailKit.Security.SecureSocketOptions.None);//连接到远程smtp服务器
                smtpClient.Authenticate(emailAddress, emailpassword);
                smtpClient.Send(mailMessage);//发送邮件
                smtpClient.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /**
         * 告警邮件文件内容
         * @author zhoujinwei
         * @created 2016/06/06
         * @param addressee  收件人名称
         * @param createDate 创建时间
         * @param hopeText 告警级别
         * @return
         */
        private static string htmlContextTest(string address, string text, string createDate, string hopeText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n" +
                    "<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">\n" +
                    "<html>\n" +
                    "\n" +
                    "<head>\n" +
                    "    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\n" +
                    "    <title>[FS.COM] Notice</title>\n" +
                    "    <style type=\"text/css\">\n" +
                    "    .context-txt {\n" +
                    "        font-family: \"Microsoft Yahei\";\n" +
                    "        font-size: 14px;\n" +
                    "        padding-left: 40px;\n" +
                    "    }\n" +
                    "    .title-txt {\n" +
                    "      font-size: 16px;\n" +
                    "      height: 40px; \n" +
                    "      font-weight: bold;\n" +
                    "      padding-left: 40px;\n" +
                    "    }\n" +
                    "    .footer-txt {\n" +
                    "      font-size: 14px;\n" +
                    "      height: 20px; \n" +
                    "      padding-left: 40px;\n" +
                    "      margin-top: 20px; \n" +
                    "      font-weight: bold; \n" +
                    "    }\n" +
                    "    </style>\n" +
                    "</head>\n" +
                    "\n" +
                    "<body>\n" +
                    "    <h5 class=\"title-txt\">Dear " + address + "</H5>\n" +
                    "    <p class=\"context-txt\">" + text + "</p>\n" +
                    "    <p class=\"context-txt\">" + hopeText + "</p>\n" +
                    "    <p class=\"footer-txt\">Sincerely,</p>\n" +
                    "    <p class=\"footer-txt\">FS Box Service Team</p>\n" +
                    "</body>\n" +
                    "</html>");
            return sb.ToString();
        }

        /// <summary>
        ///发送邮件
        /// </summary>
        /// <param name="receive">接收人</param>
        /// <param name="subject">标题</param>
        /// <param name="customerMail">客户邮箱</param>
        /// <returns></returns>         
        [Obsolete]
        public static bool TestSendMail(string receive, string subject, string customerMail)
        {
            //string sender = _configuration["EmailAddress"];// "pengju.chen@huoshen.info";
            bool flag = false;
            //string displayName = _configuration["MailboxName"];//显示名称
            //string from = _configuration["EmailAddress"]; //邮箱地址
            var fromMailAddress = new MailboxAddress(mailboxName, emailAddress);
            //发送标题中添加指定的地址
            string[] list = receive.ToString().Split(','); //多个收件人 ,隔开
            try
            {
                foreach (var item in list)
                {
                    //初始化MIMEKIT MIME消息类的新实例
                    var mailMessage = new MimeMessage();
                    //from报头中添加指定的地址
                    mailMessage.From.Add(fromMailAddress);
                    //发送标题中添加指定的地址
                    mailMessage.To.Add(new MailboxAddress(item));
                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        var replyTo = new MailboxAddress(mailboxName, emailAddress);
                        mailMessage.ReplyTo.Add(replyTo);//添加答复标题中的地址
                    }
                    string str = SendHtmlContent(customerMail, receive, DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss"));
                    var bodyBuilder = new BodyBuilder() { HtmlBody = str };
                    mailMessage.Body = bodyBuilder.ToMessageBody();
                    mailMessage.Subject = subject;
                    Task.Run(() => { flag = SendMail(mailMessage); });
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 发送邮件内容
        /// </summary>
        /// <param name="customerMail">客户邮箱</param>
        /// <param name="saleMail">销售邮箱</param>
        /// <param name="date">申请时间</param>
        /// <returns></returns>
        private static string SendHtmlContent(string customerMail, string saleMail, string date)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<!DOCTYPE html>");
            builder.Append("<html lang='en'>");
            builder.Append("<head>");
            builder.Append("    <meta charset='UTF - 8'>");
            builder.Append("    <title>Title</title>");
            builder.Append("</head>");
            builder.Append("<body>");
            builder.Append("<div style='width:100%!important;background:#fff'>");
            builder.Append("    <div style='display: none; font - size:1px; color:#232323;line-height:1px;max-height:0px;max-width:0px;opacity:0;overflow:hidden'></div>");
            builder.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#f5f6f7'>");
            builder.Append("        <tbody>");
            builder.Append("            <tr>");
            builder.Append("                <td style='border-collapse: collapse' width='100%' align='center' bgcolor='#f5f6f7'>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor='#f5f6f7' height='68' style='border-collapse: collapse' align='center'>");
            builder.Append("                                    <a href='https://img-en.fs.com' style='text-decoration: none'>");
            builder.Append("                                        <img style='display:inline-block;text-decoration: none;outline: none;height: 38px;vertical-align: middle' src='https://img-en.fs.com/includes/templates/fiberstore/images/email/Email-logo.png' alt=''>");
            builder.Append("                                        <span style=\"color: #232323;font-size: 18px;color: #4c4948;display: inline-block;vertical-align: middle;height: 24px;line-height: 24px;font-family: '微软雅黑',arial,sans-serif;border-left: 1px solid #767474;margin-left: 10px;padding-left: 10px\">编码申请</span>");
            builder.Append("                                    </a>");
            builder.Append("                                </td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor=\"#fff\" style=\"border-collapse: collapse;font-size: 14px;color: #232323;line-height: 22px;font-family: '微软雅黑',arial,sans-serif;padding: 30px 20px 0;background-color: #fff\" align=\"left\">");
            builder.Append("                                    您好,");
            builder.Append("                                </td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor='#fff' style='background-color: #fff;border-collapse: collapse' height='15'></td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor=\"#fff\" style=\"background-color: #fff;border-collapse: collapse;font-size: 14px;color: #232323;line-height: 22px;font-family:'微软雅黑',arial,sans-serif;padding: 0px 20px 0\" align=\"left\">");
            builder.Append("                                    您的客户<a style=\"color: #0070bc;text-decoration:none;font-family: Open Sans,arial,sans-serif\" href=\"mailto:flora.xing@fs.com\">" + customerMail + "</a>于" + date + "申请了编码，请在后台申请汇总-编码申请处进行审核跟进。多谢您的配合!");
            builder.Append("                                </td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor='#fff' style='background-color: #fff;border-collapse: collapse' height='25'></td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor='#f5f6f7' style='border-collapse: collapse' height='20'></td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor='#f5f6f7' style='border-collapse: collapse;font-size: 12px;color: #232323;line-height: 22px;' align='center'>");
            builder.Append("                                    <table width='100%' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                                        <tbody>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td align='center' style='border-collapse: collapse; font-size: 12px; color: #232323;line-height: 22px;font-family: Open Sans,arial,sans-serif;'>");
            builder.Append("                                                    Share Your Using Experience #<a style='color: #0681d3;text-decoration: none' href='https://img-en.fs.com'>FS.COM</a>");
            builder.Append("                                                </td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td bgcolor='#f5f6f7' style='border-collapse: collapse' height='20'></td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td align='center' style='border-collapse: collapse; font-size: 12px; color: #232323;line-height: 22px;font-family: Open Sans,arial,sans-serif;'>");
            builder.Append("                                                    <a style='display:inline-block;width:15px;height:15px;margin:0 5px;background:url(https://img-en.fs.com/includes/templates/fiberstore/images/em_icon.png) no-repeat;background-position:0 0' href='https://www.linkedin.com/company/fiberstore/' target='_blank'></a>");
            builder.Append("                                                    <a style='display:inline-block;width:15px;height:15px;margin:0 5px;background:url(https://img-en.fs.com/includes/templates/fiberstore/images/em_icon.png) no-repeat;background-position:-20px 0' href='https://www.youtube.com/FiberStore' target='_blank'></a>");
            builder.Append("                                                    <a style='display:inline-block;width:15px;height:15px;margin:0 5px;background:url(https://img-en.fs.com/includes/templates/fiberstore/images/em_icon.png) no-repeat;background-position:-40px 0' href='https://www.facebook.com/FSCOM' target='_blank'></a>");
            builder.Append("                                                    <a style='display:inline-block;width:15px;height:15px;margin:0 5px;background:url(https://img-en.fs.com/includes/templates/fiberstore/images/em_icon.png) no-repeat;background-position:-60px 0' href='https://twitter.com/Fiberstore' target='_blank'></a>");
            builder.Append("                                                    <a style='display:inline-block;width:15px;height:15px;margin:0 5px;background:url(https://img-en.fs.com/includes/templates/fiberstore/images/em_icon.png) no-repeat;background-position:-80px 0' href='https://www.pinterest.co.uk/?show_error=true' target='_blank'></a>");
            builder.Append("                                                    <a style='display:inline-block;width:15px;height:15px;margin:0 5px;background:url(https://img-en.fs.com/includes/templates/fiberstore/images/em_icon.png) no-repeat;background-position:-100px 0' href='https://www.instagram.com/fs.com_fiberstore/' target='_blank'></a>");
            builder.Append("                                                </td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td bgcolor='#f5f6f7' style='border-collapse: collapse' height='20'></td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td align='center' style='border-collapse: collapse; font-size: 12px; color: #232323;line-height: 22px;font-family: Open Sans,arial,sans-serif;'>");
            builder.Append("                                                    <a style='text-decoration:none; font-size:12px; color:#232323;line-height:12px;display:inline-block;font-family: Open Sans,arial,sans-serif;padding-right: 6px;border-right: 1px solid #232323;margin-right: 4px;' href='https://www.fs.com/contact_us.html' target='_blank'>Contact Us</a>");
            builder.Append("                                                    <a style='text-decoration:none; font-size:12px; color:#232323;line-height:12px;display:inline-block;font-family: Open Sans,arial,sans-serif;padding-right: 6px;border-right: 1px solid #232323;margin-right: 4px;' href ='https://www.fs.com/index.php?main_page=my_dashboard' target='_blank'>My Account</a>");
            builder.Append("                                                    <a style='text-decoration:none; font-size:12px; color:#232323;line-height:12px;display:inline-block;font-family: Open Sans,arial,sans-serif;padding-right: 6px;border-right: 1px solid #232323;margin-right: 4px;' href ='https://www.fs.com/shipping_delivery.html' target='_blank'>Shipping &amp; Delivery</a>");
            builder.Append("                                                    <a style='text-decoration:none; font-size:12px; color:#232323;line-height:12px;display:inline-block;font-family: Open Sans,arial,sans-serif;padding-right: 6px;' href ='https://www.fs.com/policies/day_return_policy.html' target='_blank'>Return Policy</a>");
            builder.Append("                                                </td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td bgcolor='#f5f6f7' style='border-collapse: collapse' height='15'></td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td align='center' style='border-collapse: collapse; font-size: 12px; color: #232323;line-height: 18px;font-family: Open Sans,arial,sans-serif;'>");
            builder.Append("                                                    You are subscribed to this email as <a style='color: #0681d3;text-decoration: none' href='javascript:;'>" + saleMail + "</a>.");
            builder.Append("                                                    <br>");
            builder.Append("                                                    <a href='https://www.fs.com/index.php?main_page=edit_my_account' style='color: #232323;text-decoration: none'>Click here to modify your preferences or unsubscribe.</a>");
            builder.Append("                                                </td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td bgcolor='#f5f6f7' style='border-collapse: collapse' height='15'></td>");
            builder.Append("                                            </tr>");
            builder.Append("                                            <tr>");
            builder.Append("                                                <td align='center' style='border-collapse: collapse; font-size: 12px; color: #232323;line-height: 22px;font-family: Open Sans,arial,sans-serif;'>");
            builder.Append("                                                    Copyright © 2019 FS.COM All Rights Reserved.");
            builder.Append("                                                </td>");
            builder.Append("                                            </tr>");
            builder.Append("                                        </tbody>");
            builder.Append("                                    </table>");
            builder.Append("                                </td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <table width='640' border='0' cellpadding='0' cellspacing='0'>");
            builder.Append("                        <tbody>");
            builder.Append("                            <tr>");
            builder.Append("                                <td bgcolor='#f5f6f7' style='border-collapse: collapse' height='15'></td>");
            builder.Append("                            </tr>");
            builder.Append("                        </tbody>");
            builder.Append("                    </table>");
            builder.Append("                    <div style='display:none;white-space:nowrap;font:15px courier;line-height:0'>");
            builder.Append("                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;");
            builder.Append("                    </div>");
            builder.Append("                </td>");
            builder.Append("            </tr>");
            builder.Append("        </tbody>");
            builder.Append("    </table>");
            builder.Append("    <table width='700' border='0' cellpadding='0' cellspacing='0' class='m_-7247573128365416932m_4216763956208593540table' id='m_-7247573128365416932m_4216763956208593540spacer-600' style='width:600px;max-width:600px;min-width:600px'>");
            builder.Append("        <tbody>");
            builder.Append("            <tr>");
            builder.Append("                <td bgcolor='#fff'><img src='http://images.hello.zendesk.com/EloquaImages/clients/ZendeskInc/%7B123c1adb-7774-4470-b163-77f859ab86ff%7D_spacer.gif' border='0' width='700' height='1' hspace='0' vspace='0' style='width:700px;min-width:700px' class='CToWUd'></td>");
            builder.Append("            </tr>");
            builder.Append("        </tbody>");
            builder.Append("    </table>");
            builder.Append("    <table cellpadding='0' cellspacing='0' style='border:0px;padding:0px;margin:0px;display:none;float:left'>");
            builder.Append("        <tbody>");
            builder.Append("            <tr>");
            builder.Append("                <td height='1' style='font-size:1px;line-height:1px;padding:0px'>");
            builder.Append("                    <br>");
            builder.Append("                </td>");
            builder.Append("            </tr>");
            builder.Append("        </tbody>");
            builder.Append("    </table>");
            builder.Append("    <img src='https://img-en.fs.com/includes/templates/fiberstore/images/email/email-last-hidden.png' alt=''>");
            builder.Append("</div>");
            builder.Append("</body>");
            builder.Append("</html>");
            return builder.ToString();
        }
    }
}
