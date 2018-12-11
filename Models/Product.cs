using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace ProdsAndCats.Models
{
  public class Product
  {
    [Key]
    public int ProductId {get; set;}

    [Required]
    [MinLength(3)]
    public string Name {get;set;}
    [Required]
    [MinLength(8)]
    public string Description {get;set;}
    [Required]
    [Range(0,1000000)]
    public decimal Price {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now; 
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<Association> Associations {get;set;}
    
    public Product(){}
  }
}