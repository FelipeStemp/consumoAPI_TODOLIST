namespace ConsumoAPI.Models
{
    public class Tarefa
    {
        public string? _id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool completed { get; set; } = false;
    }
}
