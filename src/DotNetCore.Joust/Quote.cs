namespace DotNetCore.Joust
{
    public class Quote
    {
        // Total price including material cost, labor cost, and margin
        public float Price { get; set; }

        // Cost of all carpet orders from suppliers
        public float MaterialCost { get; set; }

        // Total cost of installation labor
        public float LaborCost { get; set; }

        // Inventory IDs of all rolls of carpet to be purchased
        public string[] RollOrders {get;set;}


    }
}