namespace PatikaDevParamHafta1Odev.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }

    }
}
