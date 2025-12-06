namespace KorpInventory.Application.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string FormattedPrice => $"R$ {Price:N2}";
        public string StockStatus => StockQuantity > 0 ? "Em Estoque" : "Sem Estoque";
    }
}
