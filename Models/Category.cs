using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace ProdsAndCats.Models
{
  public class Category
  {
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [MinLength(3)]
    public string Name  {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Association> Associations { get; set; }
    public Category(){}
  }
}