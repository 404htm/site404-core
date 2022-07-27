namespace site_services.Models
{
    public class LayerParameter
    {
        public double Scale {get; set;}
        public int Magnitude { get; set; }
        public long? Seed { get; set; } = null;
    }
}
