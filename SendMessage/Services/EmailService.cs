using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using SendMessage.Models;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SendMessage.Services
{
    /// <summary>
    /// Сервис для отправки электронных писем.
    /// </summary>
    public class EmailService
    {
        private readonly SmtpConfig _smtpConfig;

        /// <summary>
        /// Конструктор сервиса для отправки электронных писем.
        /// </summary>
        /// <param name="smtpConfig">Конфигурация SMTP.</param>
        public EmailService(IConfiguration configuration)
        {
            _smtpConfig = configuration.GetSection("SmtpConfig").Get<SmtpConfig>();
        }

        /// <summary>
        /// Метод для создания объекта электронного письма.
        /// </summary>
        /// <param name="name">Имя отправителя.</param>
        /// <param name="emailFrom">Адрес электронной почты отправителя.</param>
        /// <param name="emailTo">Адрес электронной почты получателя.</param>
        /// <param name="subject">Тема письма.</param>
        /// <param name="body">Текст письма.</param>
        /// <returns>Объект электронного письма.</returns>
        public static async Task<MailMessage> CreateMailAsync(string name, string emailFrom, string emailTo, string subject, string body)
        {
            var from = new MailAddress(emailFrom, name);
            var to = new MailAddress(emailTo);
            var mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            return mail;
        }

        /// <summary>
        /// Метод для отправки электронного письма.
        /// </summary>
        /// <param name="host">Хост SMTP-сервера.</param>
        /// <param name="smptPort">Порт SMTP-сервера.</param>
        /// <param name="emailFrom">Адрес электронной почты отправителя.</param>
        /// <param name="pass">Пароль электронной почты отправителя.</param>
        /// <param name="mail">Объект электронного письма.</param>
        public static async Task SendMailAsync(string host, int smptPort, string emailFrom, string pass, MailMessage mail)
        {
            using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(host, smptPort))
            {
                smtp.Credentials = new NetworkCredential(emailFrom, pass);
                smtp.EnableSsl = true;
                try
                {
                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    // Обработка ошибок отправки почты, например, логирование
                    Console.WriteLine($"Failed to send email: {ex}");
                    throw; // Проброс исключения для обработки в вызывающем коде
                }
            }
        }

        /// <summary>
        /// Асинхронно отправляет электронное письмо.
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        /// <param name="email">Информация об электронном письме.</param>
        /// <returns>True, если письмо успешно отправлено; в противном случае - False.</returns>
        public async Task<bool> SendEmailAsync(DataContext dbContext, InformEmail email)
        {
            try
            {
                foreach (var recipient in email.Recipients)
                {
                    // Отправка письма
                    var mailMessage = await CreateMailAsync("Ivan", _smtpConfig.EmailFrom, recipient, email.Subject, email.Body);
                    await SendMailAsync(_smtpConfig.SmtpHost, _smtpConfig.SmtpPort, _smtpConfig.EmailFrom, _smtpConfig.Password, mailMessage);
                }
                // Логирование результата в базу данных
                email.CreatedDate = DateTime.UtcNow;
                email.Result = "OK"; // Пока мы не обрабатываем ошибки, предположим, что всегда успешно
                dbContext.InformEmails.Add(email);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок отправки почты
                Console.WriteLine($"Error sending email: {ex}");

                // Логирование неудачного результата в базу данных
                email.CreatedDate = DateTime.UtcNow;
                email.Result = "Failed";
                email.FailedMessage = ex.Message; // Сообщение об ошибке
                await dbContext.InformEmails.AddAsync(email);
                await dbContext.SaveChangesAsync();

                return false;
            }
        }
    }

}
