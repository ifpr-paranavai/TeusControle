using System;
using System.Collections.Generic;

namespace TeusControle.Infrastructure.Dtos
{
    /// <summary>
    /// Classe de retorno para 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseMessages<T>
    {
        /// <summary>
        /// Situação
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Data e hora
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Mensagens
        /// </summary>
        public IList<Messages> Messages { get; set; }

        /// <summary>
        /// Informações
        /// </summary>
        public T Data { get; set; }

        public ResponseMessages(
            bool status,
            T data,
            string message
        )
        {
            this.Status = status;
            this.TimeStamp = DateTime.Now;
            this.Data = data;
            this.AddMessage(message);
        }

        public ResponseMessages(
            bool status, 
            string message
        )
        {
            this.Status = status;
            this.TimeStamp = DateTime.Now;
            this.AddMessage(message);
        }

        /// <summary>
        /// Adiciona uma mensagem a ser retornado
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(string message)
        {
            if (this.Messages == null)
                this.Messages = new List<Messages>();

            this.Messages.Add(
                new Messages { 
                    Message = message
                }
            );
        }
    }

    public class Messages 
    {
        /// <summary>
        /// Mensagem
        /// </summary>
        public string Message { get; set; }
    }
}
