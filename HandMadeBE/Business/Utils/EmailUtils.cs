using System.Net;
using System.Net.Mail;
using Application.ErrorHandlers;
using DataAccess.Entites;
using Google.Apis.Drive.v3.Data;

namespace ClassLibrary1.Utils;

public static class EmailUtils
{
    public static void SendEmail(string toEmail, string toSubject, string toContent)
    {
        // set up send email
        string sendto = toEmail;
        string subject = toSubject;
        string content = toContent;
        // this is sender email
        string fromEmail = "nguyentuanvu020901@gmail.com";
        string fromPasswordEmail = "fhnwtwqisekdqzcr";
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //set property for email you want to send
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(sendto);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = content;
            mail.Priority = MailPriority.High;
            //set smtp port
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            //set gmail pass sender
            smtp.Credentials = new NetworkCredential(fromEmail, fromPasswordEmail);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"Send to {toEmail} failed");
        }
    }

    public static void SendVerifyCodeToEmail(Customer user)
    {
        var title = "[HANDY CAMPUS] - Xác Minh Tài Khoản";
        var content = "Xin chào " + user.FullName + "<br>" + "<br>" +
                      "Handy Campus cảm ơn bạn đã đăng ký tài khoản tại website của chúng tôi." + "<br>" + "<br>" +
                      "Bạn vui lòng nhập mã xác mình này vào ô xác nhận ở website để hoàn thành việc đăng ký tài khoản mới: "
                      + "<strong>" + user.TokenCode + "</strong><br><br>" +
                      "Chúc bạn mua sắm vui vẻ và tìm được món hàng ưng ý!" + "<br>" + "<br>" + "Đội Ngũ Kỹ Thuật Handy Campus.";
        SendEmail(user.Email, title, content);
    }

    public static void SendNewPasswordToEmail(Customer user)
    {
        var title = "[HANDY CAMPUS] - Gửi Mật Khẩu Mới";
        var content = "Xin chào " + user.FullName + "<br>" + "<br>" +
                      "Handy Campus xin gửi bạn mật khẩu mới để đăng nhập vào website của chúng tôi: " + "<strong>" + user.PassWord + "</strong><br><br>" +
                      "Bạn vui lòng thay đổi mật khẩu của mình sau khi đăng nhập để bảo mật tài khoản " + "<br><br>" +
                      "Chúc bạn mua sắm vui vẻ và tìm được món hàng ưng ý!" + "<br>" + "<br>" + "Đội Ngũ Kỹ Thuật Handy Campus.";
        SendEmail(user.Email, title, content);
    }
}