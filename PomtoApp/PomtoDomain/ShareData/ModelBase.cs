namespace PomtoDomain.ShareData
{
    public class ModelBase
    {
        public int ID { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataRemocao { get; set; }
        public bool Status { get; set; } = true;
    }
}
