﻿using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Contato.CrossCutting.Logger
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}
