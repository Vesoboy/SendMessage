using Microsoft.AspNetCore.Mvc;
using SendMessage;
using SendMessage.Models;
using SendMessage.Services;

namespace SendMessage.Controllers
{
    /// <summary>
    /// Контроллер для управления отправкой электронных писем.
    /// </summary>
    [ApiController]
    [Route("api/mails")]
    public class EmailController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly EmailService _emailService;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        public EmailController(DataContext dbContext, EmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        /// <summary>
        /// Метод для отправки электронного письма.
        /// </summary>
        /// <param name="email">Информация об отправляемом письме.</param>
        /// <returns>Результат отправки письма.</returns>
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] InformEmail email)
        {
            try
            {
                bool isSent = await _emailService.SendEmailAsync(_dbContext, email);

                if (isSent)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Failed to send email");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Метод для получения списка отправленных писем.
        /// </summary>
        /// <returns>Список отправленных писем.</returns>
        [HttpGet]
        public IActionResult GetEmails()
        {
            var emails = _dbContext.InformEmails.ToList();
            return Ok(emails);
        }
    }
}
