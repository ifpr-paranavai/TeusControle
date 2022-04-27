namespace TeusControle.Infrastructure.Dtos
{
    /// <summary>
    /// Classe de retorno para métodos internos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnData<T>
    {
        /// <summary>
        /// Situação do retorno - verdadeiro/falso
        /// </summary>
        public bool Sucess { get; set; }

        /// <summary>
        /// Informação
        /// </summary>
        public T Data { get; set; }

        public ReturnData(
            bool sucess,
            T data
        )
        {
            this.Sucess = sucess;
            this.Data = data;
        }

        public ReturnData(bool sucess)
        {
            this.Sucess = sucess;
        }

        public ReturnData()
        {
            this.Sucess = true;
        }
    }
}