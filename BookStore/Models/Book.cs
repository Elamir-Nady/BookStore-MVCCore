namespace BookStore.Models
{
    public class Book
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Descryption { get; set; }
        public string ImageURL { get; set; }
        public Auther Auther { get; set; }
    }
}
