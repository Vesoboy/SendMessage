using System.ComponentModel.DataAnnotations;

namespace SendMessage.Models
{
    /// <summary>
    /// Модель данных для представления информации о письме.
    /// </summary>
    public class InformEmail
    {
        /// <summary>
        /// Уникальный идентификатор письма.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Тема письма.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Текст письма.
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// Список адресатов письма.
        /// </summary>
        public List<string>? Recipients { get; set; }

        /// <summary>
        /// Дата и время создания письма.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Результат отправки письма.
        /// </summary>
        public string? Result { get; set; }

        /// <summary>
        /// Сообщение об ошибке в случае неудачной отправки.
        /// </summary>
        public string? FailedMessage { get; set; }
    }
}
