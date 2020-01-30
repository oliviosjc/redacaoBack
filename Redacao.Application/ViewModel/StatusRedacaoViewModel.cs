﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Redacao.Application.ViewModel
{
    public class StatusRedacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }
    }
}
