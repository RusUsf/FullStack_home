using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonkeyAPI.Models;

public partial class Monkeytable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order =1, TypeName ="serial")]
    public int Id { get; set; }

    public string? Tip { get; set; }
}
