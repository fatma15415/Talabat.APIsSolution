namespace Talabat.APIs.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }  
        public string Name { get; set; }    
        public string Description { get; set; } 
        public string PictureUrl { get; set; }   
        public decimal Price { get; set; }  
        public int ProducrBrandId { get; set;}
        public string ProducrBrand { get; set; }

        public int ProducrTypeId { get; set; }
        public string ProducrType { get; set; }


    }

}
