﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Dominio.Entity;

[Table("Contato")]
public class Contato : EntityBase
{
    [Length(2, 50)]
    public required string Nome { get; set; }
    public required string DDD { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }

    public void TratarTelefone(string telefone)
    {
        var telefoneSemMascara = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        DDD = telefoneSemMascara.Substring(0, 2);
        Telefone = telefoneSemMascara.Substring(2);
    }
}
