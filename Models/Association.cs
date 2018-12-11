using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace ProdsAndCats.Models
{
  public class Association
  {
    [Key]
    public int AssociationId { get; set; }
    [Required]
    public int CategoryId {get;set;}
    [Required]
    public int ProductId {get;set;}
    public Product ProductAssociated {get;set;}
    public Category CategoryAssociated {get;set;}
    public Association(){}
  }
}
